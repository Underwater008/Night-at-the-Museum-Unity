using UnityEngine;
using System;
using System.Collections.Generic;

public interface IGhost 
{
    string ID { get; }

    void Spawn(Vector3 pos);
    void Escape(Action escapeFunc);
    void Move();
    bool Move(Vector3 targetPos);
    bool Move(Transform targetTrans);
    void SetTargetData(List<Transform> trans);
}
