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
    private bool isPP = false;
    public Material mat;
    public Image img;

    public float speed = 10f;

    public float moveTime = 10f;
    private Vector3 pos;
    private NavMeshAgent nav;




    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();

    }

    void Start()
    {
        transform.localScale = Vector3.zero;
        pos = transform.localPosition;
        transform.DOScale(Vector3.one, 1f).OnComplete(() =>
        {
            AniPingPong(pos.z- 0.1f, pos.z+0.1f);
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

    public void SetMatFade()
    {
        if (mat != null)
        {
            mat.DOColor(Color.black, 1f);
        }
        if (img != null)
        {
            img.DOColor(Color.black, 1f);
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
    }
    private void ResetData()
    {
        DOTween.Kill(ID + name);
        transform.SetParent(GhostMgr.Single.playerTrans.GetChild(0));

    }

   // public Slider sld;
    /// <summary>
    /// hua
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.name == ID)
        {       
            transform.DOKill();
            transform.DOScale(Vector3.zero, 1f);
            if (mat != null)
            {
                mat.DOColor(Color.white, 1.5f);
            }
            if (img != null)
            {
                img.DOColor(Color.white, 1.5f);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {

        }
    }

    private void OnCollisionExit(Collision collision)
    {
       // sld.value = 0f;
       // ekeyTime = 0f;
    }
    void OnApplicationQuit()
    {
        mat.color = Color.white;
    }

}
