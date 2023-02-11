using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   //const
    public static int GameScrTop = 9;
    public static int GameScrRight = 6;
    public string oxygen = "Red(Clone)";
    public string hydrogen = "Blue(Clone)";
    public string carbon = "Green(Clone)";

    [SerializeField]private string nextScene;

    public int score;
    public int rensa = 0;
    public GameObject[] blocks; 
    public GameObject twinBlocks;
    GameObject currentBlocks;
    GameObject nextBlocks1;
    GameObject nextBlocks2;
    public GameObject[,] fieldBlocks;
    ScoreRecorder scoreRecorder;
    List<GameObject> checkedFieldBlocks = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("ScoreRecorder");
        scoreRecorder = obj.GetComponent<ScoreRecorder>();
        fieldBlocks = new GameObject[GameScrRight,GameScrTop+2];
        StartCreateBlocks();
        // array();
        // StartCoroutine(EraseBlocks());
    }
    
    //to debug
    // void array()
    // {
    //     for(int x=0; x<GameScrRight; x++)
    //     {
    //         for(int y=0; y<GameScrTop-1; y++)
    //         {
    //             GameObject piece = Instantiate(blocks[Random.Range(0,3)]);
    //             piece.transform.position = new Vector3(x,y,1);
    //             fieldBlocks[x,y] = piece;
    //         }
    //     }
    // }

    public void Drop()
    {
        int empty_block = 0;
        for(int x=0; x<GameScrRight; x++)
        {
            for(int y=0; y<GameScrTop+2; y++)
            {
                if(fieldBlocks[x,y] == null)
                {
                    empty_block++;
                }
                else if(empty_block > 0)
                {
                    fieldBlocks[x,y].transform.position += new Vector3(0, -empty_block, 0);
                    fieldBlocks[x, y-empty_block] = fieldBlocks[x,y];
                    fieldBlocks[x,y] = null;
                }
            }
            empty_block = 0;
        }
        if(JudgeRenketsu())
        {
            StartCoroutine(EraseBlocks());
            rensa++;
            scoreRecorder.AddScore();
            Debug.Log("Count: " + rensa);
        }
        if(!JudgeRenketsu())
        {   
            rensa = 0;
            if(CanCreateBlock())
            {
                CreateBlocks();
            }
            else if(!CanCreateBlock())
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        for(int x=0; x<GameScrRight; x++)
        {
            for(int y=0; y<GameScrTop+2; y++)
            {
                Destroy(fieldBlocks[x,y]);
            }
        }
        Destroy(currentBlocks);
        Destroy(nextBlocks1);
        Destroy(nextBlocks2);
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    bool CanCreateBlock()
    {
        for(int x=0; x<GameScrRight; x++)
            {
                if(fieldBlocks[x,GameScrTop] != null)//ゲームオーバーの高さってどこまで？
                {
                    return false;
                }
            }
        return true;
    }

    bool JudgeRenketsu()
    {
        for(int y=0; y<GameScrTop+2; y++)
        {
            for(int x=0; x<GameScrRight; x++)
            {
                checkedFieldBlocks.Clear();
                if((OtoH_JudgeRenketsu(x,y) || CtoO_JudgeRenketsu(x,y)) && fieldBlocks[x,y] != null)
                {
                    return true;
                }
            }
        }
        return false;
    }


            bool OtoH_JudgeRenketsu(int x, int y)
            {
                checkedFieldBlocks.Clear();
                if(CountH_fromO(x, y, 0) >= 3 && CountH_fromO(x, y, 0) <= 6 && fieldBlocks[x,y] != null)
                {
                    return true;
                }
                return false;
            }
            bool CtoO_JudgeRenketsu(int x, int y)
            {
                checkedFieldBlocks.Clear();
                if(CountO_fromC(x, y, 0) >= 7 && CountO_fromC(x, y, 0) <= 8 && fieldBlocks[x,y] != null)
                {
                    return true;
                }
                return false;
            }



    IEnumerator EraseBlocks()
    {
        yield return new WaitForSeconds(1.0f);
        for(int y=0; y<GameScrTop+2; y++)
        {   
            for(int x=0; x<GameScrRight; x++)
            {
                StartCoroutine(DestroyH2O(x,y));
                StartCoroutine(DestroyCO2(x,y));
            }
        }
        yield return new WaitForSeconds(1.0f);
        Drop();
    }

            IEnumerator DestroyH2O(int x, int y)
            {
                checkedFieldBlocks.Clear();
                yield return new WaitForSeconds(0.1f);
                if(CountH_fromO(x, y, 0) == 5 && fieldBlocks[x,y] != null)//↑→
                {
                    Destroy(fieldBlocks[x,y]);
                    Destroy(fieldBlocks[x,y+1]);
                    Destroy(fieldBlocks[x+1,y]);
                }
                checkedFieldBlocks.Clear();
                yield return new WaitForSeconds(0.1f);
                if(CountH_fromO(x, y, 0) == 4 && fieldBlocks[x,y] != null)//←↓
                {
                    Destroy(fieldBlocks[x,y]);
                    Destroy(fieldBlocks[x,y-1]);
                    Destroy(fieldBlocks[x-1,y]);
                }
                checkedFieldBlocks.Clear();
                yield return new WaitForSeconds(0.1f);
                if(CountH_fromO(x, y, 0) == 3 && fieldBlocks[x,y] != null)//↓→
                {
                    Destroy(fieldBlocks[x,y]);
                    Destroy(fieldBlocks[x,y-1]);
                    Destroy(fieldBlocks[x+1,y]);
                }
                checkedFieldBlocks.Clear();
                yield return new WaitForSeconds(0.1f);
                if(CountH_fromO(x, y, 0) == 6 && fieldBlocks[x,y] != null)//←↑
                {
                    Destroy(fieldBlocks[x,y]);
                    Destroy(fieldBlocks[x,y+1]);
                    Destroy(fieldBlocks[x-1,y]);
                }
                checkedFieldBlocks.Clear();
            }

            IEnumerator DestroyCO2(int x, int y)
            {
                checkedFieldBlocks.Clear();
                if(CountO_fromC(x, y, 0) == 7 && fieldBlocks[x,y] != null)//↑↓
                {
                    Destroy(fieldBlocks[x,y]);
                    Destroy(fieldBlocks[x,y+1]);
                    Destroy(fieldBlocks[x,y-1]);
                    yield return new WaitForSeconds(0.1f);
                }
                checkedFieldBlocks.Clear();
                if(CountO_fromC(x, y, 0) == 8 && fieldBlocks[x,y] != null)//←→
                {
                    Destroy(fieldBlocks[x,y]);
                    Destroy(fieldBlocks[x+1,y]);
                    Destroy(fieldBlocks[x-1,y]);
                    yield return new WaitForSeconds(0.1f);
                }

            }

    int CountH_fromO(int x, int y, int renketusu)
    {
        //check countedfieldblock or not
        if(fieldBlocks[x,y] == null || checkedFieldBlocks.Contains(fieldBlocks[x,y]))
        {
            return renketusu;
        }
        checkedFieldBlocks.Add(fieldBlocks[x,y]);

        renketusu++;
        if(y != 0 && fieldBlocks[x,y-1] != null && (fieldBlocks[x,y].name == oxygen) && (fieldBlocks[x,y-1].name == hydrogen))//underside
        {
            checkedFieldBlocks.Add(fieldBlocks[x,y-1]);
            renketusu++;
            if(x != GameScrRight-1 && fieldBlocks[x+1,y] != null && fieldBlocks[x+1,y].name == hydrogen)//rightside
            {
                checkedFieldBlocks.Add(fieldBlocks[x+1,y]);
                renketusu++;
            }
            else if(x != 0 && fieldBlocks[x-1,y] != null && fieldBlocks[x-1,y].name == hydrogen)//leftside
            {
                checkedFieldBlocks.Add(fieldBlocks[x-1,y]);
                renketusu += 2 ;
            }
        }
        else if(y != GameScrTop+1 && fieldBlocks[x,y+1] != null && (fieldBlocks[x,y].name == oxygen) && (fieldBlocks[x,y+1].name == hydrogen))//upperside
        {   
            checkedFieldBlocks.Add(fieldBlocks[x,y+1]);
            renketusu++;
            if(x != GameScrRight-1 && fieldBlocks[x+1,y] != null && fieldBlocks[x+1,y].name == hydrogen)//rightside
            {
                checkedFieldBlocks.Add(fieldBlocks[x+1,y]);
                renketusu += 3 ;
            }
            else if(x != 0 && fieldBlocks[x-1,y] != null && fieldBlocks[x-1,y].name == hydrogen)//leftside
            {
                checkedFieldBlocks.Add(fieldBlocks[x-1,y]);
                renketusu += 4 ;
            }
        }
      
        return renketusu;
    }

    int CountO_fromC(int x, int y, int renketusu)
    {
        //check countedfieldblock or not
        if(fieldBlocks[x,y] == null || checkedFieldBlocks.Contains(fieldBlocks[x,y]))
        {
            return renketusu;
        }
        checkedFieldBlocks.Add(fieldBlocks[x,y]);

        renketusu++;
        if(y != 0 && fieldBlocks[x,y-1] != null && (fieldBlocks[x,y].name == carbon) && (fieldBlocks[x,y-1].name == oxygen))//underside
        {
            checkedFieldBlocks.Add(fieldBlocks[x,y-1]);
            renketusu++;
            if(y != GameScrTop+1 && fieldBlocks[x,y+1] != null && (fieldBlocks[x,y+1].name == oxygen))//upperside
            {
                checkedFieldBlocks.Add(fieldBlocks[x,y+1]);
                renketusu += 5;
            }
        }
        else if(x != 0 && fieldBlocks[x-1,y] != null && (fieldBlocks[x,y].name == carbon) && (fieldBlocks[x-1,y].name == oxygen))//leftside
        {   
            checkedFieldBlocks.Add(fieldBlocks[x-1,y]);
            renketusu++;
            if(x != GameScrRight-1 && fieldBlocks[x+1,y] != null && fieldBlocks[x+1,y].name == oxygen)//rightside
            {
                checkedFieldBlocks.Add(fieldBlocks[x+1,y]);
                renketusu += 6 ;
            }   
        }     
        return renketusu;
    }

    public void StartCreateBlocks()
    {
        currentBlocks = Instantiate(twinBlocks);
        currentBlocks.transform.position = new Vector3(2,GameScrTop,1);
        GameObject block1 = Instantiate(blocks[Random.Range(0,4)]);
        block1.transform.position = new Vector3(2,GameScrTop,1);
        block1.transform.SetParent(currentBlocks.transform, true);
        GameObject block2 = Instantiate(blocks[Random.Range(0,4)]);
        block2.transform.position = new Vector3(2,GameScrTop+1,1);    
        block2.transform.SetParent(currentBlocks.transform, true);    

        nextBlocks1 = Instantiate(twinBlocks);
        nextBlocks1.transform.position = new Vector3(7,GameScrTop-2,1);
        nextBlocks1.GetComponent<BlocksController>().enabled = false;
        GameObject block3 = Instantiate(blocks[Random.Range(0,4)]);
        block3.transform.position = new Vector3(7,GameScrTop-2,1);
        block3.transform.SetParent(nextBlocks1.transform, true);
        GameObject block4 = Instantiate(blocks[Random.Range(0,4)]);
        block4.transform.position = new Vector3(7,GameScrTop-1,1);    
        block4.transform.SetParent(nextBlocks1.transform, true);   

        nextBlocks2 = Instantiate(twinBlocks);
        nextBlocks2.transform.position = new Vector3(9,GameScrTop-2,1);
        nextBlocks2.GetComponent<BlocksController>().enabled = false;
        GameObject block5 = Instantiate(blocks[Random.Range(0,4)]);
        block5.transform.position = new Vector3(9,GameScrTop-2,1);
        block5.transform.SetParent(nextBlocks2.transform, true);
        GameObject block6 = Instantiate(blocks[Random.Range(0,4)]);
        block6.transform.position = new Vector3(9,GameScrTop-1,1);    
        block6.transform.SetParent(nextBlocks2.transform, true);   
    }

    public void CreateBlocks()
    {
        currentBlocks = nextBlocks1;
        currentBlocks.transform.position = new Vector3(2,GameScrTop,1);
        currentBlocks.GetComponent<BlocksController>().enabled = true;

        nextBlocks1 = nextBlocks2;
        nextBlocks1.transform.position = new Vector3(7,GameScrTop-2,1);
    
        nextBlocks2 = Instantiate(twinBlocks);
        nextBlocks2.transform.position = new Vector3(9,GameScrTop-2,1);
        nextBlocks2.GetComponent<BlocksController>().enabled = false;
        GameObject block5 = Instantiate(blocks[Random.Range(0,4)]);
        block5.transform.position = new Vector3(9,GameScrTop-2,1);
        block5.transform.SetParent(nextBlocks2.transform, true);
        GameObject block6 = Instantiate(blocks[Random.Range(0,4)]);
        block6.transform.position = new Vector3(9,GameScrTop-1,1);    
        block6.transform.SetParent(nextBlocks2.transform, true);   
    }
}