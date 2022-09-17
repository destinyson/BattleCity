using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public bool isBornPlayer;
    public GameObject playerPrefab;
    public GameObject[] enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 0.8f);
        Destroy(gameObject, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank()
    {
        if (isBornPlayer)
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        else
        {
            int num = Random.Range(0, 4);
            if (!PlayerManager.Instance.hasBonus && Random.Range(0, 10) == 0)
                num += 4;
            GameObject enemy = Instantiate(enemyPrefab[num], transform.position, Quaternion.identity);
            PlayerManager.Instance.enemyAlive.Add(enemy);
        }
    }
}
