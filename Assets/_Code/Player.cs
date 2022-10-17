using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool CheckGhos(Transform Targer, float fieldOfViewDistance = 100f, float fieldOfViewAngle = 60f)
    {

        float dis = Vector3.Distance(transform.position, Targer.position);//求得距离
        float angle = Vector3.Angle(transform.forward, Targer.position - transform.position);//求得夹角
        if (dis < fieldOfViewDistance && angle < fieldOfViewAngle * 0.5)
        {
            return true;
        }
        return false;
    }
    private void Update()
    {
        
    }

}
