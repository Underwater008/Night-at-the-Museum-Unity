using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Slider sld;

    private float ekeyTime;

    public int targetIndex;
    private bool firstMove = false;

    private float timer;
    public bool isMove;
    private void Update()
    {
        //if (Input.GetKey(KeyCode.E))
        //{
        //    ekeyTime += Time.deltaTime;
        //    sld.value = ekeyTime / 2;
        //    if (ekeyTime >= 2f)
        //    {
        //        isMove = false;
        //        ResetData();
        //        ekeyTime = 0f;
        //    }
        //}
 

    }
    private bool CheckPoint(Vector3 targetPos)
    {
        var targetScreenPos = Camera.main.WorldToScreenPoint(targetPos);
        if (Vector2.Distance(Vector2.one*0.5f,targetScreenPos)<0.02f)
        {
            return true;
        }
        return false;
    }

    private RaycastHit hit;
    public LayerMask targetLayer;
    public bool Check()
    {
        var ray = Camera.main.ScreenPointToRay(Vector2.zero * 0.5f);
        if (Physics.Raycast(ray,out hit,100f,targetLayer))
        {
            return CheckPoint(hit.point);
        }
        return false;
    }



    void ResetData()
    {

    }
}
