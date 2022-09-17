using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWall : MonoBehaviour
{
    public AudioSource hitAudio;

    private void PlayAudio()
    {
        hitAudio.Play();
    }
}
