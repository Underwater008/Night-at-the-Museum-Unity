using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.AI;
using DG.Tweening;
using System;

public class GhostMgr : MonoBehaviour
{
    private HashSet<Ghost> ghostList;

    public static GhostMgr Single;

    public Transform playerTrans;
    public Player pl;


    public Text timerTxt;
    public float totalTimer = 60f;
    public float curretTime;
    public float Value
    {
        get
        {
            return curretTime / totalTimer;
        }
    }

    private void Awake()
    {
        Single = this;
        curretTime = totalTimer;
        ghostList = transform.GetComponentsInChildren<Ghost>().ToHashSet();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        pl = playerTrans.GetComponent<Player>();
        foreach (var item in ghostList)
        {
            item.gameObject.GetComponentInChildren<Rigidbody>().useGravity = false;
        }
    }

    private void Update()
    {
        if (curretTime > 0f)
        {
            var info = Mathf.FloorToInt(curretTime);
            if (info==0)
            {
                foreach (var item in ghostList)
                {
                    item.gameObject.GetComponentInChildren<Rigidbody>().useGravity = true;
                    item.gameObject.GetComponent<NavMeshAgent>().enabled = true;
                    item.SetMatFade();
                    item.isMove = true;
                    DOTween.Kill(item.name);
                }           
            }
            timerTxt.text = info + "s";
            curretTime -= Time.deltaTime;
        }
    }
}
