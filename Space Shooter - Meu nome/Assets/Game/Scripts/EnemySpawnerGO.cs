using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerGO : MonoBehaviour
{
    public GameObject EnemyGO; // prefab do inimigo

    float maxSpawnRateInSeconds = 10f;
    void SpawnEnemy() // essa função vai criar os inimigos no topo da tela e randomicamente vai escolher uma posição entre a direita e a esquerda da tela
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn() // tempo que o inimigo vai nascer
    {
        float spawnInSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {          
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds); // seleciona um numero entre 1 e a variavel maxSpawnRateInSeconds
        }
        else
            spawnInSeconds = 1f;
        Invoke("SpawnEnemy", spawnInSeconds);
    }

    // função para dificultar o jogo
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    // função para começar o spawn dos inimigos

    public void ScheduleEnemySpawner()
    {
        //reseta o numero de spawn máximo
        float maxSpawnRateInSeconds = 3f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        // aumenta a taxa de spawn a cada 20 segundos
        InvokeRepeating("IncreaseSpawnRate", 0f, 20f);
    }

    // função para interromper o spawn dos inimigos

    public void CancelSpawn()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
