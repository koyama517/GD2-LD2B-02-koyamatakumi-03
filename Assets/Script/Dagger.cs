using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    Transform target;  // �^�[�Q�b�g��Transform
    public float speed = 5f;  // �ړ����x

    private Vector3 initialPosition;  // �����ʒu

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {

        target = GameObject.FindWithTag("Target").GetComponent<Transform>();

        // �^�[�Q�b�g�̕����Ɍ������Ĉړ�
        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        // �����ɂԂ������猳�̈ʒu�ɖ߂�
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
