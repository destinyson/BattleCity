using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int enemy = 20;
    public bool isDead;
    public int enemyBornNum = 0;
    public List<GameObject> enemyAlive;
    public bool hasBonus;
    public bool createBonus;

    public bool showScore;
    public Vector3 diePos;
    public int displayScore;

    public bool freeze;
    public float freezeTimeVal;

    public bool heartDefense;
    public float heartDefenseTimeVal;

    public bool isDefended;
    public float defendTimeVal;

    public GameObject born;
    public GameObject failUI;
    public GameObject GameOverIMG;
    public float stopHeight;
    public float GameOverMoveSpeed;

    public GameObject[] EnemyIcon;
    public Text playerLife;
    public Text levelText;

    public int enemy1_num;
    public int enemy2_num;
    public int enemy3_num;
    public int enemy4_num;

    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; ++i)
            EnemyIcon[i].SetActive(true);
        levelText.text = PlayerParameter.level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerParameter.fail)
        {
            if (GameOverIMG.transform.position.y < stopHeight)
                GameOverIMG.transform.Translate(GameOverIMG.transform.up * GameOverMoveSpeed * Time.deltaTime, Space.World);
            else
                Invoke("GoToScore", 3);
        }
        if (enemy == 0)
            Invoke("GoToScore", 3);
        if (isDead)
            Recover();
        playerLife.text = PlayerParameter.life.ToString();
    }

    private void Recover()
    {
        PlayerParameter.tankLevel = 0;
        if (PlayerParameter.life <= 0)
            PlayerParameter.fail = true;
        else
        {
            --PlayerParameter.life;
            GameObject player = Instantiate(born, new Vector3(-2.76f, -9.2f, 0), Quaternion.identity);
            player.GetComponent<Born>().isBornPlayer = true;
            isDead = false;
        }
    }

    private void GoToScore()
    {
        SceneManager.LoadScene(3);
    }
}
