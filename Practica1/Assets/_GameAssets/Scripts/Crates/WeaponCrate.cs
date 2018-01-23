using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrate : MonoBehaviour {


    private int[] Restore = new int[] { 140, 2 }; //Van en orden: AKM, Granadas

    [SerializeField] private EBehaviour currentType;

    public enum EBehaviour
    {
        AKM,
        Grenade
    }

    // Use this for initialization
    void Start ()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        mat.SetColor("_Color", new Color(0.1f, 0.4f, 0.1f));

        int indexChild = (int)currentType;
        transform.GetChild(indexChild).gameObject.SetActive(true);
	}

    /// <summary>
    /// Al pasar por la caja, añadimos el arma al jugador, y le damos munición. Si ya tiene arma, se queda sólo con la munición
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        ShooterCharacter chara = other.GetComponent<ShooterCharacter>();
        if (chara)
        {
            int indexOfWeapon = (int)currentType + 1; //AKM es 1, Granada es 2
            chara.AddWeapon(indexOfWeapon);

            chara.AddAmmo(indexOfWeapon, Restore[(int)currentType]);
            Destroy(gameObject);
        }
    }

}
