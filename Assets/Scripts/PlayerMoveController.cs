using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;
    public float maxSpeed;
    public float force = 8;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        //改变运动状态
        if (horizontalmove > 0.5 || horizontalmove < -0.5 || verticalmove > 0.5 || verticalmove < -0.5)
        {
            Vector2 vector = new Vector2(horizontalmove, verticalmove);
            anim.SetBool("walk", true);
            
            rb.AddForce(vector.normalized * force);//标准化，防止斜向移动过快

        }
        else
        {
            //依靠rigidbody2D 自带的LinearDrag来产生阻尼
            anim.SetBool("walk", false);
        }
    }
}
