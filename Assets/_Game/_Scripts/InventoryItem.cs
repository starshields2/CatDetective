using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item _itemData;
    public Image _DisplayIcon;
    [SerializeField] private Button _inventoryButton;
    public bool isKeyItem;

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
        _inventoryButton = GetComponent<Button>();

        _inventoryButton.onClick.AddListener(UseItem);
    }

    public void UseItem()
    {
        Debug.Log("Used Item. Meow.");
        if (isKeyItem)
        {
            Debug.Log("Meow, correct item.");
            //feedback
            //do item thing.
            //destroy item
        }
    }
}
