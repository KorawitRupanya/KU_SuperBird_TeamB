using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class SuperBirdSceneManager : MonoBehaviour
    { 
        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public static IEnumerator LoadSceneAsync(string name, Action finishAction)
        {
            var async = SceneManager.LoadSceneAsync(name);

            while (!async.isDone)
            {
                yield return null;
            }

            finishAction?.Invoke();
        }
    }