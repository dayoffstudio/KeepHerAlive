using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//负责背包开闭、
public class BagController : MonoBehaviour
{
    public GameObject myBag;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenMyBag();
    }
    //开关背包
    void OpenMyBag(){
        if(Input.GetKeyDown(KeyCode.O)){
            isOpen=!isOpen;
            myBag.SetActive(isOpen);
        }
    }
}
