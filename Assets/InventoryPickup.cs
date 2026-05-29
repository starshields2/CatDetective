using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : MonoBehaviour
{
    public GameObject _itemData; // what item prefab is it.
    public string _dedicatedLine; //what peter will say when quinn gives him the item.
    public bool inRangeOfPlayer; //are we in range of the player? y/n
    public Transform playerAttchPoint; //where does it go when we grab it?
                                       
    public PlayerController playerController;

    void OnMouseDown()
    {
        if (inRangeOfPlayer)
        {
            Debug.Log("Get Item! Meow!");
            AttachToPlayer();
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
}
