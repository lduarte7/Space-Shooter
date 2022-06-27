using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed;
    GameObject TextScore;

    void Start()
    {
        speed = 6f;
        TextScore = GameObject.FindGameObjectWithTag("ScoreText"); // aula de pontuação
    }

    // Update is called once per frame
    void Update()
    {
        // pega a posição atual da bala
        Vector2 position = transform.position;

        // calcula a nova posição da bala
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        // atualiza a nova posição da bala
        transform.position = position;

        // essa é o ponto de cima e da direita da tela
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // se ir para fora da parte de cima da tela, destruimos a bala
        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyShip")
        {
            TextScore.GetComponent<GameScore>().Score += 100; // adiciona 100 pontos ao score
            Destroy(gameObject);
        }
    }

}
