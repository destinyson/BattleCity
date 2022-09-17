using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTools : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] BonusSprite;

    private int type;
    public float lastTimeVal;
    public bool on = true;
    public float blingTimeVal;

    public GameObject explodePrefab;

    public AudioClip addLifeAudio;
    public AudioClip defaultAudio;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        type = Random.Range(0, 6);
        //type = 4;
        sr.sprite = BonusSprite[type];
    }

    private void Update()
    {
        if (lastTimeVal > 10)
        {
            Destroy(gameObject);
            PlayerManager.Instance.hasBonus = false;
        }
        else
        {
            if (blingTimeVal > 0.5f)
            {
                on = !on;
                sr.sprite = BonusSprite[on ? type : 6];
                blingTimeVal = 0;
            }
            else
                blingTimeVal += Time.deltaTime;
            lastTimeVal += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tank")
        {
            PlayerParameter.playerScore += 500;
            if (type == 0)
            {
                ++PlayerParameter.life;
                AudioSource.PlayClipAtPoint(addLifeAudio, transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(defaultAudio, transform.position);
                if (type == 1)
                {
                    PlayerManager.Instance.freeze = true;
                    PlayerManager.Instance.freezeTimeVal = 0;
                }
                else if (type == 2)
                {
                    PlayerManager.Instance.heartDefense = true;
                    PlayerManager.Instance.heartDefenseTimeVal = 0;
                }
                else if (type == 3)
                {
                    for (int i = PlayerManager.Instance.enemyAlive.Count - 1; i >= 0; --i)
                    {
                        Instantiate(explodePrefab, PlayerManager.Instance.enemyAlive[i].transform.position, PlayerManager.Instance.enemyAlive[i].transform.rotation);
                        Destroy(PlayerManager.Instance.enemyAlive[i]);
                        PlayerManager.Instance.enemyAlive.RemoveAt(i);
                        --PlayerManager.Instance.enemy;
                        PlayerManager.Instance.EnemyIcon[PlayerManager.Instance.enemy].SetActive(false);
                    }
                }
                else if (type == 4)
                {
                    if (PlayerParameter.tankLevel < 3)
                        ++PlayerParameter.tankLevel;
                }
                else if (type == 5)
                {
                    PlayerManager.Instance.isDefended = true;
                    PlayerManager.Instance.defendTimeVal = 10;
                }
            }
            PlayerManager.Instance.diePos = Camera.main.WorldToScreenPoint(transform.position);
            PlayerManager.Instance.displayScore = 500;
            PlayerManager.Instance.showScore = true;
            Destroy(gameObject);
            PlayerManager.Instance.hasBonus = false;
        }
    }
}
