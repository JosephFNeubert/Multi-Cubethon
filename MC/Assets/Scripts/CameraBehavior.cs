using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset.y = 1;
        offset.z = -5;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.position + offset;
    }
}
