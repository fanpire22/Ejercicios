using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerSimpleMove : MonoBehaviour
{

    private CharacterController _controller;
    [SerializeField] private float _movementSpeed;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }


    private void Update()
    {

        //Obtenemos los dos ejes: Lateral y vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Vector de movimiento: Debemos multiplicarlo por el tiempo de cada frame para hacerlo estable, y también por la velocidad
        Vector3 speed = (transform.forward * vertical + transform.right * horizontal) * _movementSpeed * Time.deltaTime;

        //Esta función de movimiento respeta la gravedad y también las colisiones. A su vez, se mueve correctamente por pendientes y también escalones
        _controller.SimpleMove(speed);


        if (Input.GetButtonDown("Jump"))
        {
            Time.timeScale = .1f;
            Invoke("RestoreBulletTime", 2);
        }

    }

    private void RestoreBulletTime()
    {
        Time.timeScale = 1;
    }

}
