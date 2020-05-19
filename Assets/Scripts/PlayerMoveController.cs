using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerMoveController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;
    public float maxSpeed;
    public float startSpeed;
    public float runSpeed;

    private enum playerState
    {
        WALK,
        RUN
    }
    private playerState currentState = playerState.WALK;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        String state = "walk";
        switch (currentState)
        {
            case playerState.WALK:
                Movement(state,startSpeed);
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    currentState = playerState.RUN;
                    anim.SetBool("run", true);

                }
                break;
            case playerState.RUN:
                Movement(state,runSpeed);
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    currentState = playerState.WALK;
                    anim.SetBool("run", false);
                }
                break;
        }
    }
    void Movement(String state,float speed)
    {
        float horizontalmove = Input.GetAxisRaw("Horizontal");
        float verticalmove = Input.GetAxisRaw("Vertical");
        //改变运动状态
        if (horizontalmove > 0.5 || horizontalmove < -0.5 || verticalmove > 0.5 || verticalmove < -0.5)
        {
            anim.SetBool("walk", true);
            Vector2 vector = new Vector2(horizontalmove, verticalmove) * speed * Time.deltaTime + rb.velocity;
            
            if (vector.magnitude > maxSpeed)
            {
                vector = vector.normalized*maxSpeed;//标准化，防止斜向移动过快
            }
            rb.velocity = vector;

        }
        else
        {
            anim.SetBool("walk", false);
        }
    }
}


