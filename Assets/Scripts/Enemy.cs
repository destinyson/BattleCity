using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3;
    public bool isBonus;
    public bool isHeavy;
    public int hitCount;
    private Vector3 bulletEulerAngles;

    public GameObject bulletPrefab;
    public GameObject explodePrefab;
    public AudioSource heavyAudio;

    public Sprite[] tankSprite;
    public bool changeSprite;
    public float changeSpriteTimeVal;
    private int index = 2;

    private float timeVal = 0;
    private float changeDirTimeVal = 0;

    private bool isStopped;
    private bool upDisabled;
    private bool downDisabled;
    private bool leftDisabled;
    private bool rightDisabled;

    public int score;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Instance.freeze)
        {
            if (PlayerManager.Instance.freezeTimeVal >= 20)
            {
                PlayerManager.Instance.freeze = false;
                PlayerManager.Instance.freezeTimeVal = 0;
            }
            else
                PlayerManager.Instance.freezeTimeVal += Time.deltaTime;
        }
        else
        {
            if (timeVal >= 2)
                Attack();
            else
                timeVal += Time.deltaTime;

            if (changeSpriteTimeVal >= 0.1f)
            {
                changeSprite = !changeSprite;
                changeSpriteTimeVal = 0;
            }
            else
                changeSpriteTimeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.freeze)
            Move();    
    }


    private void Attack()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
    }

    private void Move()
    {
        if (changeDirTimeVal >= 4)
        {
            List<int> dir = new List<int>();
            if (!upDisabled)
                dir.Add(0);
            if (!rightDisabled)
                dir.Add(1);
            if (!downDisabled)
                dir.Add(2);
            if (!leftDisabled)
                dir.Add(3);
            if (dir.Count > 0)
                index = dir[Random.Range(0, dir.Count)];
            upDisabled = false;
            rightDisabled = false;
            downDisabled = false;
            leftDisabled = false;
            changeDirTimeVal = 0;
        }
        else
            changeDirTimeVal += Time.fixedDeltaTime;

        Debug.Log(index);
        Debug.Log(changeDirTimeVal);
        index = index % 4 + 4 * (changeSprite ? 1 : 0);
        if (isHeavy)
            index += 8 * hitCount;
        sr.sprite = tankSprite[index];

        if (index > 0)
        {
            if (index % 2 == 1)
            {
                bulletEulerAngles = new Vector3(0, 0, index % 4 == 3 ? 90 : -90);
                transform.Translate(Vector3.right * (index % 4 == 3 ? -1 : 1) * moveSpeed * Time.fixedDeltaTime, Space.World);
            }
            else
            {
                bulletEulerAngles = new Vector3(0, 0, index % 4 == 2 ? -180 : 0);
                transform.Translate(Vector3.up * (index % 4 == 2 ? -1 : 1) * moveSpeed * Time.fixedDeltaTime, Space.World);
            }
        }
    }

    private void Die()
    {
        if (isBonus)
        {
            PlayerManager.Instance.createBonus = true;
            isBonus = false;
        }
        if (!isHeavy || hitCount == 3)
        {
            Instantiate(explodePrefab, transform.position, transform.rotation);
            PlayerParameter.playerScore += score;
            switch (score)
            {
                case 100: ++PlayerManager.Instance.enemy1_num; break;
                case 200: ++PlayerManager.Instance.enemy2_num; break;
                case 300: ++PlayerManager.Instance.enemy3_num; break;
                case 400: ++PlayerManager.Instance.enemy4_num; break;
                default: break;
            }
            --PlayerManager.Instance.enemy;
            for (int i = 0; i < PlayerManager.Instance.enemyAlive.Count; ++i)
            {
                if (PlayerManager.Instance.enemyAlive[i].transform == gameObject.transform)
                {
                    Destroy(PlayerManager.Instance.enemyAlive[i]);
                    PlayerManager.Instance.enemyAlive.RemoveAt(i);
                    break;
                }
            }
            Destroy(gameObject);
            PlayerManager.Instance.EnemyIcon[PlayerManager.Instance.enemy].SetActive(false);
            PlayerManager.Instance.enemyAlive.RemoveAll(item => item == null);
        }
        else
        {
            ++hitCount;
            heavyAudio.Play();
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (index == 2 && collision.contacts[0].normal.y == 1)
        {
            downDisabled = true;
            changeDirTimeVal = 4;
        }
        else if (index == 3 && collision.contacts[0].normal.x == 1)
        {
            leftDisabled = true;
            changeDirTimeVal = 4;
        }
        else if (index == 0 && collision.contacts[0].normal.y == -1)
        {
            upDisabled = true;
            changeDirTimeVal = 4;
        }
        else if (index == 1 && collision.contacts[0].normal.x == -1)
        {
            rightDisabled = true;
            changeDirTimeVal = 4;
        }
    }
}
