using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [SerializeField] private int score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GamePlayManager.Instance.AddScore(score);
            SFXController.Instance.PlayBonus();
            GamePlayManager.Instance.DeleteFood(this);
        }
        
    }
}
