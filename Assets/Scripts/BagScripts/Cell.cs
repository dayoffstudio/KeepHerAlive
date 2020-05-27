using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Item cellItem;
    public Image cellImage;
    public Text cellItemNo;

    //点击物品显示信息
    public void ItemOnClicked(){
        BagManager.UpdateItemInfo(cellItem.itemInfo);
    }
}
