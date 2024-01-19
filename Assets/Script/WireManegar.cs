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
                // �I�u�W�F�N�g�̈ʒu���擾���Đ����X�V
                lineRenderer.SetPosition(0, object1.position);
                lineRenderer.SetPosition(1, object2.position);
            }
        }
    }
}
