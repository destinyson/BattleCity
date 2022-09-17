using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreUI : MonoBehaviour
{
    public Text recordText;
    public Text levelText;
    public Text scoreText;

    public Text Enemy1_Num_Text;
    public Text Enemy1_Score_Text;
    public Text Enemy2_Num_Text;
    public Text Enemy2_Score_Text;
    public Text Enemy3_Num_Text;
    public Text Enemy3_Score_Text;
    public Text Enemy4_Num_Text;
    public Text Enemy4_Score_Text;
    public Text Total_Num_Text;

    private bool enemy1_count;
    private bool enemy2_count;
    private bool enemy3_count;
    private bool enemy4_count;
    private bool total_count;
    private int enemy1_count_num = 1;
    private int enemy2_count_num = 1;
    private int enemy3_count_num = 1;
    private int enemy4_count_num = 1;

    public AudioSource scoreAudio;

    private float scoreTimeVal;
    // Start is called before the first frame update
    void Start()
    {
        recordText.text = PlayerParameter.record.ToString();
        levelText.text = PlayerParameter.level.ToString();
        scoreText.text = PlayerParameter.playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreTimeVal >= 0.2f)
        {
            if (!enemy1_count)
            {
                if (PlayerManager.Instance.enemy1_num == 0)
                {
                    int num = 0;
                    int score = 0;
                    Enemy1_Num_Text.text = num.ToString();
                    Enemy1_Score_Text.text = score.ToString();
                    enemy1_count = true;
                }
                else
                {
                    Enemy1_Num_Text.text = enemy1_count_num.ToString();
                    int score = 100 * enemy1_count_num;
                    Enemy1_Score_Text.text = score.ToString();
                    scoreAudio.Play();
                    if (enemy1_count_num == PlayerManager.Instance.enemy1_num)
                        enemy1_count = true;
                    else
                        ++enemy1_count_num;
                }
            }
            else if (!enemy2_count)
            {
                if (PlayerManager.Instance.enemy2_num == 0)
                {
                    int num = 0;
                    int score = 0;
                    Enemy2_Num_Text.text = num.ToString();
                    Enemy2_Score_Text.text = score.ToString();
                    enemy2_count = true;
                }
                else
                {
                    Enemy2_Num_Text.text = enemy2_count_num.ToString();
                    int score = 200 * enemy2_count_num;
                    Enemy2_Score_Text.text = score.ToString();
                    scoreAudio.Play();
                    if (enemy2_count_num == PlayerManager.Instance.enemy2_num)
                        enemy2_count = true;
                    else
                        ++enemy2_count_num;
                }
            }
            else if (!enemy3_count)
            {
                if (PlayerManager.Instance.enemy3_num == 0)
                {
                    int num = 0;
                    int score = 0;
                    Enemy3_Num_Text.text = num.ToString();
                    Enemy3_Score_Text.text = score.ToString();
                    enemy3_count = true;
                }
                else
                {
                    Enemy3_Num_Text.text = enemy3_count_num.ToString();
                    int score = 300 * enemy3_count_num;
                    Enemy3_Score_Text.text = score.ToString();
                    scoreAudio.Play();
                    if (enemy3_count_num == PlayerManager.Instance.enemy3_num)
                        enemy3_count = true;
                    else
                        ++enemy3_count_num;
                }
            }
            else if (!enemy4_count)
            {
                if (PlayerManager.Instance.enemy4_num == 0)
                {
                    int num = 0;
                    int score = 0;
                    Enemy4_Num_Text.text = num.ToString();
                    Enemy4_Score_Text.text = score.ToString();
                    enemy4_count = true;
                }
                else
                {
                    Enemy4_Num_Text.text = enemy4_count_num.ToString();
                    int score = 400 * enemy4_count_num;
                    Enemy4_Score_Text.text = score.ToString();
                    scoreAudio.Play();
                    if (enemy4_count_num == PlayerManager.Instance.enemy4_num)
                        enemy4_count = true;
                    else
                        ++enemy4_count_num;
                }
            }
            else if (!total_count)
            {
                int total = PlayerManager.Instance.enemy1_num +
                            PlayerManager.Instance.enemy2_num +
                            PlayerManager.Instance.enemy3_num +
                            PlayerManager.Instance.enemy4_num;
                Total_Num_Text.text = total.ToString();
                total_count = true;
            }
            else
            {
                if (PlayerParameter.fail)
                    Invoke("GoToGameOver", 1.5f);
                else
                    Invoke("GoToPrepare", 1.5f);
            }
            scoreTimeVal = 0;
        }
        else
            scoreTimeVal += Time.deltaTime;
    }

    private void GoToGameOver()
    {
        SceneManager.LoadScene(4);
    }

    private void GoToPrepare()
    {
        ++PlayerParameter.level;
        SceneManager.LoadScene(1);
    }
}
