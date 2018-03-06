using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


//Guardamos en un archivo de texto dónde nos encontramos, etc
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class CheckPoint : MonoBehaviour
{

    Animator _ani;
    bool isActive;
    private void Awake()
    {
        _ani = GetComponent<Animator>();
    }

    public void Reset()
    {
        isActive = false;
        _ani.SetBool("Action", isActive);
    }

    /// <summary>
    /// Recogemos los datos del jugador y los guardamos en el fichero de guardado
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")||isActive) return;
        //Es un checkpoint válido. Reseteo todos los Chekpoints y lo habilito
        GameManager.instance.ResetCheckPoints();

        isActive = true;
        _ani.SetBool("Action", isActive);

        FSaveData data = new FSaveData();
        data.Position = collision.transform.position;
        data.SceneIndex = SceneManager.GetActiveScene().buildIndex;

        string s = JsonUtility.ToJson(data);

        Save(s);

    }

    /// <summary>
    /// Guardamos los datos generados por el punto de guardado
    /// </summary>
    /// <param name="Json"></param>
    private void Save(string Json)
    {

        File.WriteAllText(string.Format("{0}{1}", FSaveData.FullPath, FSaveData.FileName), Json);

    }

}
