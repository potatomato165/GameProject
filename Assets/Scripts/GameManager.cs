using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject talkpanel;
    public TextMeshProUGUI talktext;
    public GameObject NPC;
    public bool isAction;
    public TalkManager tm;
    public int talkindex = 0;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        isAction = false;
    }
    public void Action(GameObject npc)
    {
        NPC = npc;
        NPCdata npcdata = NPC.GetComponent<NPCdata>();
        Talk(npcdata.id);
        talkpanel.SetActive(isAction);
    }
    public void Talk(int id)
    {
        string line = tm.GetLine(id, talkindex);
        if(line == null)
        {
            isAction = false;
            talkindex = 0;
            return;
        }
        talktext.text = line;

        isAction = true;
        talkindex++;
        
    }
}
