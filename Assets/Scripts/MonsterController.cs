using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    //public GameObject player;
    static Transform player;
    private Animator anim;
    private new Rigidbody2D rigidbody;
    //private bool monsterAwake = false;
    private bool dead = false;
    private bool attacked = false;

    public float chaseRadius;    //追击半径，玩家进入后怪物会追击玩家
    public float wanderRadius;   //放弃半径，当怪物超出会放弃追击
    public float attackRange;    //攻击距离
    public float startSpeed;
    public float chaseSpeed;
    public float maxSpeed = 4;
    public float force = 8;
    private enum MonsterState
    {
        WANDER,     //移动巡逻
        CHASE,      //追击玩家
        ATTACK      //攻击玩家
    }
    private MonsterState currentState = MonsterState.WANDER;//默认状态为移动
    public float actRestTme;            //更换待机指令的间隔时间
    private float lastActTime;          //最近一次指令时间
    private int attackKind;             //攻击方式
    private float x, y;                 //巡逻方向
    private float diatanceToPlayer;         //怪物与玩家的距离

    public float getAttackForce = 2;
    public GameObject bulletHole;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        //检查并修正怪物设置
        //攻击距离不大于自卫半径，否则就无法触发追击状态，直接开始战斗了
        attackRange = Mathf.Min(chaseRadius, attackRange);
        //游走半径不大于追击半径，否则怪物可能刚刚开始追击就停止
        wanderRadius = Mathf.Min(chaseRadius, wanderRadius);

        InvokeRepeating("DirChange", 0.1f, 2f);
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            //游走，根据状态随机时生成的目标位置修改朝向，并向前移动
            case MonsterState.WANDER:
                //改变运动状态
                Movement(x, y, startSpeed);
                //该状态下的检测指令
                WanderRadiusCheck();
                //WanderRadiusCheck();
                break;

            //追击状态，朝着玩家跑去
            case MonsterState.CHASE:
                //改变状态
                float x1=0, y1 = 0;
                y1 = player.position.y - this.transform.position.y;
                x1 = player.position.x - this.transform.position.x;
                if (x1 < 0) x1 = -1;
                else if (x1 > 0) x1 = 1;
                if (y1 < 0) y1 = -1;
                else if (y1 > 0) y1 = 1;
                Movement(x1, y1, chaseSpeed);
                //切换动画
                anim.SetBool("chasing", true);

                //该状态下的检测指令
                ChaseRadiusCheck();
                break;
            case MonsterState.ATTACK:
                //切换动画
                anim.SetBool("attack", true);
                //攻击方式
                Random rd = new Random();
                //attackKind = rd.Next(1,2);
                //anim.SetInteger("attackKind", attackKind);
                anim.SetInteger("attackKind", 1);
                break;
        }
    }
    void DirChange()
    {
        y = Random.value < 0.3f ? 0f : 1f;
        x = Random.value < 0.3f ? 0f : 1f;
        if (x!=0)
        {
          x = Random.value < 0.5f ? -1f : 1f;
        }
        if (y != 0)
        {
           y = Random.value < 0.5f ? -1f : 1f;
        }
    }
    void Movement(float x,float y,float speed)
    {
        if (x > 0.5 && transform.eulerAngles.y != 180)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (x < -0.5 && transform.eulerAngles.y != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        Vector2 vector = new Vector2(x, y) * speed * Time.deltaTime + rigidbody.velocity;
        anim.SetBool("chasing", false);
        if (vector.magnitude > speed)
        {
            vector = vector.normalized * speed;//标准化，防止斜向移动过快
        }
        rigidbody.velocity = vector;
    }
    ///
    /// 游走状态检测，检测敌人距离
    ///
    void WanderRadiusCheck()
    {
        diatanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (diatanceToPlayer < chaseRadius)
        {
            currentState = MonsterState.CHASE;
        }
    }

    ///
    /// 追击状态检测，检测敌人是否进入攻击范围
    ///
    void ChaseRadiusCheck()
    {
        diatanceToPlayer = Vector2.Distance(player.position, transform.position);
        //如果进入攻击距离就开始攻击
        if (diatanceToPlayer < attackRange)
        {
            //检测攻击CD
            //if() break;
            currentState = MonsterState.ATTACK;
        }
        //如果超出追击范围则开始游走
        if (diatanceToPlayer > wanderRadius)
        {
            currentState = MonsterState.WANDER;
        }
    }

    //被攻击
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

    //动画事件
    void AfterAttack()
    {
        anim.SetInteger("attackKind", 0);
        anim.SetBool("attack", false);
        currentState = MonsterState.CHASE;
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
