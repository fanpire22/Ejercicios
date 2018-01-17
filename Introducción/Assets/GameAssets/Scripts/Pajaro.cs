using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pajaro : MonoBehaviour {
    
    //Fuerza de salto
    public int fuerza = 200;
    //Sprites del pájaro y objetos visibles
    public Sprite sprAleteo;
    public Sprite sprBase;
    public Sprite sprMuerte;
    private Rigidbody2D rig;
    private SpriteRenderer render;
    //Puntuación
    public int Puntos = 0;
    //Estado de vida
    public bool Muerto = false;
    //Sonido
    public AudioSource sfxSalto;
    public AudioSource sfxGolpe;
    public AudioSource sfxMuerte;
    public AudioSource sfxPuntos;

    // Use this for initialization
    private void Start () {
        rig = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	private void Update ()
    {
            //Si pulsamos el botón de salto, saltamos (Duh.)
            if (!Muerto) {
            if (Input.GetButtonDown("Jump")) {
                Saltar();
            } else {
                CalcularAnguloCaida();
            }
        }
    }

    /// <summary>
    /// El pájaro se ha chocado con algo: GAME OVER
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision) {
        //print(collision.collider.name.ToLower());
        if (!Muerto) {
            Muerto = true;
            sfxGolpe.Play();
            sfxMuerte.Play();
            render.sprite = sprMuerte;
            switch (collision.collider.name.ToLower()) {
                case "suelo":
                    //Colocamos el sprite de muerte, y lo rotamos
                    rig.rotation = -90;
                    break;
                case "columnasuelo":
                    break;
                case "columnacielo":
                    break;
                case "techo":
                    //No se muere. Simplemente, no sube más
                    Muerto = false;
                    RestaurarSpriteInicial();
                    break;
                default:
                    break;
            }
            if(Muerto)
                Invoke("Reiniciarpartida", 3f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Hemos pulsado salir
            Reiniciarpartida();
        }
    }

    /// <summary>
    /// Determina cuándo salimos de un trigger.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        Puntos += 1;
        sfxPuntos.Play();
        //print(string.Format("He conseguido {0}", Puntos));
    }

    /// <summary>
    /// Función para controlar el salto del pájaro
    /// </summary>
    private void Saltar() {

        sfxSalto.Play();
        render.sprite = sprAleteo;
        rig.velocity = Vector2.zero;
        rig.AddForce(Vector2.up * fuerza);

        Invoke("RestaurarSpriteInicial", 0.1f);
    }

    /// <summary>
    /// Función que restaura el sprite a su posición inicial
    /// </summary>
    private void RestaurarSpriteInicial() {
        if (!Muerto) {
            render.sprite = sprBase;
        }
    }

    /// <summary>
    /// Función que gira el pájaro cuando empieza a caer
    /// </summary>
    private void CalcularAnguloCaida() {
        if (rig.velocity.y > 0) {
            rig.rotation = 0;
        } else if(rig.rotation >-90) {
            //hacemos que caiga en picada.
            rig.rotation = rig.rotation - (rig.velocity.y * -1);
        }
    }

    /// <summary>
    /// Función que reinicia la partida a su estado inicial
    /// </summary>
    private void Reiniciarpartida() {
        SceneManager.LoadScene(0);
    }

}
