using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManegar : MonoBehaviour
{
    public Transform[] points; // 線を結ぶポイントのTransform配列

    private LineRenderer lineRenderer;

    void Start()
    {
        // LineRendererコンポーネントを取得または追加
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // 線の幅や材質などの設定
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.blue;

        // 最初に線を描画
        DrawLine();
    }

    void Update()
    {
        // オブジェクトが移動した場合など、線を再描画
        DrawLine();
    }

    void DrawLine()
    {
        // 線のポイントを更新
        List<Vector3> linePoints = new List<Vector3>();
        foreach (Transform point in points)
        {
            linePoints.Add(point.position);
        }

        // LineRendererにポイントを設定して描画
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
}
