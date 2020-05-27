using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerState : MonoBehaviour
{
    public Image playerHPImage;
    public Image followerHPImage;
    public int MaxHP = 10;

    private int _playerHP;
    private int _followerHP;
    public int playerHP
    {
        get
        {
            return _playerHP;
        }
        set
        {
            if (value<0)
            {
                _playerHP = 0;
            }
            else
            {
                _playerHP = value;
            }
            playerHPImage.fillAmount = (float)_playerHP / 10;
        }

    }
    public int followerHP
    {
        get
        {
            return _followerHP;
        }
        set
        {
            if (value < 0)
            {
                _followerHP = 0;
            }
            else
            {
                _followerHP = value;
            }
            playerHPImage.fillAmount = (float)_followerHP / 10;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
