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
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("FatherSlap", true);
        SoundManager.Instance.StopSound();
    }

    public void SlapKid()
    {
        StartCoroutine("SlapRoutine");
        SoundManager.Instance.PlaySound(clip);
    }

    public void EndSlap()
    {
        animator.SetBool("FatherSlap", false);
    }
}
