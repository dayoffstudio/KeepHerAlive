using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Transform shoulder;
    public Transform muzzle;
    public GameObject bullet;
    public float bullectVelocity = 200;
    private Vector3 mousePosition = new Vector3();
    private Vector3 sightLine = new Vector3();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            Instantiate(bullet,muzzle.position,muzzle.rotation);
        }
    }
}
