using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPortal : MonoBehaviour {

    [SerializeField] float CoolDown;
    [SerializeField] Transform Destiny;

    ParticleSystem _ps;
    BoxCollider2D col;
    float _firstTimeEnter = -1;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
        col = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(_firstTimeEnter > 0 && Time.time > _firstTimeEnter + CoolDown)
        {
            SimonBelmont simonSimon = GameManager.instance.getSimonSimon();
            simonSimon.transform.position = new Vector3(Destiny.position.x,Destiny.position.y,simonSimon.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _ps.Play();
            _firstTimeEnter = Time.time;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _ps.Stop();
            _firstTimeEnter = -1;
        }
    }
}
