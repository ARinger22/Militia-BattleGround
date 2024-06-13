using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public int enemyCount = 0;

    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        Debug.Log("Enemy count: " + enemyCount);
    }

    public void CountZero()
    {
        enemyCount -= 1;
        if(enemyCount == 0)
        {
            Debug.Log("You win!");
        }
    }
}
