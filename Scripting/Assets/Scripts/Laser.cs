using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    //private float velocidad = 10f;
    [SerializeField] private GameObject _prefabExplosion;
    private int _danno;

#region "Propiedades"

    public int Danno { set { _danno = value; } }

#endregion

    // Use this for initialization
    void Start () {
        Invoke("Disipar", 3f);
	}
	
	// Update is called once per frame
	void Update () {
        //Hacemos que avance a una velocidad constante en la dirección en la que mira. IMPORTANTE, el local es en TRANSFORM.FORWARD
        //transform.localPosition = transform.localPosition + transform.up * Time.deltaTime * -1 * velocidad;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //Cancelamos las invocaciones previas para que no intente autodestruirse por tiempo
        CancelInvoke();

        if ( collision.gameObject.CompareTag("Soldado"))
        {
            //Hemos chocado contra un soldado
            collision.collider.GetComponent<Soldado>().RecibirDisparo(_danno);
        }else if (collision.gameObject.CompareTag("Arma"))
        {
            //Hemos chocado contra el arma
            collision.collider.GetComponentInParent<Soldado>().RecibirDisparo(_danno);
        }

        //Creamos un efecto de explosión en el lugar de colisión para que quede bonito
        GameObject nuevaExplosion = Instantiate(_prefabExplosion);
        nuevaExplosion.transform.position = this.transform.position;

        //Programamos para que desaparezca la explosión, y borramos la bala. Lo programamos a un segundo porque la duración de la explosión es ligeramente inferior de ese tiempo
        Destroy(this.gameObject);
        Destroy(this.transform.parent.gameObject);
        Destroy(nuevaExplosion, 1);
    }

    public void Disipar()
    {
        //Programamos para que desaparezca la bala.
        Destroy(this.gameObject);
        Destroy(this.transform.parent.gameObject);
    }


}
