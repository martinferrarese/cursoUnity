using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Hace que en Unity esto se pueda usar como un deslizable para hacerlo variar
    [Range(0, 0.10f)]
    public float velocidadDelParallax = 0.02f;
    public RawImage background;
    public RawImage platform;
    public GameState estadoDelJuego = GameState.Idle;
    public GameObject uiIdle;
    public GameObject player;
    public enum GameState
    {
        Idle,
        Playing
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (estadoDelJuego == GameState.Idle && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0)))
        {
            estadoDelJuego = GameState.Playing;
            uiIdle.SetActive(false);
            player.SendMessage("cambiarAnimacionA", "PlayerRun");
        }
        else if (estadoDelJuego == GameState.Playing)
        {
            InciarParallax();
        }
    }

    private void InciarParallax()
    {
        // Necesario para regular la velocidad independientemente del dispositivo
        float velocidadFinal = velocidadDelParallax * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + velocidadFinal, 0, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + velocidadFinal * 4, 0, 1f, 1f);
    }
}
