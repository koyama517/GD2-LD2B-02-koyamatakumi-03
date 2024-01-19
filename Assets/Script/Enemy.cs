using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    bool isHit;
    bool isDeth;
    bool isDiscovery;
    bool isLeft;

    Vector3 killDir;
    public GameObject bloodPrefab;
    public GameObject bloodPrefab2;
    GameObject blood;

    public GameObject player;

    public LayerMask targetLayer; // ターゲットとなるオブジェクトのレイヤー
    public float detectionRange = 5f; // 索敵範囲

    public GameObject iPrefab;
    GameObject i;

    private int framesToWait = 20; // 待機するフレーム数
    private int currentFrame = 0; // 現在のフレーム数

    public GameObject attackPrefab;
    GameObject attack;

    public Vector2 initialDirection = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        // 初期方向を設定する
        SetInitialDirection();
    }

    void SetInitialDirection()
    {
        // 初期方向をベクトルから角度に変換
        float angle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg;

        // オブジェクトを初期方向に回転させる
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Update is called once per frame
    void Update()
    {
        if (isDiscovery)
        {
            if (i == null)
            {
                i = Instantiate(iPrefab,
                    new Vector3(transform.position.x,
                    transform.position.y + 1.5f,
                    transform.position.z),
                    Quaternion.identity
                    );
            }
            // 待機フレーム数を経過したら行動する
            if (currentFrame >= framesToWait)
            {
                PerformAction(); // ここで行動するメソッドを呼び出す
            }
            else
            {
                currentFrame++;
            }
        }
        else
        {
            currentFrame = 0;
            if (attack != null)
            {
                Destroy(attack);
            }
            if (i != null)
            {
                Destroy(i);
            }
        }
        DetectTarget();

        if (isDeth)
        {

            if (player != null)
            {
                if (player.transform.position.x > transform.position.x)
                {
                    isLeft = true;
                }
                else
                {
                    isLeft = false;
                }
            }

            if (blood == null && !isLeft)
            {
                blood = Instantiate(bloodPrefab,
                    new Vector3(transform.position.x + 1.5f, transform.position.y,
                    1), Quaternion.identity);
            }

            else if (blood == null && isLeft)
            {
                blood = Instantiate(bloodPrefab2,
                    new Vector3(transform.position.x - 1.5f, transform.position.y,
                   1), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private void PerformAction()
    {
        // ここで特定の行動を実行する
        // 例: Debug.Log("特定のフラグが立ってから数フレーム後に行動しました！");
        if (attack == null)
        {
            if (isLeft)
            {
                attack = Instantiate(attackPrefab,
                    new Vector3(transform.position.x + 2.25f, transform.position.y, 0),
                                Quaternion.identity);
            }
            else
            {
                attack = Instantiate(attackPrefab,
                    new Vector3(transform.position.x - 2.25f, transform.position.y, 0),
                                Quaternion.identity);
            }
        }




    }

    void DetectTarget()
    {
        // 前方に向かって索敵
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, detectionRange, targetLayer);

        isDiscovery = false;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (player != null)
                {
                    PlayerMove playerMove = player.GetComponent<PlayerMove>();
                    if (!playerMove.isInvisible)
                    {
                        isDiscovery = true;
                    }
                    else
                    {
                        isDiscovery = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Attack1")
        {
            isDeth = true;
        }

        if (collision.gameObject.tag == "Attack2")
        {
            isHit = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack2")
        {
            if (isHit)
            {
                isDeth = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isDiscovery = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isDiscovery = false;
        }
    }
}
