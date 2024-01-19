using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public float moveDistance = 5f;  // 移動する距離
    public float moveSpeed = 5f;     // 移動速度

    public bool isMoving = false;   // 移動中かどうかのフラグ
    private Vector3 targetPosition;  // 移動先の位置
    bool isGrounded;

    public GameObject swordPrefab;
    GameObject sword;

    void Update()
    {
        if (isMoving)
        {
            // イージングで移動する
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 移動が完了したら移動フラグを解除
            if (Vector3.Distance(transform.position, targetPosition) < 1f)
            {
                isMoving = false;
                if(sword != null)
                {
                    Destroy(sword);
                }
            }
        }
    }
    public void Dash(InputAction.CallbackContext context)
    {

        if (isGrounded)
        {
            if (context.performed && !isMoving)
            {
                PlayerMove playerMove = GetComponent<PlayerMove>();
                if (playerMove != null)
                {
                    bool isRight = playerMove.isRight;

                    if (isRight)
                    {
                        // 移動先の位置を計算して移動フラグを設定
                        targetPosition = transform.position + new Vector3(moveDistance, 0, 0);
                        if (sword == null)
                        {
                            sword = Instantiate(swordPrefab,
                                new Vector3(transform.position.x + 1.5f, transform.position.y, 0),
                                Quaternion.identity);
                        }
                    }
                    else
                    {
                        targetPosition = transform.position + new Vector3(-moveDistance, 0, 0);
                        if (sword == null)
                        {
                            sword = Instantiate(swordPrefab,
                                new Vector3(transform.position.x - 1.5f, transform.position.y, 0),
                                Quaternion.identity);
                        }
                    }
                }
                isMoving = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void QuickMoveDistance(float distance)
    {
        // 移動方向を取得して、その方向に指定された距離だけ素早く移動する
        Vector3 moveDirection = transform.right; // 例: キャラクターの前方向に移動
        transform.Translate(moveDirection * moveSpeed * distance * Time.deltaTime, Space.World);
    }
}
