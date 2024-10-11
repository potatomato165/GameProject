using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject talkpanel;
    public TextMeshProUGUI talktext;
    public GameObject NPC;
    public static bool isAction;
    public TalkManager tm;
    public int id;
    public int talkindex = 0;
    public bool isStop = false;
    [SerializeField] Image cutton1;
    [SerializeField] Image cutton2;
    [SerializeField] GameObject pausemenu;
    void Awake()
    {
        cutton1.fillAmount = 0;
        cutton2.fillAmount = 0;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        pausemenu.SetActive(false);
        isAction = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isStop = !isStop;
        }
        if(isStop)
        {
            Time.timeScale = 0f;
            pausemenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausemenu.SetActive(false);
        }
    }
    public void Mainmenubutton()
    {
        SceneManager.LoadScene("Start");
    }
    public void Resumebutton()
    {
        isStop = false;
    }
    public void Quitgamebutton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void Action(GameObject npc)
    {
        NPC = npc;
        NPCdata npcdata = NPC.GetComponent<NPCdata>();
        id = npcdata.id;
        Talk();
        talkpanel.SetActive(isAction);
    }
    public void Talk()
    {
        string line = tm.GetLine(id, talkindex);
        if(line == null)
        {
            isAction = false;
            talkpanel.SetActive(isAction);
            talkindex = 0;
            StartCoroutine(Blink());
            return;
        }
        talktext.text = line;

        isAction = true;
        talkindex++;
        
    }
    IEnumerator Blink()
    {
        while (cutton1.fillAmount <= 0.5f)
        {
            cutton1.fillAmount += Time.deltaTime * 0.3f;
            cutton2.fillAmount = cutton1.fillAmount;
            yield return null;
        }
        Destroy(NPC);
        switch(id)
        {
            case 1:
                SceneManager.LoadScene("Love");
                break;
            case 2:
                SceneManager.LoadScene("Death");
                break;
            case 3:
                break;
        }
        yield return new WaitForSeconds(0.5f);
        while (cutton1.fillAmount > 0)
        {
            cutton1.fillAmount -= Time.deltaTime * 0.3f;
            cutton2.fillAmount = cutton1.fillAmount;
            yield return null;
        }
        PlayerController pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pc.FindGM();
        yield return null;
    }
}
