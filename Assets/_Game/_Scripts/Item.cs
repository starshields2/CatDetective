using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(
    fileName = "NewItem",
    menuName = "Inventory/Item",
    order = 1
)]
public class Item : ScriptableObject
{
    public string _itemName;
    public int ID;
    public Sprite icon;
    public string description;
    public string dialogueLine;
    
}
