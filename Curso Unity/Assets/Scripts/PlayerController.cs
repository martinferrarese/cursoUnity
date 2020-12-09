using System;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animador;
    public GameObject juego;
    public AudioClip sonidoDeSalto;
    public AudioClip sonidoDeMuerte;
    private AudioSource sonidosDelPersonaje;
    private float posicionInicialY;
    private bool estaEnElSuelo;
    void Start()
    {
        animador = GetComponent<Animator>();
        sonidosDelPersonaje = GetComponent<AudioSource>();
        posicionInicialY = transform.position.y;
    }

    void Update()
    {
        estaEnElSuelo = transform.position.y == posicionInicialY;
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
        if(estaEnElSuelo)
            Reproducir(sonidoDeSalto);

    }
    void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.tag == "Enemy")
        {
            animador.Play("PlayerLost");
            juego.SendMessage("DetenerMusica");
            Reproducir(sonidoDeMuerte);
        }

        juego.SendMessage("PerderJuego");
    }

    private void Reproducir(AudioClip sonido)
    {
        sonidosDelPersonaje.clip = sonido;
        sonidosDelPersonaje.Play();
    }
}
