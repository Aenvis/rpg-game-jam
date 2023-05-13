using UnityEngine;
using System.Collections;

public class StaryAnimationController : MonoBehaviour
{
    private Animator animator;
    public float minWaitTime = 3f; // Minimum time to wait before changing idle animation
    public float maxWaitTime = 12f; // Maximum time to wait before changing idle animation

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("IdleAnimationRoutine");
    }

    IEnumerator IdleAnimationRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

            // Randomly select one of the special idle animations
            int randomIdle = Random.Range(2, 4);

            if (randomIdle == 2)
            {
                animator.SetTrigger("Drinking");
            }
            else if (randomIdle == 3)
            {
                animator.SetTrigger("Stomach");
            }
        }
    }
}
