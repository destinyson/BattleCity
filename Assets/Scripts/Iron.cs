using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : MonoBehaviour
{
    public AudioSource hitAudio;

    private void PlayAudio()
    {
        hitAudio.Play();
    }
}
