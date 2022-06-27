using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject TextScore; // aula de pontuação


    float speed; // velocidade do inimigo


    public GameObject ExplosionGO;

    void Start()
    {
        speed = 2f; // para definir a velocidade

        
    }


    void Update()
    {
        // para pegar a posição atual do inimigo
        Vector2 position = transform.position;

        // calcula a nova posição do inimigo
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        // atualiza a nova posição
        transform.position = position;

        // esse é o ponto esquerdo e de baixo da tela
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // se o inimigo for para fora da tela de baixo, é destruido
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "PlayerShip" || collision.tag == "PlayerBullet")
        {

            PlayExplosion();


            Destroy(gameObject); // destroi a nave inimiga
            TextScore = GameObject.FindGameObjectWithTag("ScoreText"); // aula de pontuação
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}
