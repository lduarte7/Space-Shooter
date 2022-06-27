using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    Text timeUI; //referencia ao contador de texto UI

    float StartTime; //o tempo em que o usuario clica em JOGAR
    float ellapsedTime; //o tempo decorrido quando o jogador clicou em JOGAR
    bool StartCount; //verifica se iniciou o contador

    int minutes;
    int seconds;

    // Start is called before the first frame update
    void Start()
    {
        StartCount = false;

        timeUI = GetComponent<Text>(); // pega o componente UI deste gameObject
    }

    public void StartTimeCounter() // função para iniciar o contador do tempo
    {

        StartTime = Time.time;
        StartCount = true;

    }

    public void StopTimerCount() // função para parar
    {
        StartCount = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartCount)
        {
            ellapsedTime = Time.time - StartTime; // conta o tempo decorrido

            minutes = (int)ellapsedTime / 60; // calcula os minutos
            seconds = (int)ellapsedTime % 60; // calcula os segundos

            timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds); // atualiza o objeto de texto UI do cenario

        }
    }
}
