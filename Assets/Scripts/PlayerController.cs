using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    void Start()
    {
        
    }
    private float input;
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if(input != 0)
        {
            animator.SetBool("isWalk", true);
            rb.velocity = new Vector3(input,0,0) * 5f;
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }
}
