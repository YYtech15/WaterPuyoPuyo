using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController : MonoBehaviour
{
    float time_now ;
    // Start is called before the first frame update
    void Start()
    {
        time_now = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.gameObject.transform.position += new Vector3(-1,0,0);
            if(!CanMove())
            {
                this.gameObject.transform.position += new Vector3(1,0,0);
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.gameObject.transform.position += new Vector3(1,0,0);
            if(!CanMove())
            {
                this.gameObject.transform.position += new Vector3(-1,0,0);
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) || Time.time - time_now > 1)
        {
            this.gameObject.transform.position += new Vector3(0,-1,0);
            if(!CanMove())
            {
                this.gameObject.transform.position += new Vector3(0,1,0);//Can't move at the bottom 
                ChangeToFieldBlocks();//unmovable blocks to fieldblocks
                this.gameObject.transform.DetachChildren();
                // FindObjectOfType<GameManager>().StartCreateBlocks();
                FindObjectOfType<GameManager>().Drop();
                Destroy(this.gameObject, 10f);//destroy parent in 10 seconds
                this.enabled = false;//detach this script from parent
            }

            time_now = Time.time;
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            while(CanMove())
            {
                this.gameObject.transform.position += new Vector3(0,-1,0);
            }
            if(!CanMove())
            {
                this.gameObject.transform.position += new Vector3(0,1,0);//Can't move at the bottom 
                ChangeToFieldBlocks();//unmovable blocks to fieldblocks
                this.gameObject.transform.DetachChildren();
                // FindObjectOfType<GameManager>().StartCreateBlocks();
                FindObjectOfType<GameManager>().Drop();
                Destroy(this.gameObject, 10f);//destroy parent in 10 seconds
                this.enabled = false;//detach this script from parent
            }

            time_now = Time.time;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            this.gameObject.transform.RotateAround(transform.position, new Vector3(0,0,1), -90);
            foreach(Transform childBlocks in transform)
            {
                childBlocks.transform.RotateAround(childBlocks.transform.position, new Vector3(0,0,1), 90);
            }
            if(!CanMove())
            {
                this.gameObject.transform.RotateAround(transform.position, new Vector3(0,0,1), 90);
                foreach(Transform childBlocks in transform)
                {
                    childBlocks.transform.RotateAround(childBlocks.transform.position, new Vector3(0,0,1), -90);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            this.gameObject.transform.RotateAround(transform.position, new Vector3(0,0,1), 90);
            foreach(Transform childBlocks in transform)
            {
                childBlocks.transform.RotateAround(childBlocks.transform.position, new Vector3(0,0,1), -90);
            }
            if(!CanMove())
            {
                this.gameObject.transform.RotateAround(transform.position, new Vector3(0,0,1), -90);
                foreach(Transform childBlocks in transform)
                {
                    childBlocks.transform.RotateAround(childBlocks.transform.position, new Vector3(0,0,1), 90);
                }
            }
        }
    }

    bool CanMove()
    {
        foreach(Transform childBlocks in transform)
        {
            int X = Mathf.RoundToInt(childBlocks.transform.position.x);
            int Y = Mathf.RoundToInt(childBlocks.transform.position.y);
            if(X < 0 || X > GameManager.GameScrRight-1 || Y < 0 )
            {
                return false;
            }
            if(FindObjectOfType<GameManager>().fieldBlocks[X,Y] != null)
            {
                return false;
            }
        }
        return true;
    }

    void ChangeToFieldBlocks()
    {
        foreach(Transform childBlocks in transform)
        {
            int X = Mathf.RoundToInt(childBlocks.transform.position.x);
            int Y = Mathf.RoundToInt(childBlocks.transform.position.y);
            FindObjectOfType<GameManager>().fieldBlocks[X,Y] = childBlocks.gameObject;
        }
    }
}