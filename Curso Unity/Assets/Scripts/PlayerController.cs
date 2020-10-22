using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animador;

    void Start()
    {
        animador = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void cambiarAnimacionA(string animacion = null)
    {
        if (!String.IsNullOrEmpty(animacion))
        {
            animador.Play(animacion);
        }

    }
}
