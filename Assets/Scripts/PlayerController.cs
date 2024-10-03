using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] GameManager gm;
    private GameObject npc;
    void Start()
    {

    }
    private float input;
    void Update()
    {
        input = Input.GetAxis("Horizontal");
        if(input != 0)
        {
            if(input > 0)
            {
                sr.flipX = false;
            }
            else if(input < 0)
            {
                sr.flipX = true;
            }
            animator.SetBool("isWalk", true);
            rb.velocity = new Vector3(input,0,0) * 5f;
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }


    private Vector2 dir;
    private void FixedUpdate() 
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            dir = new Vector2(Input.GetAxis("Horizontal"),0).normalized;
        }
        Debug.DrawRay(rb.position, dir, Color.green);
        RaycastHit2D rayhit = Physics2D.Raycast(rb.position, dir, 5f, LayerMask.GetMask("NPC"));
        if (rayhit.collider != null)
        {
            npc = rayhit.collider.gameObject;
        
            if(Input.GetKeyDown(KeyCode.P))
            {
                gm.Action(npc);
            }
        }
    }
}
