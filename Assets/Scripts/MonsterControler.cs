using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterControler : MonoBehaviour
{
    public GameObject player;

    private Animator anim;

    private bool monsterAwake = false;
    private bool dead = false;
    private bool attacked = false;
    public float chaseSpeed;
    public float maxSpeed = 4;
    public float force = 8;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FindHuman();
        anim.SetBool("dead",dead);
        //追逐
        if (monsterAwake)
        {
            //agent.SetDestination(player.transform.position);
        }
    }
    //寻找人类
    void FindHuman()
    {
        if (!dead)
        {
            //得到与主角的距离                    
            float distance = Vector2.Distance(this.transform.position, player.transform.position);
            //距离判断         
            if (distance > 8f)
            {
                monsterAwake = false;
            }
            else
            {
                monsterAwake = true;
            }
            if (!attacked)
            {
                anim.SetBool("chasing", monsterAwake);
            }
           

            if (monsterAwake && distance <= 2.3f)
            {
                //判断攻击CD       
                //攻击状态
                //anim.SetBool("attack", true);
                //anim.SetInteger("attackKind", 1);
                //攻击动作结束在计时器中结束攻击状态
            }
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "bullet")
        {
            attacked = true;
            anim.SetBool("beAttacked", true);
        }
    }

    void AfterAttacked()
    {
        anim.SetBool("beAttacked", false);
    }
    void AfterDead()
    {
        anim.SetTrigger("deadbody");
    }
}
