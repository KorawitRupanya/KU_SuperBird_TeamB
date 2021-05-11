using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GamePlayManager.Instance.AddScore(1);
        }
    }
}
