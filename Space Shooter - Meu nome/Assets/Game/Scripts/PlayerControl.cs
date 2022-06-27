using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// O PLAYER DEVE ESTAR INATIVO QUANDO O JOGO COMEÇAR 

public class PlayerControl : MonoBehaviour
{
    public float Speed; // velocidade da nave

    public GameObject PlayerBulletGO; // esse é o Prefab do nosso tiro
    public GameObject Bullet1Position;
    public GameObject Bullet2Position;
    public GameObject ExplosionGO;

    AudioSource Laser;



    public Text LivesUIText; //referencia para o texto de vidas da UI


    const int MaxLives = 3; // máximo de vidas do player
    int lives; // vida atual do player


    public GameObject GameManagerGO; // referencia para nosso GameManager

    float accelStartY; // pegar o valor do Y acelerometro ao iniciar 

    public void Init()
    {
        lives = MaxLives;

        //atualiza o texto de vidas UI
        LivesUIText.text = lives.ToString();

        //reseta a posição da nave para o centro da tela
        transform.position = new Vector2(0, 0);

        //define esse objeto de jogo para ativo
        gameObject.SetActive(true);

        Laser = GetComponent<AudioSource>();
    }

    void Start (){
        accelStartY = Input.acceleration.y; // só no final do jogo
    }

    void Update()
    {
        // atira os projeteis quando a tecla espaço é pressionada
        /*if (Input.GetKeyDown("space"))
        {
            Laser.Play();

            //instancia o primeiro tiro
            GameObject bullet1 = (GameObject)Instantiate(PlayerBulletGO);
            bullet1.transform.position = Bullet1Position.transform.position; //define a posição inicial do tiro

            //instancia o segundo tiro
            GameObject bullet2 = (GameObject)Instantiate(PlayerBulletGO);
            bullet2.transform.position = Bullet2Position.transform.position; //define a posição inicial do tiro
        }*/

        //float x = Input.GetAxisRaw("Horizontal"); // O valor vai ser -1, 0 ou 1 (esquerda, parado e direita)
        //float y = Input.GetAxisRaw("Vertical"); // O valor vai ser -1, 0 ou 1 (baixo, parado e cima)

        //com base na entrada do valor armazenado na direção do vetor, normalizamos isso para pegar um vetor na Unity
        //Vector2 direction = new Vector2(x, y);


        float x = Input.acceleration.x;
        float y = Input.acceleration.y - accelStartY;

        Vector2 direction = new Vector2(x, y);

        if (direction.sqrMagnitude > 1) {
            direction.Normalize();
        }
                

        Move(direction);
    }


    public void Shoot (){
            Laser.Play();

            //instancia o primeiro tiro
            GameObject bullet1 = (GameObject)Instantiate(PlayerBulletGO);
            bullet1.transform.position = Bullet1Position.transform.position; //define a posição inicial do tiro

            //instancia o segundo tiro
            GameObject bullet2 = (GameObject)Instantiate(PlayerBulletGO);
            bullet2.transform.position = Bullet2Position.transform.position; //define a posição inicial do tiro
    }


    void Move(Vector2 direction)
    {
        //procura os limites da tela para os movimentos do jogador (esquerda, direita, cima, baixo e nos cantos da tela

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // essa é a parte de baixo e a esquerda(canto) da tela
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // essa é a parte de cima e a direita(canto) da tela

        max.x = max.x - 0.225f; //subtrai o valor do player sprite pela metade da largura
        min.x = min.x + 0.225f; //soma o valor do player sprite pela metade da largura

        max.y = max.y - 0.285f; //subtrai o valor do player sprite pela metade da altura
        min.y = min.y + 0.285f; //soma o valor do player sprite pela metade da altura

        //pega a posição atual do player 
        Vector2 pos = transform.position;

        //calculando a nova posição
        pos += direction * Speed * Time.deltaTime;

        //garantindo que a nova posição não esteja fora da tela
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //atualizando a nova posição do player
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyShip")
        {
 

            lives--; // subtrai uma vida
            LivesUIText.text = lives.ToString(); // atualiza o texto Lives no UI

            if (lives == 0) // se o player morrer
            {
                // Muda o estado do game manager para o game over
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);

                PlayExplosion();

                gameObject.SetActive(false); //esconde a nave
            }
           


            // Destroy(gameObject); // em vez de destruir o player, vamos só esconder ele
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
