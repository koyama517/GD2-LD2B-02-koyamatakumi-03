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

    public LayerMask targetLayer; // �^�[�Q�b�g�ƂȂ�I�u�W�F�N�g�̃��C���[
    public float detectionRange = 5f; // ���G�͈�

    public GameObject iPrefab;
    GameObject i;

    private int framesToWait = 20; // �ҋ@����t���[����
    private int currentFrame = 0; // ���݂̃t���[����

    public GameObject attackPrefab;
    GameObject attack;

    public Vector2 initialDirection = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        // ����������ݒ肷��
        SetInitialDirection();
    }

    void SetInitialDirection()
    {
        // �����������x�N�g������p�x�ɕϊ�
        float angle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg;

        // �I�u�W�F�N�g�����������ɉ�]������
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
        // �����œ���̍s�������s����
        // ��: Debug.Log("����̃t���O�������Ă��琔�t���[����ɍs�����܂����I");
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
        // �O���Ɍ������č��G
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
