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
        if(GameManager.isAction)
        {
            input = 0;
        }
        else
        {
            input = Input.GetAxis("Horizontal");
        }
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
        npc = GameObject.FindWithTag("NPC");
        if(Input.GetKeyDown(KeyCode.Space) && GameManager.isAction)
        {
            gm.Talk();
        }
    }
    public void Conversationstart()
    {
        gm.Action(npc);
    }
    public void FindGM()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
}
