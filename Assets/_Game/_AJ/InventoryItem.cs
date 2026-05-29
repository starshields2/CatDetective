using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item _itemData;
    public Image _DisplayIcon;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        this.gameObject.name = _itemData._itemName;
        _DisplayIcon.sprite = _itemData.icon;
    }
}
