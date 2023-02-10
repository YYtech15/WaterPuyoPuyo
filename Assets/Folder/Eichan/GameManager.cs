using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   //const
    public static int GameScrTop = 11;
    public static int GameScrRight = 6;

    public GameObject[] blocks; 
    public GameObject twinBlocks;
    GameObject currentBlocks;
    public GameObject[,] fieldBlocks;

    List<GameObject> checkedFieldBlocks = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        fieldBlocks = new GameObject[6,13];
        CreateBlocks();
        //array();
        //StartCoroutine(EraseBlocks());
    }

    //not use
    void array()
    {
        for(int x=0; x<6; x++)
        {
            for(int y=0; y<10; y++)
            {
                GameObject piece = Instantiate(blocks[Random.Range(0,4)]);
                piece.transform.position = new Vector3(x,y,0);
                fieldBlocks[x,y] = piece;
            }
        }
    }

    public void Drop()
    {
        int empty_block = 0;
        for(int x=0; x<6; x++)
        {
            for(int y=0; y<13; y++)
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
        if(BoolRenketsu())
        {
            StartCoroutine(EraseBlocks());
        }
        if(!BoolRenketsu())
        {
            CreateBlocks();
        }
    }
    bool BoolRenketsu()
    {
        for(int x=0; x<6; x++)
        {
            for(int y=0; y<13; y++)
            {
                checkedFieldBlocks.Clear();
                if(CountRenketsu(x, y, 0) >= 4 && fieldBlocks[x,y] != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
    //ここをいじる
    IEnumerator EraseBlocks()
    {
        yield return new WaitForSeconds(0.5f);
        for(int x=0; x<6; x++)
        {
            for(int y=0; y<13; y++)
            {
                checkedFieldBlocks.Clear();
                if(CountRenketsu(x, y, 0) >= 4 && fieldBlocks[x,y] != null)
                {
                    Destroy(fieldBlocks[x,y]);
                }
            }
        }
        yield return new WaitForSeconds(0.5f);
        Drop();
    }
    //ここをいじる
    int CountRenketsu(int x, int y, int renketusu)
    {
        //check countedfieldblock or not
        if(fieldBlocks[x,y] == null || checkedFieldBlocks.Contains(fieldBlocks[x,y]))
        {
            return renketusu;
        }
        checkedFieldBlocks.Add(fieldBlocks[x,y]);

        renketusu++;
        if(x != GameScrRight-1 && fieldBlocks[x+1,y] != null && fieldBlocks[x,y].name == fieldBlocks[x+1,y].name)//rightside
        {
            renketusu = CountRenketsu(x+1, y, renketusu);
        }
        if(x != 0 && fieldBlocks[x-1,y] != null && fieldBlocks[x,y].name == fieldBlocks[x-1,y].name)//leftside
        {
            renketusu = CountRenketsu(x-1, y, renketusu);
        }
        if(y != 0 && fieldBlocks[x,y-1] != null && fieldBlocks[x,y].name == fieldBlocks[x,y-1].name)//underside
        {
            renketusu = CountRenketsu(x, y-1, renketusu);
        }
        if(y != GameScrTop+1 && fieldBlocks[x,y+1] != null && fieldBlocks[x,y].name == fieldBlocks[x,y+1].name)//upperside
        {
            renketusu = CountRenketsu(x, y+1, renketusu);
        }
        return renketusu;
    }

    public void CreateBlocks()
    {
        currentBlocks = Instantiate(twinBlocks);
        currentBlocks.transform.position = new Vector3(2,GameScrTop,0);

        GameObject block1 = Instantiate(blocks[Random.Range(0,4)]);
        block1.transform.position = new Vector3(2,GameScrTop,0);
        block1.transform.SetParent(currentBlocks.transform, true);

        GameObject block2 = Instantiate(blocks[Random.Range(0,4)]);
        block2.transform.position = new Vector3(2,GameScrTop+1,0);    
        block2.transform.SetParent(currentBlocks.transform, true);    
    }
}
