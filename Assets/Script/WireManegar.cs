using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManegar : MonoBehaviour
{
    public Transform object1;
    Transform object2;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Attack2");
        if (obj != null)
        {
            object2 = obj.GetComponent<Transform>();

            if (object2 != null)
            {
                // オブジェクトの位置を取得して線を更新
                lineRenderer.SetPosition(0, object1.position);
                lineRenderer.SetPosition(1, object2.position);
            }
        }
    }
}
