using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
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

    private Dictionary<string, List<Transform>> targetDic = new Dictionary<string, List<Transform>>();
    public Transform targetRootTrans;

    public Func<bool> check;
    public float Value
    {
        get
        {
          //  print(curretTime / totalTimer);
            return curretTime / totalTimer;
        }
    }

    void InitData()
    {
        List<Transform> list;
        foreach (Transform item in targetRootTrans)
        {
            list = new List<Transform>();
            foreach (Transform t in item)
            {
                list.Add(t);
            }
            targetDic.Add(item.name, list);
        }
    }

    private void Awake()
    {
        Single = this;
        curretTime = totalTimer;
        InitData();
        ghostList = transform.GetComponentsInChildren<Ghost>().ToHashSet();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        pl = playerTrans.GetComponent<Player>();
       // StartCoroutine(Timer());
        foreach (var item in ghostList)
        {
            //print(item.ID);
            item.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

    }
    IEnumerator DelayPos(Ghost ghost)
    {
        yield return new WaitForSeconds(0.5f);
   
        ghost.Move();
    }

    private void Update()
    {
        if (curretTime > 0f)
        {
            var info = Mathf.FloorToInt(curretTime);
            if (info==0)
            {
                // StartCoroutine(Timer());
                foreach (var item in ghostList)
                {
                    item.gameObject.GetComponent<Rigidbody>().useGravity = true;                
                    item.SetMatFade();
                    item.isMove = true;
                    DOTween.Kill(item.name);
                    if (targetDic.ContainsKey(item.name))
                    {
                        item.SetTargetData(targetDic[item.name]);
                    }
                    else
                    {
                        Debug.LogError("no this kry:" + item.name);
                        return;
                    }
                }
              
            }
            timerTxt.text = info + "s";
            curretTime -= Time.deltaTime;
        }

    }
}
