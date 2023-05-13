using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaryController : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        GameManager.Instance.EndGameEvent.AddListener(SlapKid);
    }

    public void SlapKid()
    {
        animator.SetBool("Slap", true);
    }
}
