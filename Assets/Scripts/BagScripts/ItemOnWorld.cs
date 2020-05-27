using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Bag playerBag;
    
    ////玩家碰撞可收集物体
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            AddNewItem();
            Destroy(gameObject);
        }
    }

    //物体存入背包中
    public void AddNewItem(){
        if(!playerBag.itemList.Contains(thisItem)){
            //放到背包数据列表中
            playerBag.itemList.Add(thisItem);
            ////放到UI中的背包Cell中
            //BagManager.CreatNewItem(thisItem);
            //由bagManager.RefreshItem替代
        }
        else{
            thisItem.itemNo+=1;
        }
        BagManager.RefreshItem();
    }
    
}
