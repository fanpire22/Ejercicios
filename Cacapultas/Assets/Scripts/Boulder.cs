using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {

    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _damage;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosion = Instantiate(_explosion);
        GameObject damage = Instantiate(_damage);

        explosion.transform.position = transform.position;
        damage.transform.position = transform.position;

        Destroy(explosion, 3.75f);
        Destroy(damage, 3f);
        Destroy(this.gameObject);
    }
}
