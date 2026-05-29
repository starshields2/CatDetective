using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : MonoBehaviour
{
    public GameObject _itemData;
    public string _dedicatedLine;
    public bool inRangeOfPlayer;
    public Transform playerAttchPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (inRangeOfPlayer)
        {
            Debug.Log("I'M CLICKING");
            AttachToPlayer();
        }
    }

    public void AttachToPlayer()
    {
        this.gameObject.transform.position = playerAttchPoint.position;
        this.gameObject.transform.parent = playerAttchPoint;
    }
}
