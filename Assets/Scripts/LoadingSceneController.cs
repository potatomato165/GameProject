using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TextMeshProUGUI loadProcessText;
    private static string nextScene;
    
    private float remainLoadTime = 5.0f;  // 인위적인 로딩 시간
    private float timer = 0f;

    // 씬을 로드하는 static 메서드
    public static void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is null or empty!");
            return;
        }

        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // 로딩이 시작될 때 실행
    void Start()
    {
        if (string.IsNullOrEmpty(nextScene))
        {
            Debug.LogError("Next scene is not set. Make sure to call LoadScene with a valid scene name.");
            return;
        }

        StartCoroutine(LoadSceneProcess());
    }

    // 실제 씬 로딩을 처리하는 코루틴
    IEnumerator LoadSceneProcess()
    {
        // 마지막 씬 언로드 대신 씬을 그대로 전환
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);

        if (op == null)
        {
            Debug.LogError("Failed to load scene: " + nextScene);
            yield break;
        }

        op.allowSceneActivation = false;  // 씬 자동 활성화 비활성화
        timer = 0f;

        // 씬 로딩 중 처리
        while (!op.isDone)
        {
            // op.progress는 0.9까지 증가
            if (op.progress < 0.9f)
            {
                loadingBar.value = op.progress;
            }
            else
            {
                // 로딩이 90%에 도달하면 나머지 10%는 인위적으로 처리
                timer += Time.deltaTime;
                loadingBar.value = Mathf.Lerp(0.9f, 1f, timer / remainLoadTime);

                // 로딩바가 100%에 도달한 경우 씬 활성화
                if (loadingBar.value >= 1f)
                {
                    loadProcessText.text = "100%";
                    yield return new WaitForSeconds(0.5f); // 잠깐의 대기시간
                    op.allowSceneActivation = true;  // 씬 전환 허용
                    yield break;  // 코루틴 종료
                }
            }

            // 로딩 퍼센트 업데이트
            loadProcessText.text = Mathf.RoundToInt(loadingBar.value * 100) + "%";

            yield return null;  // 다음 프레임 대기
        }
    }
}