using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public float maxSpeed = 4;
    public float force = 8;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
        bool movePressed = false;
        if (horizontalmove > 0.5)
        {
            sr.flipX = true;
            movePressed = true;
        }
        else if (horizontalmove < -0.5)
        {
            sr.flipX = false;
            movePressed = true;
        }
        if (verticalmove > 0.5)
        {
            //可以在这里添加前后动画切换
            movePressed = true;
        }
        else if (verticalmove < -0.5)
        {
            movePressed = true;
        }
        //改变运动状态
        if (movePressed)
        {
            Vector2 vector = new Vector2(horizontalmove, verticalmove);
            anim.SetBool("walk", true);

            rb.AddForce(vector.normalized * force);//标准化，防止斜向移动过快
            if(rb.velocity.sqrMagnitude> maxSpeed)
            {
                vector = rb.velocity;
                vector.Normalize();//标准化再乘上速度的话，速度大小会永远小于maxSpeed
                vector *= maxSpeed;
                rb.velocity.Set(vector.x,vector.y);
            }
        }
        else
        {
            //依靠rigidbody2D 自带的LinearDrag来产生阻尼
            anim.SetBool("walk", false);
        }
        

    }
}
