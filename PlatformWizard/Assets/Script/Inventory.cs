using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{



    public float PNumberOfItems { get; set; }

    private InventorySlot[] mChildObjects;

    private InventoryItem[] mInventoryItem = new InventoryItem[30];

    private bool shown = false;

    // Use this for initialization
    private void Start()
    {
        mChildObjects = new InventorySlot[transform.GetChildCount()];
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            mChildObjects[i] = transform.GetChild(i).GetComponent<InventorySlot>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!shown)
            {
               
                shown = true;
                Time.timeScale = 0;
                transform.position = new Vector3(Camera.main.transform.position.x - (Camera.main.aspect/2),
                                                 Camera.main.transform.position.y - (Camera.main.aspect/4), -20);
                NotifyObservers();
            }
            else if (shown)
            {
                Time.timeScale = 1;
                shown = false;
                transform.position = new Vector3(Camera.main.transform.position.x - (Camera.main.aspect/2),
                                                 Camera.main.transform.position.y - (Camera.main.aspect/4), -30);
            }



        }
      
    }

  

    public bool IsRegistered(InventoryItem pInventoryItem)
    {
 
               foreach (InventorySlot tInventorySlot in mChildObjects)
               {
                   Debug.Log("Pos : " +(Vector2.Distance(pInventoryItem.transform.position, tInventorySlot.transform.position)));
                  if(Mathf.Abs((Vector2.Distance(pInventoryItem.transform.position,tInventorySlot.transform.position))) < 1.5f)
                  {
                     if(tInventorySlot.mInventoryItem == null || tInventorySlot.mInventoryItem == pInventoryItem)
                     {
                         return false;
                        
                     }
                     else
                     {
                         return true;
                         
                     }


                      
                  }
                
            
          
       
           
        }
        return false;

    }


       
    


    public Vector3 Register(InventoryItem pInventoryItem)
    {
        for (int i = 0; i < mInventoryItem.Length; i++)
        {
            if (mInventoryItem[i] == null)
            {
                foreach (InventorySlot mChildTranfrom in mChildObjects)
                {
                    if (mChildTranfrom != null && mChildTranfrom.mInventoryItem == null)
                    {
                        mInventoryItem[i] = pInventoryItem;
                        mInventoryItem[i].transform.position = mChildTranfrom.transform.position;
                        mChildTranfrom.mInventoryItem = pInventoryItem;
                        mInventoryItem[i].transform.position = new Vector3(mChildTranfrom.transform.position.x, mChildTranfrom.transform.position.y, mChildTranfrom.transform.position.z );
                        return mInventoryItem[i].transform.position;
                    }
                }
               
                mInventoryItem[i] = pInventoryItem;
                return mInventoryItem[i].transform.position;
            }
        }
        return transform.position;
    }



public void Register(InventoryItem pInventoryItem, Vector3 pPlaceToPostion)
    {


        foreach (InventorySlot tInventorySlot in mChildObjects)
        {
            
                if (Mathf.Abs(Vector2.Distance(tInventorySlot.transform.position, pInventoryItem.transform.position)) < 0.5f)
                {
                    Debug.Log("Register two");
                   
                    pInventoryItem.transform.position = new Vector3(tInventorySlot.transform.position.x, tInventorySlot.transform.position.y,tInventorySlot.transform.position.z);

               
            }
       
        }
    }

    public void UnRegister(Transform pInventoryItem)
    {
        foreach (InventorySlot tInventorySlot in mChildObjects)
        {
            if(tInventorySlot.mInventoryItem == pInventoryItem)
            {
                tInventorySlot.mInventoryItem = null;
            }
        }
    }

    public void NotifyObservers()
    {
        for (int i = 0; i < mInventoryItem.Length; i++)
        {
            if (mInventoryItem[i] != null)
            {
                foreach (InventorySlot mChildTranfrom in mChildObjects)
                {
                  if(mChildTranfrom.mInventoryItem == mInventoryItem[i])
                  {
                     
                      mInventoryItem[i].transform.position = new Vector3(mChildTranfrom.transform.position.x, mChildTranfrom.transform.position.y, mChildTranfrom.transform.position.z);
                  }
                }
            }
        }
    }

    
}
