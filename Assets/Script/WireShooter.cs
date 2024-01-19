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

    void Update()
    {

        if (wire == null)
        {
            isShoot = false;
        }
        else
        {
            wire.GetComponent<Wire>().SetPlayerPos(transform);

        }
    }

    void ShootWire()
    {
        if (wire == null)
        {
            // ���C���[�̃C���X�^���X�𐶐�
            wire = Instantiate(wirePrefab, transform.position, Quaternion.identity);

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
