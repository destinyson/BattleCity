using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HiScoreUI : MonoBehaviour
{
    public Text HiScoreText;
    public Text recordText;
    public float blingTimeVal;
    private bool colorIndex;

    public AudioSource hiAudio; 
    // Start is called before the first frame update
    void Start()
    {
        recordText.text = PlayerParameter.record.ToString();
        hiAudio.Play();
        Invoke("ReturnMenu", 8);
    }

    // Update is called once per frame
    void Update()
    {
        if (blingTimeVal >= 0.05f)
        {
            Color color = colorIndex ? Color.blue : Color.red;
            HiScoreText.color = color;
            recordText.color = color;
            colorIndex = !colorIndex;
            blingTimeVal = 0;
        }
        else
            blingTimeVal += Time.deltaTime;
    }

    private void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
