using System.Collections;
using TMPro; // TextMeshPro를 사용하려면 추가해야 함
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas blink;
    [SerializeField] GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public TMP_InputField playerRepl; // TMP_InputField 사용
    public GameObject NPC;
    public static bool isAction;
    public static bool isListening = false;
    public TalkManager tm;
    public int id;
    public int talkIndex = 0;
    public bool isStop = false;
    [SerializeField] Image curtain1;
    [SerializeField] Image curtain2;
    [SerializeField] GameObject pauseMenu;

    void Awake()
    {
        curtain1.fillAmount = 0;
        curtain2.fillAmount = 0;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        blink.sortingOrder = 0;
        pauseMenu.SetActive(false);
        isAction = false;
        talkPanel.SetActive(false);
        playerRepl.gameObject.SetActive(false); // InputField 비활성화
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isStop = !isStop;
        }

        if (isStop)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void MainMenuButton()
    {
        Destroy(gameObject);
        tm.DestroyTalk();
        SceneManager.LoadScene("Start");
    }

    public void ResumeButton()
    {
        isStop = false;
    }

    public void QuitGameButton()
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
        NPCdata npcData = NPC.GetComponent<NPCdata>();
        id = npcData.id;

        PlayerController pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pc.Stopplayer();
        Talk();
        talkPanel.SetActive(isAction);
    }

    public void Talk()
    {
        string line = tm.GetLine(id, talkIndex);
        if (line == null)
        {
            isAction = false;
            talkPanel.SetActive(isAction);
            talkIndex = 0;
            StartCoroutine(Blink());
            return;
        }

        if (line == "[플레이어 대답]")
        {
            playerRepl.gameObject.SetActive(true);
            playerRepl.onEndEdit.AddListener(SubmitPlayerReply);
            isListening = true;
        }
        else
        {
            playerRepl.gameObject.SetActive(false); // InputField 비활성화
        }

        talkText.text = line;
        isAction = true;
        talkIndex++;
    }

    public void SubmitPlayerReply(string playerInput)
    {
        Debug.Log("Player Reply: " + playerInput);
        playerRepl.text = "";
        playerRepl.gameObject.SetActive(false);
        isListening = false;
        Talk();
    }
    IEnumerator Blink()
    {
        blink.sortingOrder = 5;
        while (curtain1.fillAmount <= 0.5f)
        {
            curtain1.fillAmount += Time.deltaTime * 0.3f;
            curtain2.fillAmount = curtain1.fillAmount;
            yield return null;
        }

        switch (id)
        {
            case 1:
                SceneManager.LoadScene("Love");
                break;
            case 2:
                SceneManager.LoadScene("Death");
                break;
            case 3:
                SceneManager.LoadScene("Ending");
                break;
        }

        yield return new WaitForSeconds(0.5f);

        while (curtain1.fillAmount > 0)
        {
            curtain1.fillAmount -= Time.deltaTime * 0.3f;
            curtain2.fillAmount = curtain1.fillAmount;
            yield return null;
        }
        blink.sortingOrder = 0;

        PlayerController pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pc.FindGM();
        yield return null;
    }
}