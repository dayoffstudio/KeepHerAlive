using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    private Animator anim;
    private new Rigidbody2D rigidbody;
    public float hp;
    public float hunger;
    public float chaseRadius;    //追击半径
    public float standRadius;    //半径待机
    static Transform player;

    public float chaseSpeed = 4;
    public float force = 8;

    private enum FollowerState
    {
        STAND,  //呆在玩家身边
        CHASE,  //追击玩家
        DEAD
    }
    private FollowerState currentState = FollowerState.STAND;//默认状态为移动
    private float diatanceToPlayer;                         //爱人与玩家的距离

    public float getAttackForce = 2;
    public GameObject bulletHole;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>(); 
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            //游走，根据状态随机时生成的目标位置修改朝向，并向前移动
            case FollowerState.STAND:
                //改变运动状态
                anim.SetBool("walk", false);
                //该状态下的检测指令
                StandRadiusCheck();
                //WanderRadiusCheck();
                break;

            //追击状态，朝着玩家跑去
            case FollowerState.CHASE:
                //改变状态
                Movement(player.position - this.transform.position, chaseSpeed);
                //切换动画
                anim.SetBool("walk", true);

                //该状态下的检测指令
                ChaseRadiusCheck();
                break;
        }
    }
    void StandRadiusCheck()
    {
        diatanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (diatanceToPlayer > chaseRadius)
        {
            currentState = FollowerState.CHASE;
        }
    }
    void ChaseRadiusCheck()
    {
        diatanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (diatanceToPlayer < standRadius)
        {
            currentState = FollowerState.STAND;
        }
    }
    void Movement(Vector2 vector2, float speed)
    {
        if (vector2.x > 0 && transform.eulerAngles.y != 180)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (vector2.x < 0 && transform.eulerAngles.y != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        vector2 = vector2.normalized * speed;//标准化，防止斜向移动过快
        rigidbody.velocity = vector2;
    }
    //被攻击
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Bullet")
        {

            GetAttacked(collider);
        }
        else if (collider.tag == "Player")
        {

        }
    }
    private void GetAttacked(Collider2D collider)
    {
        anim.SetBool("hurt", true);
        Rigidbody2D bulletRB = collider.GetComponent<Rigidbody2D>();
        Vector2 force = bulletRB.velocity * getAttackForce;
        rigidbody.AddForce(force);
        Instantiate(bulletHole, collider.transform.position, new Quaternion());
        Destroy(collider.gameObject, 0.01f);
        if (force.x > 0.1 && transform.eulerAngles.y != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (force.x < -0.1 && transform.eulerAngles.y != 180)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    void AfterAttacked()
    {
        anim.SetTrigger("hurtend");
    }
    void AfterDead()
    {
        anim.SetTrigger("deadbody");
    }
}
