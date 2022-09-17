using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerParameter.playerScore > PlayerParameter.record)
        {
            PlayerParameter.record = PlayerParameter.playerScore;
            Invoke("GoToHiScore", 1.8f);
        }
        else
            Invoke("ReturnMenu", 1.8f);
    }

    private void GoToHiScore()
    {
        SceneManager.LoadScene(5);
    }

    private void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
