using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManegar : MonoBehaviour
{
    public Transform[] points; // �������ԃ|�C���g��Transform�z��

    private LineRenderer lineRenderer;

    void Start()
    {
        // LineRenderer�R���|�[�l���g���擾�܂��͒ǉ�
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // ���̕���ގ��Ȃǂ̐ݒ�
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.blue;

        // �ŏ��ɐ���`��
        DrawLine();
    }

    void Update()
    {
        // �I�u�W�F�N�g���ړ������ꍇ�ȂǁA�����ĕ`��
        DrawLine();
    }

    void DrawLine()
    {
        // ���̃|�C���g���X�V
        List<Vector3> linePoints = new List<Vector3>();
        foreach (Transform point in points)
        {
            linePoints.Add(point.position);
        }

        // LineRenderer�Ƀ|�C���g��ݒ肵�ĕ`��
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
}
