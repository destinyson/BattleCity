using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;
    public bool isPlayerBullet;
    public GameObject hitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullet)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullet)
                {
                    if (!collision.gameObject.GetComponent<Enemy>().isHeavy || collision.gameObject.GetComponent<Enemy>().hitCount == 3)
                    {
                        PlayerManager.Instance.diePos = Camera.main.WorldToScreenPoint(collision.gameObject.transform.position);
                        PlayerManager.Instance.displayScore = collision.gameObject.GetComponent<Enemy>().score;
                        PlayerManager.Instance.showScore = true;
                    }          
                    else
                        Instantiate(hitPrefab, transform.position, transform.rotation);
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Instantiate(hitPrefab, transform.position, transform.rotation);
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Iron":
                if (isPlayerBullet)
                {
                    collision.SendMessage("PlayAudio");
                    if (PlayerParameter.tankLevel == 3)
                        Destroy(collision.gameObject);
                }
                Instantiate(hitPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
                break;
            case "AirWall":
                if (isPlayerBullet)
                    collision.SendMessage("PlayAudio");
                Instantiate(hitPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
                break;
            case "Bullet":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            default: break;
        }  
    }
}
