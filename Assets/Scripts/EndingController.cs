using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject trigger;
    private bool isEnd = false;

    void Start()
    {
        mainCamera.transform.position = new Vector3(0, 0, -10); // 기본 카메라 위치 설정 (Z축 값 조정)
    }

    void Update()
    {
        // 카메라가 아래로 계속 이동하는 부분
        if (!isEnd)
        {
            mainCamera.transform.position -= new Vector3(0, Time.deltaTime, 0);
        }
    }

    // 트리거에 들어갔을 때 발생하는 이벤트
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trigger")) // 트리거에 진입한 오브젝트가 플레이어인지 확인
        {
            isEnd = true;
            StartCoroutine(EndSequence()); // 종료 시퀀스 시작
        }
    }

    // 씬 전환을 위한 종료 시퀀스
    IEnumerator EndSequence()
    {
        // 여기서 몇 초간 지연을 줄 수 있습니다.
        yield return new WaitForSeconds(2.0f); // 2초 지연 후 씬 전환

        // 씬 전환
        SceneManager.LoadScene("Start");
    }
}