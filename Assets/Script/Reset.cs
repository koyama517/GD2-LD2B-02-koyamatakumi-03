using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public GameObject targetObject; // 消えるべきオブジェクト

    public float delayInSeconds = 3f; // シーンをロードするまでの遅延時間

    void Update()
    {
        if (targetObject == null) {
                // 遅延時間後にシーンをロード
                Invoke("LoadNextScene", delayInSeconds);
            }
        
    }

    void LoadNextScene()
    {
        // 現在のシーンの次のシーンをロード
        SceneManager.LoadScene(0);
    }
}
