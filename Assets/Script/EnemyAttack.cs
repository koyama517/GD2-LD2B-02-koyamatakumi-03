using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public string targetTag = "YourTargetTag";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ���������I�u�W�F�N�g�̃^�O���Ώۂ̃^�O�ƈ�v����ꍇ
        if (other.CompareTag(targetTag))
        {
            Destroy(other.gameObject);
        }
    }

}
