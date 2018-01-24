using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] private GameObject prefMuzzle;
    [SerializeField] private GameObject prefBullet;
    private float fuerzaBala = 5;
    private Transform _rayOrigin;

    private void Awake()
    {
        _rayOrigin = transform.Find("GunMuzzle");
        base.AddAmmo(200);
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnShoot()
    {

        GameObject flash = Instantiate(prefMuzzle, _rayOrigin);
        Destroy(flash, 0.2f);

        Vector3 CenterScreen = Camera.main.ViewportToScreenPoint(new Vector3(.5f, .5f));
        Ray ray = Camera.main.ScreenPointToRay(CenterScreen);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
        {
            Vector3 worldPoint = hit.point;
            Vector3 dirWorldPoint = (worldPoint - _rayOrigin.position).normalized;

            RaycastHit[] hits = Physics.RaycastAll(_rayOrigin.position, dirWorldPoint, 1000);

            for (int i = 0; i < hits.Length; i++)
            {
                if (i > 1) break;

                Damageable dmg = hits[i].collider.GetComponent<Damageable>();
                Rigidbody rb = hits[i].collider.GetComponent<Rigidbody>();

                if (dmg)
                {
                    dmg.GetDamage(base._damage, 0);
                }

                if (rb)
                {
                    rb.AddForceAtPosition(hits[i].point, dirWorldPoint * fuerzaBala, ForceMode.Impulse);
                }


                //Instanciamos la partícula del impacto en la superficie en la que impactemos, siempre que sea un rigidbody.
                //¡Y, por supuesto, el prefab debe mirar hacia la dirección desde la que vino el impacto!
                Quaternion rotation = Quaternion.LookRotation(hits[i].normal);

                //Obtenemos el prefab propio del Damageable. Si no tiene propio, nos quedamos el del arma
                GameObject pref = null;
                if (dmg) pref = dmg.GetOverrideBulletHole();
                if (!pref) pref = prefBullet;

                //Colocamos por fin la partícula
                GameObject.Instantiate(pref, hits[i].point, rotation);

            }
        }
    }

    /// <summary>
    /// Función para disparar, heredada de WeaponBase
    /// </summary>
    protected override void OnSecondAction()
    {

    }
}