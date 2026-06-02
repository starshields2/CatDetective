using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> _inventory = new List<GameObject>(); //Inevntory items.
    public GameObject InventoryParent; // The Parent GO where the inventory items go. 
    public GameObject ItemTemplate; // When giving Peter items, Quinn's script should replace this with the correct GO. 
    public int maxItems = 4;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddToInventory();
        }
    }

    [ContextMenu("TestInventoryAddition")]
    public void AddToInventory()
    {
        if(_inventory.Count < maxItems)
        {
            Debug.Log("Added item to inventory. Meow.");
            GameObject itemToAdd = Instantiate(ItemTemplate, InventoryParent.transform.position, InventoryParent.transform.rotation);
            itemToAdd.transform.SetParent(InventoryParent.transform, false);
            _inventory.Add(itemToAdd);
           
        }
        else if (_inventory.Count >= maxItems)
        {
            Debug.Log("Can't add any more items. Make Peter use an item. Meow.");
            //feed back feed back, have Peter say something cool.
        }
    }
}
