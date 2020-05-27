using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//负责控制背包内物品管理
public class BagManager : MonoBehaviour
{
    static BagManager instance;    //maybe static

    public Bag myBag;
    public GameObject cellGrid;
    public Cell cellPrefab;
    public Text itemInfo;

    //确保单例
    void Awake() {
        if(instance!=null){
            Destroy(this);
        }
        instance=this;
        //一来就显示背包中物品
        RefreshItem();
    }

    
    private static void OnEnable() {
        instance.itemInfo.text="";
    }

    public static void UpdateItemInfo(string itemDescrip){
        instance.itemInfo.text=itemDescrip;
    }
    
    //将Item类中数据存入Cell中
    public static void CreatNewItem(Item item){  //maybe static
        //在grid中加入预设体cell，角度不变
        Cell newItem=Instantiate(instance.cellPrefab,instance.cellGrid.transform.position,Quaternion.identity);
        //获得预设体的物品本身，以便取得其内部数据
        newItem.gameObject.transform.SetParent(instance.cellGrid.transform);

        newItem.cellItem=item;
        newItem.cellImage.sprite=item.itemImage;
        newItem.cellItemNo.text=item.itemNo.ToString();
    }

    public static void RefreshItem(){
        //销毁grid中全部数据后重新加载
        //销毁
        for(int i=0;i<instance.cellGrid.transform.childCount;i++){
            if(instance.cellGrid.transform.childCount==0)
               break;
            Destroy(instance.cellGrid.transform.GetChild(i).gameObject);
        }

        //重新创建
        for(int i=0;i<instance.myBag.itemList.Count;i++){
            CreatNewItem(instance.myBag.itemList[i]);
        }

    }
}
