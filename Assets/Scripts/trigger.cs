using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public Camera mainCamera; // 주 카메라를 참조
    public float targetSize = 10f; // 목표 확대 크기
    public float zoomSpeed = 5f; // 확대 속도

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        StartCoroutine(ZoomOut());
    }
    IEnumerator ZoomOut()
    {
        while (mainCamera.orthographicSize < targetSize)
        {
            mainCamera.orthographicSize += zoomSpeed * Time.deltaTime;
            yield return null;
        }
    }
}

