using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaryController : MonoBehaviour
{
    public Animator animator;
    public AudioClip clip;

    private void Start()
    {
        GameManager.Instance.EndGameEvent.AddListener(SlapKid);
    }

    IEnumerator SlapRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("FatherSlap", true);
    }

    public void SlapKid()
    {
        StartCoroutine("SlapRoutine");
    }

    public void EndSlap()
    {
        animator.SetBool("FatherSlap", false);
    }
}
