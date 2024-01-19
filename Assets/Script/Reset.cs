using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public GameObject targetObject; // ������ׂ��I�u�W�F�N�g

    public float delayInSeconds = 3f; // �V�[�������[�h����܂ł̒x������

    void Update()
    {
        if (targetObject == null) {
                // �x�����Ԍ�ɃV�[�������[�h
                Invoke("LoadNextScene", delayInSeconds);
            }
        
    }

    void LoadNextScene()
    {
        // ���݂̃V�[���̎��̃V�[�������[�h
        SceneManager.LoadScene(0);
    }
}
