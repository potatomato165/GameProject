using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject talkpanel;
    public TextMeshProUGUI talktext;
    public GameObject NPC;
    public bool isAction;
    void Start()
    {
        isAction = false;
    }
    public void Action(GameObject npc)
    {
        if(isAction)
        {
            NPC = null;
            isAction = false;
        }
        else
        {
            isAction = true;
            NPC = npc;
            talktext.text = NPC.name;
        }

        talkpanel.SetActive(isAction);
    }
}
