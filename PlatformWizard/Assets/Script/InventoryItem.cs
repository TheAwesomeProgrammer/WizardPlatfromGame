using UnityEngine;
using System.Collections;

public enum Item
{
    IsLiftet,
    IsDroppet,
    IsLaying
}

public class InventoryItem : MonoBehaviour
{

    

    private Inventory mInventory;
    private Item mItem;

	// Use this for initialization
	void Start () {
	mInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    mItem = Item.IsLaying;
	}
	
	// Update is called once per frame
	void Update () {
        if(mItem == Item.IsLiftet)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x,transform.position.y,-10);
        }
	}

    void OnTriggerEnter(Collider pCollider)
    {
       if(pCollider.tag == "Player")
       {
           transform.position = mInventory.Register(this);
           
       }
        
    }
    void OnMouseDown()
    {

        if ( mItem == Item.IsLaying)
        {
            mItem = Item.IsLiftet;
        }

        else if (!mInventory.IsRegistered(this) && mItem == Item.IsLiftet)
        {
            mItem = Item.IsLaying;
            mInventory.Register(this,transform.position);
            
        }

      
       
    }
   
  
}
