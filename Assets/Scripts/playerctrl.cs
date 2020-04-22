using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerctrl : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator anim;
    public float speed;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        float horizontalmove = Input.GetAxisRaw("Horizontal");
        float verticalmove = Input.GetAxisRaw("Vertical");
        Vector2 vector2;
        //改变运动状态
        if (horizontalmove * verticalmove != 0)
        {
            float speedsqrt=(float)Math.Sqrt(speed / 2);
            vector2 = new Vector2(horizontalmove * speedsqrt, verticalmove * speedsqrt);

        }
        else
        {
            vector2 = new Vector2(horizontalmove * speed, verticalmove * speed);
        }
        rb.AddForce(vector2);
        //改变动画
        anim.SetFloat("walk", Math.Abs(horizontalmove)+Math.Abs(verticalmove));
        //改变方向
        if (horizontalmove>0)
        {
            sr.flipX = true;
        }
        if(horizontalmove<0)
        {
            sr.flipX = false;
        }
    }
}
