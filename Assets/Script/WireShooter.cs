using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WireShooter : MonoBehaviour
{
    public GameObject wirePrefab; // ワイヤーのプレハブ
    GameObject wire;
    public GameObject Object;

    Vector3 dir;

    bool isShoot;

    private LineRenderer lineRenderer;

    void Start()
    {
        // LineRendererコンポーネントを取得または追加
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // 線の幅や色などを設定
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material.color = Color.red;
    }

    void Update()
    {

        if (wire == null)
        {
            isShoot = false;
            lineRenderer.material.color = Color.clear;
        }
        else
        {
            wire.GetComponent<Wire>().SetPlayerPos(transform);
            lineRenderer.material.color = Color.red;
            // 線の始点と終点を設定
            lineRenderer.SetPosition(0, new Vector3( transform.position.x, transform.position.y - 0.3f, -2));
            lineRenderer.SetPosition(1, new Vector3(wire.transform.position.x, wire.transform.position.y, 2));
        }
    }

    void ShootWire()
    {
        if (wire == null)
        {
            // ワイヤーのインスタンスを生成
            wire = Instantiate(wirePrefab, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);

            // ワイヤーにターゲットを設定
            wire.GetComponent<Wire>().SetTarget(dir);
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            if (!isShoot)
            {
                dir = Object.transform.position - transform.position;
                ShootWire();
                isShoot = true;
            }
        }

    }


}
