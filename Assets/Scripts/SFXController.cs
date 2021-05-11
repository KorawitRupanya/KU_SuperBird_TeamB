using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController Instance;
    [SerializeField] private AudioSource jump, fail, bonus, pass;
    private void Awake()
    {
        Instance = this;
    }
    public void PlayJump()
    {
        jump.Play();
    }
    public void PlayFail()
    {
        fail.Play();
    }
    public void PlayBonus()
    {
        bonus.Play();
    }
    public void PlayPass()
    {
        pass.Play();
    }
}
