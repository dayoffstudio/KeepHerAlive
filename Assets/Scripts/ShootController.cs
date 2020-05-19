using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Transform shoulder;
    public Transform muzzle;
    private Animator anima;
    public GameObject bullet;
    public GameObject objShoulder;
    public GameObject fireEffect;
    public int coolDown = 40;
    private int coolDownCount = 0;
    private bool isOnFire = false;
    private Vector3 mousePosition = new Vector3();
    private Vector3 sightLine = new Vector3();
    private Rigidbody2D rb;
    private bool if_armed=false;
    
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coolDownCount = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        //攻击是否被激活
        if (Input.GetKeyDown(KeyCode.F))
        {
            if_armed = !if_armed;
            //激活或禁用移动手臂
            objShoulder.SetActive(if_armed);
        }
        if (anima.GetBool("run") && anima.GetBool("walk"))
        {
            objShoulder.SetActive(false);
        }
        else
        {
            objShoulder.SetActive(if_armed);
        }
        if (!if_armed)
        {
            anima.SetBool("armed", false);
            float horizontalmove = Input.GetAxisRaw("Horizontal");


            if (horizontalmove > 0.5&&transform.eulerAngles.y!=180)
            {

                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (horizontalmove<-0.5 && transform.eulerAngles.y != 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            return;
        }

        anima.SetBool("armed", if_armed);
        //需要添加没死的情况判断
        if (Input.mousePosition != mousePosition)
        {
            mousePosition = Input.mousePosition;
            sightLine = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
            
            
            if (sightLine.x > 0.1)
            {
                float deg = Mathf.Rad2Deg * Mathf.Atan(sightLine.y / sightLine.x);
                transform.eulerAngles = new Vector3(0, 180, 0);
                shoulder.eulerAngles = new Vector3(0, 0, deg);
            }
            else if(sightLine.x < -0.1)
            {
                float deg = Mathf.Rad2Deg * Mathf.Atan(-sightLine.y / sightLine.x);
                transform.eulerAngles = new Vector3(0,0,0);
                shoulder.eulerAngles = new Vector3(0, 180, deg);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            isOnFire = true;

            
            fireEffect.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isOnFire = false;
            coolDownCount = coolDown;
            fireEffect.SetActive(false);
        }
        if (isOnFire)
        {
            ++coolDownCount;
            if (coolDownCount >= coolDown)
            {
                Instantiate(bullet, muzzle.position, muzzle.rotation);
                coolDownCount = 0;
            }
        }
        Debug.Log(coolDownCount);
    }
}

