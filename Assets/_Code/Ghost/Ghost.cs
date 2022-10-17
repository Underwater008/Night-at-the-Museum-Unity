using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Ghost : MonoBehaviour, IGhost
{
    public Vector3 spawnPos;
    public float targetPosZ;
    private bool isPP = false;
    public Material mat;

    public float speed = 10f;

    public float moveTime = 10f;
    private Vector3 pos;
    [SerializeField]
    private List<Transform> targetList;
    private NavMeshAgent nav;

    private CharacterController cc;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        cc = GetComponent<CharacterController>();

    }

    void Start()
    {
        transform.localScale = Vector3.zero;
        pos = transform.localPosition;
        Spawn(spawnPos);

        transform.DOScale(Vector3.one, 1f).OnComplete(() =>
        {
            AniPingPong(transform.position.z, targetPosZ);
        });


    }
    private float timer;
    public bool isMove;

    private void AniPingPong(float from, float to)
    {
        transform.DOLocalMoveZ(to, 0.2f).OnComplete(() => AniPingPong(to, from)).SetId(transform.name);
    }

    public string ID
    {
        get
        {
            return transform.name;
        }
    }

    public void Escape(Action escapeFunc)
    {

    }

    public bool Move(Vector3 targetPos)
    {
        //transform.LookAt(targetPos);
        transform.DOMove(targetPos, moveTime).SetId(ID + name);
            ;
       // transform.Translate(targetPos * speed * Time.deltaTime);
       // transform.localPosition = (targetPos - transform.localPosition).normalized * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, targetPos) <= 0.3f)
        {
            return true;
        }
        return false;
    }

    public bool CheckGhos(Transform Targer, float fieldOfViewDistance = 100f, float fieldOfViewAngle = 105f)
    {

        float dis = Vector3.Distance(transform.position, Targer.position);//求得距离
        float angle = Vector3.Angle(transform.forward, Targer.position - transform.position);//求得夹角
        if (dis < fieldOfViewDistance && angle < fieldOfViewAngle * 0.5)
        {
            return true;
        }
        return false;
    }

    public void SetMatFade()
    {
        if (mat != null)
        {
            mat.DOColor(Color.black, 1f);
        }
    }

    private float ekeyTime;

    public int targetIndex;
    private bool firstMove = false;
    void Update()
    {
        if (!isMove)
        {
            return;
        }
        timer += Time.deltaTime;
        if (GhostMgr.Single.pl.CheckGhos(transform) && timer >= 2f)
        {
            // Move();
        }
        if (targetList != null && targetList.Count > 0)
        {
            if (Move(targetList[targetIndex]))
            {
                
                targetIndex++;
                targetIndex = targetIndex % targetList.Count;
                if (targetIndex==0)
                {
                    targetIndex = 1;
                }
            }
            else
            {
                Move(targetList[targetIndex]);
            }
        }
        else
        {
            Debug.LogWarning("Null");
        }


    }
    private void ResetData()
    {
        DOTween.Kill(ID + name);
        transform.SetParent(GhostMgr.Single.playerTrans.GetChild(0));

    }

    public Slider sld;
    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.tag == "Bullet" && isMove)
        {
            moveTime += 3f;
            
        }
        if (collision.gameObject.name==ID)
        {
            transform.DOKill();
            isMove = false;
            transform.DOMove(pos, 1f).OnComplete(() =>
            {
                transform.DOScale(Vector3.zero, 1f);
                mat.DOColor(Color.white, 1.5f);
            });
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {

            if (Input.GetKey(KeyCode.E))
            {
                if (moveTime >= 10f&&isMove)
                {
                    ekeyTime += Time.deltaTime;
                    sld.value = ekeyTime / 2;                   
                    print(ekeyTime);
                   
                    if (ekeyTime >= 2f)
                    {
                        isMove = false;
                        ResetData();
                        ekeyTime = 0f;
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        sld.value = 0f;
        ekeyTime = 0f;
    }
    public void Move()
    {
        //transform.LookAt(GhostMgr.Single.playerTrans);
        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, -GhostMgr.Single.playerTrans.forward * Random.Range(5, 12f), speedV);
        //transform.DOMove(-GhostMgr.Single.playerTrans.forward * Random.Range(7, 14f), moveTime).SetEase(Ease.OutBounce);
        //timer = 0f;
    }
    void OnApplicationQuit()
    {
        mat.color = Color.white;
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = spawnPos;
    }

    public bool Move(Transform targetTrans)
    {
        return Move(targetTrans.localPosition);
    }

    public void SetTargetData(List<Transform> trans)
    {
        targetList = trans;
    }




}
