using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public GameObject player;

    private Animator anim;
    private new Rigidbody2D rigidbody;
    private bool monsterAwake = false;
    private bool dead = false;
    private bool attacked = false;
    public float chaseSpeed;
    public float maxSpeed = 4;
    public float force = 8;
    public float getAttackForce = 2;
    public GameObject bulletHole;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
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
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Bullet")
        {

            GetAttacked(collider);
        }
        else if (collider.tag=="Player")
        {

        }
    }
    private void GetAttacked(Collider2D collider)
    {
        anim.SetBool("beAttacked", true);
        Rigidbody2D bulletRB = collider.GetComponent<Rigidbody2D>();
        Vector2 force = bulletRB.velocity * getAttackForce;
        rigidbody.AddForce(force);
        Instantiate(bulletHole, collider.transform.position,new Quaternion());
        Destroy(collider.gameObject, 0.01f);
        if (force.x>0.1&&transform.eulerAngles.y!=0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(force.x<-0.1 && transform.eulerAngles.y != 180)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
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
