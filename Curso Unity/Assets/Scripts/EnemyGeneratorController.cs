using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public GameObject fabricadorDeMonstruos;
    public float tiempoDeCreacion = 0.2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CrearEnemigo()
    {
        Instantiate(fabricadorDeMonstruos, transform.position, Quaternion.identity);
    }

    void ComenzarGeneracionDeEnemigos()
    {
        InvokeRepeating("CrearEnemigo", 0f, tiempoDeCreacion);
    }

}
