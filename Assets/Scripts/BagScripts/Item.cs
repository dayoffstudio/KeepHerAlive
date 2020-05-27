using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Bag/Item")]
public class Item : ScriptableObject {
    public string itemName;
    public Sprite itemImage;
    public int itemNo;
    public string itemInfo;
}

