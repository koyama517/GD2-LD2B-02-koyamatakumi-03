using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Sword : MonoBehaviour
{
    GameObject player;

    Vector3 dis;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        dis = player.transform.position - transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = 
            new Vector3(player.transform.position.x - dis.x,
            player.transform.position.y,
            player.transform.position.z);
    }
}
