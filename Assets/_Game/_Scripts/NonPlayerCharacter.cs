using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public Dialogue _dialogue;
    public Inventory _inventorySystem;
    public GameObject ItemPickedUp;
    public PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartTriggeredDialogue();
        }
        if(other.tag == "Item")
        {
            ItemPickedUp = other.gameObject;

            StartCoroutine(StartItemCollection());

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _dialogue.ClearDialogue();
        }
    }

    void StartTriggeredDialogue()
    {
        Debug.Log("Triggering Dialogues");
        _dialogue.InGameDialogue();
    }

    public IEnumerator StartItemCollection()
    {
        InventoryPickup _itemInfo = ItemPickedUp.GetComponent<InventoryPickup>();
        _inventorySystem.ItemTemplate = _itemInfo._itemData;
        
        _dialogue.itemDialogue = _itemInfo._dedicatedLine;
        _inventorySystem.AddToInventory();
       
        
        
        StartPlayItemDialogue();
        yield return new WaitForSeconds(3f);
        DestroyItemPickedUp();
    }

    void DestroyItemPickedUp()
    {
        Destroy(ItemPickedUp);
        ItemPickedUp = null;
        _playerController.itemGrabbed = false;
    }

    void StartPlayItemDialogue()
    {
        _dialogue.PlayItemDialogue();
        
    }
}
