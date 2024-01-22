using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public float speed = 10f; // ワイヤーの速度
    private Vector3 direction; // ターゲットのTransform
    Transform start;

    bool isReturn;

    private void Start()
    {
        isReturn = false;
    }

    public void SetTarget(Vector3 newTarget)
    {
        direction = newTarget;
    }

    public void SetPlayerPos(Transform newTransform)
    {
        start =  newTransform;
    }

    void Update()
    {
        if (!isReturn)
        {
            if (direction != null)
            {
                // ターゲットの方向に向かって移動
               
                transform.Translate(direction.normalized * speed * Time.deltaTime);
            }

            float dis = Vector3.Distance(transform.position, start.position);
            if (dis > 5)
            {
                Debug.Log(dis);
                isReturn = true;
            }

        }
        else
        {
            direction = start.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Blood")
        {
            isReturn = true;
        }
        else if(collision.gameObject.tag == "Player")
        {
            if (isReturn)
            {
                Destroy(gameObject);
            }
        }
    }

}
