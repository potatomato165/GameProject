using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Start()
    {
        transform.position = player.transform.position;
        transform.position = transform.position + new UnityEngine.Vector3(0,2,-10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.position = transform.position + new UnityEngine.Vector3(0,2,-10);
    }
}
