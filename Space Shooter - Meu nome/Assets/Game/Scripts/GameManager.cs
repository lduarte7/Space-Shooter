using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;

    public GameObject enemySpawner; // referencia do objeto de spawner dos inimigos

    public GameObject GameoverGO; // referencia ao sprite de Gameover

    public GameObject TextScore; // ref ao Text Score

    public GameObject PlanetController; // ref ao PlanetController

    public GameObject TimeCounterGO;

    public GameObject Title;
    public enum GameManagerState
    {
        Opening,
        Gameplay,
        Gameover,
    }

    GameManagerState GMState;



    public GameObject PlayerBulletGO; // esse é o Prefab do nosso tiro
    public GameObject Bullet1Position;
    public GameObject Bullet2Position;
    public GameObject shotButton;

    AudioSource Laser;
    // Start is called before the first frame update
    void Start()
    {
        Laser = GetComponent<AudioSource>();
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {

            case GameManagerState.Opening:
                // ativa o titulo
                Title.SetActive(true);

                // esconde o game over
                GameoverGO.SetActive(false);

                // ativa o botão de play para visivel
                playButton.SetActive(true);

                // os planetas não vão se mexer quando estiver no menu
                PlanetController.SetActive(false);

                shotButton.SetActive(false);

                break;

            case GameManagerState.Gameplay:
                // desativa o titulo
                Title.SetActive(false);

                // reseta a pontuação, aula de score
                TextScore.GetComponent<GameScore>().Score = 0;

                // ativa os planetas
                PlanetController.SetActive(true);

                // esconde o botão de Play
                playButton.SetActive(false);

                // ativa o estado do jogador para visivel e inicia as vidas
                playerShip.GetComponent<PlayerControl>().Init();

                // inicia o contador de tempo
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                // começa o spawn de inimigos
                enemySpawner.GetComponent<EnemySpawnerGO>().ScheduleEnemySpawner();

                shotButton.SetActive(true);
                break;
            case GameManagerState.Gameover:

                // desativa o contador de tempo
                TimeCounterGO.GetComponent<TimeCounter>().StopTimerCount();

                // desativa o spawn dos inimigos
                enemySpawner.GetComponent<EnemySpawnerGO>().CancelSpawn();

                TextScore.GetComponent<GameScore>().Score = 0; // reseta o score

                // mostra o gameover
                GameoverGO.SetActive(true);

                shotButton.SetActive(false);

                // muda o Game manager para o estado de Opening depois de 5 segundos
                Invoke("ChangeToOpeningState", 5f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();

    }

    public void StarGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState(); 
    }

    // função para mudar o Game Manager para o estado de abertura
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening); 
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
}
