using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public float moveDistance = 5f;  // �ړ����鋗��
    public float moveSpeed = 5f;     // �ړ����x

    public bool isMoving = false;   // �ړ������ǂ����̃t���O
    private Vector3 targetPosition;  // �ړ���̈ʒu
    bool isGrounded;

    public GameObject swordPrefab;
    GameObject sword;

    void Update()
    {
        if (isMoving)
        {
            // �C�[�W���O�ňړ�����
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // �ړ�������������ړ��t���O������
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
                        // �ړ���̈ʒu���v�Z���Ĉړ��t���O��ݒ�
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
        // �ړ��������擾���āA���̕����Ɏw�肳�ꂽ���������f�����ړ�����
        Vector3 moveDirection = transform.right; // ��: �L�����N�^�[�̑O�����Ɉړ�
        transform.Translate(moveDirection * moveSpeed * distance * Time.deltaTime, Space.World);
    }
}
