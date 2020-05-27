using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public Image image;
    public Text text;

    public void LoadNextScene(){
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene(){
        loadScreen.SetActive(true);
        AsyncOperation operation=SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);//加载下一个场景
        //operation.allowSceneActivation=false;     //停在加载页面，想看到效果就取消注释
        while(!operation.isDone){
            text.text="抗体研究中..."+operation.progress*100+"%";
            yield return null;
        }
    } 

}
