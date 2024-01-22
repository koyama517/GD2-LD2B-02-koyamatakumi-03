using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serch : MonoBehaviour
{

    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (obj == null)
        {
            Destroy(gameObject);
        }
    }
}
