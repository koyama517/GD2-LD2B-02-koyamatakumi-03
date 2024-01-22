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

    public GameObject bloodPrefab;
    GameObject blood;

    public GameObject player;

    public LayerMask targetLayer; // �^�[�Q�b�g�ƂȂ�I�u�W�F�N�g�̃��C���[
    public float detectionRange = 5f; // ���G�͈�

    public GameObject iPrefab;
    GameObject i;

    private int framesToWait = 20; // �ҋ@����t���[����
    private int currentFrame = 0; // ���݂̃t���[����

    public GameObject attackPrefab;
    GameObject attack;

    public Vector2 initialDirection = Vector2.right;
    Vector2 dir;
    Vector2 exitDirection;


    // Start is called before the first frame update
    void Start()
    {
        // ����������ݒ肷��
        SetInitialDirection();
        dir = initialDirection;
    }

    void SetInitialDirection()
    {
        // �����������x�N�g������p�x�ɕϊ�
        float angle = Mathf.Atan2(initialDirection.y, -initialDirection.x) * Mathf.Rad2Deg;

        // �I�u�W�F�N�g�����������ɉ�]������
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
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
            // �ҋ@�t���[�������o�߂�����s������
            if (currentFrame >= framesToWait)
            {
                PerformAction(); // �����ōs�����郁�\�b�h���Ăяo��
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
            float angleInRadians = Mathf.Atan2(exitDirection.y, exitDirection.x);

            //// ���W�A����x�ɕϊ�
            float angleInDegrees = Mathf.Rad2Deg * angleInRadians;

            if (blood == null)
            {
                blood = Instantiate(bloodPrefab,
                    new Vector3(transform.position.x, transform.position.y,
                    1), Quaternion.Euler(0, 0, angleInDegrees));
            }
            if (i != null)
            {
                Destroy(i);
            }
            Debug.Log(exitDirection);
            Destroy(gameObject);
        }
    }

    private void PerformAction()
    {
        // �����œ���̍s�������s����
        // ��: Debug.Log("����̃t���O�������Ă��琔�t���[����ɍs�����܂����I");
        if (attack == null)
        {
            if (dir.x == 1)
            {
                attack = Instantiate(attackPrefab,
                    new Vector3(transform.position.x - 2.25f, transform.position.y, 0),
                                Quaternion.identity);
            }
            else
            {
                attack = Instantiate(attackPrefab,
                    new Vector3(transform.position.x + 2.25f, transform.position.y, 0),
                                Quaternion.identity);
            }
        }




    }

    void DetectTarget()
    {
        // �O���Ɍ������č��G
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, detectionRange, targetLayer);

        isDiscovery = false;
        Debug.DrawLine(transform.position,transform.position + new Vector3(dir.x,dir.y,0) * detectionRange);
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
                // �����ŕ������擾�����
                exitDirection = CalculateExitDirection(collision.transform.position);
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
    private Vector2 CalculateExitDirection(Vector2 otherPosition)
    {
        // ���̃g���K�[�I�u�W�F�N�g���瑼�̃I�u�W�F�N�g�ւ̕������v�Z
        return (otherPosition - (Vector2)transform.position).normalized;
    }
}
