using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTriggert : MonoBehaviour
{
    [SerializeField] PlayerController pc;
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.isAction = true;
        pc.Conversationstart();
    }
}
