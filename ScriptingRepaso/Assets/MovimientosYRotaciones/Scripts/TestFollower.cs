using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFollower : MonoBehaviour {

    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _prefabParticle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Vector3 directionToTarget = _target.transform.position - transform.position;

        //directionToTarget.y = 0;

        //if (directionToTarget.magnitude > 0.8)
        //{
            this.transform.LookAt(_target.transform.position);

        //    this.transform.position += directionToTarget * _speed * Time.deltaTime;
        //}

        Vector3 targetposition = _target.transform.position;
        targetposition.y = this.transform.position.y;
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetposition, _speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ICE")
        {
            GameObject part = Instantiate(_prefabParticle);
            part.transform.position = this.transform.position;

            Destroy(part, 1);
            Destroy(this.gameObject, 0.1f);
        }
    }
}
