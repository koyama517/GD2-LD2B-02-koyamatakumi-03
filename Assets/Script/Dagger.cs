using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    Transform target;  // ターゲットのTransform
    public float speed = 5f;  // 移動速度

    private Vector3 initialPosition;  // 初期位置

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {

        target = GameObject.FindWithTag("Target").GetComponent<Transform>();

        // ターゲットの方向に向かって移動
        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        // 何かにぶつかったら元の位置に戻る
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.1f);
        if (hit.collider != null)
        {
            ReturnToInitialPosition();
        }
    }

    void ReturnToInitialPosition()
    {
        transform.position = initialPosition;
    }
}
