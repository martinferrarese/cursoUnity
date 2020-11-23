using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animador;
    public GameObject juego;
    void Start()
    {
        animador = GetComponent<Animator>();
    }

    void Update()
    {
        GameController.GameState estadoDelJuego = juego.GetComponent<GameController>().estadoDelJuego;
        bool jugadorPresionaSaltar = (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow));

        if (estadoDelJuego == GameController.GameState.Playing && jugadorPresionaSaltar)
        {
            Saltar();
        }
    }

    public void CambiarAnimacionA(string animacion = null)
    {
        if (!String.IsNullOrEmpty(animacion))
        {
            animador.Play(animacion);
        }
    }

    private void Saltar()
    {
        animador.Play("PlayerJump");
    }
    void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.tag == "Enemy")
        {
            animador.Play("PlayerLost");
        }

        juego.SendMessage("PerderJuego");
    }
}
