using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrepareUI : MonoBehaviour
{
    public Text levelText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = PlayerParameter.level.ToString();

        if (PlayerParameter.chooseLevel)
        {
            if (Input.GetKeyDown(KeyCode.J))
                PlayerParameter.level = PlayerParameter.level == 1 ? 1 : PlayerParameter.level - 1;
            else if (Input.GetKeyDown(KeyCode.K))
                PlayerParameter.level = PlayerParameter.level == PlayerParameter.maxLevel ? PlayerParameter.maxLevel : PlayerParameter.level + 1;
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayerParameter.chooseLevel = false;
                Invoke("GoToGame", 0.5f);
            }
        }
        else
            Invoke("GoToGame", 1);
    }

    private void GoToGame()
    {
        SceneManager.LoadScene(2);
    }
}
