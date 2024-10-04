using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public Dictionary<int, string[]>  Number;
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        Number = new Dictionary<int, string[]>();
        GenerateData();
    }
    void GenerateData()
    {
        Number.Add(1, new string[] {"아", "졸려", "배고파", "나 돌아갈래"} );
    }
    public string GetLine(int id, int lineindex)
    {
        if (lineindex == Number[id].Length)
        {
            return null;
        }
        else
        {
            return Number[id][lineindex];
        }
    }
}
