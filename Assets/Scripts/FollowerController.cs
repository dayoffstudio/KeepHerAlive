using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player ownerPlayer = pet.OwnerPlayer;
        GameObject target = GetTarget(ownerPlayer);
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            PetInfo petInfo = GetPetInfo(pet.ID);
            var dir = target.transform.position - transform.position;
            int skillRange = petInfo.skillRange;
            if (skillRange < distance) //  当和主人之间的距离超过技能距离时跟随
            {
                Action.ChangeAction(CAction.RunFront);// 改变动画
                var destPos = transform.position + dir.normalized * (distance - skillRange + 1);
                transform.position = Vector3.MoveTowards(transform.position, destPos, Time.deltaTime * move_Speed);
            }
            var rotation = Quaternion.LookRotation(dir); //  获得 目标方向
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);  // 差值  宠物朝向趋向目标
            return;
        }
    }
}
