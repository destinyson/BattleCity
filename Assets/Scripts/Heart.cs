using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;

    public Sprite brokenSprite;
    public GameObject explodePrefab;
    public AudioClip dieAudio;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Die()
    {
        Instantiate(explodePrefab, transform.position, transform.rotation);
        sr.sprite = brokenSprite;
        PlayerParameter.fail = true;
        PlayerParameter.tankLevel = 0;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
