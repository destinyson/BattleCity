using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public float moveSpeed;
    public float stopHeight;
    public Text playerText;
    public Text recordText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerParameter.life = 2;
        PlayerParameter.level = 1;
        PlayerParameter.fail = false;
        int score = PlayerParameter.playerScore;
        playerText.text = score == 0 ? "00" : score.ToString();
        recordText.text = PlayerParameter.record.ToString();
        PlayerParameter.playerScore = 0;
        PlayerParameter.chooseLevel = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < stopHeight)
            transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = transform.position;
            pos.y = stopHeight;
            transform.position = pos;
        }
    }
}
