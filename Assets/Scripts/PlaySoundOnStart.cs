using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{

    void Start()
    {
        SoundManager.Instance.PlaySound();
    }

}
