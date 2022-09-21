using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    public GameObject[] item;
    public GameObject bonus;
    public Text scoreText;

    public List<GameObject> wallList = new List<GameObject>();
    private bool ironWall;

    public float enemyBornTimeVal = 0;

    public GameObject[] level_map;

    private void Awake()
    {
        InitMap(PlayerParameter.level);
    }

    private void Update()
    {
        if (PlayerManager.Instance.createBonus)
        {
            int i = Random.Range(0, 13);
            int j = Random.Range(0, 13);
            if (!(i == 12 && j == 6))
            {
                PlayerManager.Instance.hasBonus = true;
                CreateItem(bonus, new Vector3(1.51f * j - 8.8f, -1.51f * i + 8.92f, 0), Quaternion.identity); ;
            }
            PlayerManager.Instance.createBonus = false;
            PlayerManager.Instance.hasBonus = true;
        }

        if (enemyBornTimeVal >= 5)
        {
            if (PlayerManager.Instance.enemyBornNum < 20 && PlayerManager.Instance.enemyAlive.Count < 4)
            {
                Invoke("CreateEnemy", 0.5f);
                enemyBornTimeVal = 0;
            }
        }
        else
            enemyBornTimeVal += Time.deltaTime;

        if (PlayerManager.Instance.heartDefense)
        {
            if (PlayerManager.Instance.heartDefenseTimeVal > 30)
            {
                for (int i = wallList.Count - 1; i >= 0; --i)
                {
                    Destroy(wallList[i]);
                    wallList.RemoveAt(i);
                }
                GameObject newWall1 = Instantiate(item[1], new Vector3(-1.25f, -7.69f, 0), Quaternion.identity);
                newWall1.transform.SetParent(gameObject.transform);
                wallList.Add(newWall1);
                GameObject newWall2 = Instantiate(item[1], new Vector3(0.26f, -7.69f, 0), Quaternion.identity);
                newWall2.transform.SetParent(gameObject.transform);
                wallList.Add(newWall2);
                GameObject newWall3 = Instantiate(item[1], new Vector3(1.77f, -7.69f, 0), Quaternion.identity);
                newWall3.transform.SetParent(gameObject.transform);
                wallList.Add(newWall3);
                GameObject newWall4 = Instantiate(item[1], new Vector3(-1.25f, -9.2f, 0), Quaternion.identity);
                newWall4.transform.SetParent(gameObject.transform);
                wallList.Add(newWall4);
                GameObject newWall5 = Instantiate(item[1], new Vector3(1.77f, -9.2f, 0), Quaternion.identity);
                newWall5.transform.SetParent(gameObject.transform);
                wallList.Add(newWall5);
                ironWall = false;
                PlayerManager.Instance.heartDefense = false;
            }
            else
            {
                if (!ironWall)
                {
                    for (int i = wallList.Count - 1; i >= 0; --i)
                    {
                        DestroyImmediate(wallList[i]);
                        wallList.RemoveAt(i);
                    }
                    GameObject newWall1 = Instantiate(item[2], new Vector3(-1.25f, -7.69f, 0), Quaternion.identity);
                    newWall1.transform.SetParent(gameObject.transform);
                    wallList.Add(newWall1);
                    GameObject newWall2 = Instantiate(item[2], new Vector3(0.26f, -7.69f, 0), Quaternion.identity);
                    newWall2.transform.SetParent(gameObject.transform);
                    wallList.Add(newWall2);
                    GameObject newWall3 = Instantiate(item[2], new Vector3(1.77f, -7.69f, 0), Quaternion.identity);
                    newWall3.transform.SetParent(gameObject.transform);
                    wallList.Add(newWall3);
                    GameObject newWall4 = Instantiate(item[2], new Vector3(-1.25f, -9.2f, 0), Quaternion.identity);
                    newWall4.transform.SetParent(gameObject.transform);
                    wallList.Add(newWall4);
                    GameObject newWall5 = Instantiate(item[2], new Vector3(1.77f, -9.2f, 0), Quaternion.identity);
                    newWall5.transform.SetParent(gameObject.transform);
                    wallList.Add(newWall5);
                    ironWall = true;
                }
                PlayerManager.Instance.heartDefenseTimeVal += Time.deltaTime;
            }
        }

        if (PlayerManager.Instance.showScore)
        {
            PlayerManager.Instance.showScore = false;
            scoreText.text = PlayerManager.Instance.displayScore.ToString();
            scoreText.transform.position = PlayerManager.Instance.diePos;
            Invoke("hideScore", 1);
        }
    }

    private void InitMap(int level)
    {
        Instantiate(level_map[level - 1], new Vector3(0, 0, 0), Quaternion.identity);
        // ¿ÕÆøÇ½
        for (int i = -1; i <=
            13; ++i)
        {
            CreateItem(item[6], new Vector3(1.51f * i - 8.8f, -10.71f, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(1.51f * i - 8.8f, 10.43f, 0), Quaternion.identity);
        }
        for (int i = 0; i < 13; ++i)
        {
            CreateItem(item[6], new Vector3(-10.31f, 1.51f * i - 9.2f, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(10.83f, 1.51f * i - 9.2f, 0), Quaternion.identity);
        }
        // Íæ¼Ò
        GameObject player = Instantiate(item[3], new Vector3(-2.76f, -9.2f, 0), Quaternion.identity);
        player.GetComponent<Born>().isBornPlayer = true;
        // µÐÈË
        CreateItem(item[3], new Vector3(-8.8f, 8.92f, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0.26f, 8.92f, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(9.32f, 8.92f, 0), Quaternion.identity);
        PlayerManager.Instance.enemyBornNum += 3;
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject item = Instantiate(createGameObject, createPosition, createRotation);
        item.transform.SetParent(gameObject.transform);
    }
    
    private void CreateEnemy()
    {
        List<int> bornPos = new List<int>();
        for (int i = 0; i < 3; ++i)
        {
            bool haveEnemy = false;
            for (int j = 0; j < PlayerManager.Instance.enemyAlive.Count; ++j)
            {
                if (PlayerManager.Instance.enemyAlive[j].GetComponent<Enemy>().x == 6 * i && PlayerManager.Instance.enemyAlive[j].GetComponent<Enemy>().y == 0)
                {
                    haveEnemy = true;
                    break;
                }
            }
            if (PlayerManager.Instance.player.GetComponent<Player1>().x == 6 * i && PlayerManager.Instance.player.GetComponent<Player1>().y == 0)
                haveEnemy = true;
            if (!haveEnemy)
                bornPos.Add(i);
        }
        if (bornPos.Count > 0)
        {
            Vector3 EnemyPos = new Vector3(9.06f * bornPos[Random.Range(0, bornPos.Count)] - 8.8f, 8.92f, 0);
            CreateItem(item[3], EnemyPos, Quaternion.identity);
            ++PlayerManager.Instance.enemyBornNum;
        }
    }
    
    private void hideScore()
    {
        scoreText.text = "";
    }
}
