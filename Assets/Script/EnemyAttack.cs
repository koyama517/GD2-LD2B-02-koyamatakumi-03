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
        // 当たったオブジェクトのタグが対象のタグと一致する場合
        if (other.CompareTag(targetTag))
        {
            Destroy(other.gameObject);
        }
    }

}
