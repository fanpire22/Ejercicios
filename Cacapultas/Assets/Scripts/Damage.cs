using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    public float _rad = 1;
    public float _sizeStep = 1;

    private float _elapsedTime;
    private float _lerp;

    private void Update()
    {
        //Lineal
        _elapsedTime += Time.deltaTime * _sizeStep;
        _lerp = Mathf.Lerp(0, _rad * 2, _elapsedTime);
        transform.localScale = Vector3.one * _lerp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _rad);
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _lerp * 0.5f);

        for(int i = 0; i < colliders.Length; i++)
        {

            if(colliders[i].CompareTag("Damageable"))
            {
                colliders[i].GetComponent<Damageable>().Damage();
            }

        }
    }
}
