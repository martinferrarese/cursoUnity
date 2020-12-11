using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Hace que en Unity esto se pueda usar como un deslizable para hacerlo variar
    [Range(0, 0.10f)]
    public float velocidadDelParallax = 0.02f;
    public RawImage background;
    public RawImage platform;
    public AudioSource musicaDeFondo;
    public GameState estadoDelJuego = GameState.Idle;

    public GameObject uiIdle;
    public GameObject uiLost;

    public float scaleTime = 6f;
    public float scaleIncrement = .25f;

    public GameObject player;
    public GameObject generadorDeEnemigos;
    public enum GameState
    {
        Idle,
        Playing,
        ListoParaReiniciar
    };

    // Start is called before the first frame update
    void Start()
    {
        uiIdle.SetActive(true);
        uiLost.SetActive(false);
        estadoDelJuego = GameState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        //Empieza el juego
        if (estadoDelJuego == GameState.Idle && Input.anyKey)
        {
            estadoDelJuego = GameState.Playing;
            musicaDeFondo.Play();
            uiIdle.SetActive(false);
            player.SendMessage("CambiarAnimacionA", "PlayerRun");
            player.SendMessage("IniciarEfectoDePolvo");
            generadorDeEnemigos.SendMessage("ComenzarGeneracionDeEnemigos");
            InvokeRepeating("GameTimeScale", scaleTime, scaleTime);
        }
        else if (estadoDelJuego == GameState.Playing)
        {
            IniciarParallax();
        }
        else if (estadoDelJuego == GameState.ListoParaReiniciar && Input.GetKeyDown(KeyCode.R))
        {
            //Recarga la escena
            SceneManager.LoadScene("Principal");
        }

    }

    private void IniciarParallax()
    {
        // Necesario para regular la velocidad independientemente del dispositivo
        float velocidadFinal = velocidadDelParallax * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + velocidadFinal, 0, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + velocidadFinal * 4, 0, 1f, 1f);
    }

    public void PerderJuego()
    {
        uiLost.SetActive(true);
        generadorDeEnemigos.SendMessage("CancelarGeneracionDeEnemigos");
        generadorDeEnemigos.SendMessage("LimpiarEnemigos");
        ResetTimeScale();
        estadoDelJuego = GameState.ListoParaReiniciar;
    }

    public void DetenerMusica()
    {
        musicaDeFondo.Stop();
    }

    public void GameTimeScale()
    {
        //Encargado de la escala de velocidad del juego
        //Le aumenta un cuarto de velocidad a ese tiempo
        Time.timeScale += scaleIncrement;
        Debug.Log("Ritmo incrementado: " + Time.timeScale);
    }

    private void ResetTimeScale()
    {
        CancelInvoke("GameTimeScale");
        Time.timeScale = 1f;
        Debug.Log("Ritmo reestablecido: " + Time.timeScale);
    }
}
