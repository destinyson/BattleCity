using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float timeVal = 0;

    public Sprite[] tankSprite;
    public bool changeSprite;
    public float changeSpriteTimeVal;

    public GameObject bulletPrefab;
    public GameObject explodePrefab;
    public GameObject shieldPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;

    private SpriteRenderer sr;
    private int index;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.isDefended = true;
        PlayerManager.Instance.defendTimeVal = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Instance.isDefended)
        {
            shieldPrefab.SetActive(true);
            PlayerManager.Instance.defendTimeVal -= Time.deltaTime;
            if (PlayerManager.Instance.defendTimeVal <= 0)
            {
                PlayerManager.Instance.isDefended = false;
                shieldPrefab.SetActive(false);
            }
        }
        if (timeVal >= 0.8f - 0.2f * Mathf.Min(PlayerParameter.tankLevel, 2))
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

    private void FixedUpdate()
    {
        if (PlayerParameter.fail)
            return;
        Move();
    }


    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            index = (h < 0 ? 3 : 1) + 4 * (changeSprite ? 1 : 0) + 8 * PlayerParameter.tankLevel;
            sr.sprite = tankSprite[index];
            bulletEulerAngles = new Vector3(0, 0, h > 0 ? -90 : 90);
            transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
            if (Mathf.Abs(h) > 0.05f)
            {
                moveAudio.clip = tankAudio[1];
                if (!moveAudio.isPlaying)
                    moveAudio.Play();
            }
            return;
        }
        else
        {
            float v = Input.GetAxisRaw("Vertical");
            if (v != 0)
            {
                index = (v < 0 ? 2 : 0) + 4 * (changeSprite ? 1 : 0) + 8 * PlayerParameter.tankLevel;
                sr.sprite = tankSprite[index];
                bulletEulerAngles = new Vector3(0, 0, v > 0 ? 0 : 180);
            }
            transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
            if (Mathf.Abs(v) > 0.05f)
            {
                moveAudio.clip = tankAudio[1];
                if (!moveAudio.isPlaying)
                    moveAudio.Play();
            }
            else
            {
                index = index % 4 + 4 * (changeSprite ? 1 : 0) + 8 * PlayerParameter.tankLevel;
                sr.sprite = tankSprite[index];
                moveAudio.clip = tankAudio[0];
                if (!moveAudio.isPlaying)
                    moveAudio.Play();
            }
        }
    }

    private void Die()
    {
        if (PlayerManager.Instance.isDefended)
            return;

        PlayerManager.Instance.isDead = true;
        Instantiate(explodePrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
