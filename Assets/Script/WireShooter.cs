using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WireShooter : MonoBehaviour
{
    public GameObject wirePrefab; // ���C���[�̃v���n�u
    GameObject wire;
    public GameObject Object;

    Vector3 dir;

    bool isShoot;

    private LineRenderer lineRenderer;

    void Start()
    {
        // LineRenderer�R���|�[�l���g���擾�܂��͒ǉ�
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // ���̕���F�Ȃǂ�ݒ�
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
            // ���̎n�_�ƏI�_��ݒ�
            lineRenderer.SetPosition(0, new Vector3( transform.position.x, transform.position.y - 0.3f, -2));
            lineRenderer.SetPosition(1, new Vector3(wire.transform.position.x, wire.transform.position.y, 2));
        }
    }

    void ShootWire()
    {
        if (wire == null)
        {
            // ���C���[�̃C���X�^���X�𐶐�
            wire = Instantiate(wirePrefab, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);

            // ���C���[�Ƀ^�[�Q�b�g��ݒ�
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
