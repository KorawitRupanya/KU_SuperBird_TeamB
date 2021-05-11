using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretEndingReveal : MonoBehaviour
{
    float elapsed = 0f;
    public void LoadSceneCoroutine(string name, Action action)
    {
        StartCoroutine(SuperBirdSceneManager.LoadSceneAsync(name,
            () =>
            {
                action?.Invoke();
                StopAllCoroutines();
            }
        ));
    }

    private IEnumerator SecretSceneLoad()
    {
        yield return new WaitForSeconds(2);
        LoadSceneCoroutine("SecretEnding",null);
    }
    
    private void OutputTime() {
        Debug.Log("Seconds"+Time.time);
    }

    private void Start()
    {
        StartCoroutine(SecretSceneLoad());
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            OutputTime();
        }
        SecretSceneLoad();
    }
}