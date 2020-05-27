using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textspeed;
    bool textfinished;//打印效果
    bool canceltype;//加速打印

    [Header("头像")]
    public Sprite face1,face2;

    List<string> textlist = new List<string>();

    void Awake()
    {
        GetTextFormFile(textFile);
    }

    private void OnEnable()
    {
        textfinished = true;
        StartCoroutine(SetTextUI());  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && index == textlist.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
           
            if (textfinished && !canceltype)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textfinished)
            {
                canceltype = !canceltype;
            }
        }
    }
    
    void GetTextFormFile(TextAsset file)
    {
        textlist.Clear();
        index = 0;

        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textlist.Add(line);
        }
    }
    
    IEnumerator SetTextUI()
    {
        textfinished = false;
        textLabel.text = "";
        switch (textlist[index])
        {
            case "A":
                faceImage.sprite = face1;
                index++;
                break;
            case "B":
                faceImage.sprite = face2;
                index++;
                break;
        }
        int letter = 0;
        while (!canceltype && letter < textlist[index].Length - 1)
        {
            textLabel.text += textlist[index][letter];
            letter++;
            yield return new WaitForSeconds(textspeed);
        }
        textLabel.text = textlist[index];
        canceltype = false;
        textfinished = true;
        index++;
    }
}
