using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManegar : MonoBehaviour
{
    public GameObject cursorObject;
    
    public Target enemyDetection;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        Transform nearestEnemy = enemyDetection.GetNearestEnemy();

        if (nearestEnemy != null)
        {
            cursorObject.transform.position = new Vector3(nearestEnemy.position.x,nearestEnemy.position.y,-3);
            cursorObject.SetActive(true); // 敵がいる場合はカーソルを表示
        }
        else
        {
            cursorObject.SetActive(false); // 敵がいない場合はカーソルを非表示
        }
    }
}
