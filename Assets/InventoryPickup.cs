using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : MonoBehaviour
{
    public enum ItemType
    {
        Action, // does it do something in the world?
        CaseItem, // should we give it to Peter?
        Sequence // does it unlock/reveal something else?
    }

    public ItemType _itemType = ItemType.Action;
    public CaseManager _caseManager;
    public GameObject _itemDataGameObject; // what item prefab is it.
    public string _dedicatedLine; //what peter will say when quinn gives him the item.
    public bool inRangeOfPlayer; //are we in range of the player? y/n
    public Transform playerAttchPoint; //where does it go when we grab it?
                                       
    public PlayerController playerController;

    public ActionItem _actionItem;


    void OnMouseDown()
    {
        //when we click on an item, check if it's in range of player and then determine what to do based on such.

        if (inRangeOfPlayer)
        {
            switch (_itemType)
            {
                case ItemType.Action:
                    Debug.Log("Trigger another Action.");
                    TriggerAction();
                    break;
                case ItemType.CaseItem:
                    Debug.Log("Give this item to Peter.");
                    AttachToPlayer();
                    break;
                case ItemType.Sequence:
                    Debug.Log("Active a different item.");
                    break;
                default:
                    break;
            }
            
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            inRangeOfPlayer = true;
            playerController = other.gameObject.GetComponent<PlayerController>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            inRangeOfPlayer = false;
            playerController = null;
        }
    }
    public void AttachToPlayer()
    {
        if (!playerController.itemGrabbed)
        {
        playerController.itemGrabbed = true;
        this.gameObject.transform.position = playerAttchPoint.position;
        this.gameObject.transform.parent = playerAttchPoint;
        }
        else if (playerController.itemGrabbed)
        {
            Debug.Log("Already have an item, meow.");
            //feedback.
        }
    }

    void TriggerAction()
    {
        Debug.Log("Triggering Action");
        _actionItem.TriggerNewAction();
    }
}
