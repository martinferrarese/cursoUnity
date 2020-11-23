using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocidad = 2f;
    public Rigidbody2D enemigo;

    // Start is called before the first frame update
    void Start()
    {
        enemigo = GetComponent<Rigidbody2D>();
        enemigo.velocity = Vector2.left * velocidad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if(colisionDetectada.gameObject.tag == "ParedIzquierda")
            Destroy(gameObject);
    }
}
