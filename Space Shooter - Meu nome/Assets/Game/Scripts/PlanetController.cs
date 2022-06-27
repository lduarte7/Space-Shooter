using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PlanetController : MonoBehaviour
{
    public GameObject[] Planets;

    Queue<GameObject> avaiblePlanets = new Queue<GameObject>();


    void Start()
    {
        avaiblePlanets.Enqueue(Planets[0]);
        avaiblePlanets.Enqueue(Planets[1]);
        avaiblePlanets.Enqueue(Planets[2]);
        avaiblePlanets.Enqueue(Planets[3]);

        InvokeRepeating("MovePlanetDown", 0, 20f);
    }


    void Update()
    {
        EnqueuePlanets();
    }
    void MovePlanetDown()
    {
        if (avaiblePlanets.Count == 0)
            return;


        GameObject aPlanet = avaiblePlanets.Dequeue();

        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    void EnqueuePlanets()
    {
        foreach(GameObject aPlanet in Planets)
        {
            if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                aPlanet.GetComponent<Planet>().ResetPos();

                avaiblePlanets.Enqueue(aPlanet);
            }
        }
    }
}
