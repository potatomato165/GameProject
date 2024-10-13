using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class TalkManager : MonoBehaviour
{
    public Dictionary<int, List<string>> Number; // 동적으로 줄을 추가할 수 있는 List<string> 사용

    void ReadTxt(int id, string filePath)
    {
        filePath = Path.Combine(Application.streamingAssetsPath, filePath);
        FileInfo fileInfo = new FileInfo(filePath);
        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            string value = reader.ReadLine();

            while (value != null)
            {
                // 해당 ID에 대한 리스트에 각 줄 추가
                if (!Number.ContainsKey(id))
                {
                    Number[id] = new List<string>(); // 키가 없을 경우 리스트 초기화
                }
                Number[id].Add(value);
                
                // 다음 줄 읽기
                value = reader.ReadLine();
            }
            reader.Close();
        }
        else
        {
            Debug.LogError("파일을 찾을 수 없습니다: " + filePath);
        }
    }

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        Number = new Dictionary<int, List<string>>();
        GenerateData();
    }

    void GenerateData()
    {
        ReadTxt(1, "./Scene1.txt");
        ReadTxt(2, "./Scene2.txt");
        ReadTxt(3, "./Scene3.txt");
    }

    public string GetLine(int id, int lineIndex)
    {
        if (Number.ContainsKey(id) && lineIndex < Number[id].Count)
        {
            return Number[id][lineIndex]; // 인덱스가 유효할 경우 해당 줄 반환
        }
        return null; // 인덱스가 범위를 벗어나면 null 반환
    }
    public void DestroyTalk()
    {
        Destroy(gameObject);
    }
}