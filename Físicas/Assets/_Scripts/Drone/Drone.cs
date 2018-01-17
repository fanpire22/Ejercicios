using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationSpeed = 50;
    [SerializeField] private GameObject _impactPrefab;
    [SerializeField] private float _maxAngleShoot = 90;
    [SerializeField] private float _forceBullet = 50;

    [Header("Values")]
    private float _rotationY;
    private Rigidbody _rig;
    private Vector3 _move;

    private Ray _ray;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float upDown = Input.GetAxis("UpDown");

        Vector3 forward = transform.forward * vertical;
        Vector3 up = transform.up * upDown;

        _rotationY += horizontal * rotationSpeed * Time.deltaTime;
        _move = (forward + up) * speed;

        _ray.origin = transform.position;
        _ray.direction = transform.forward;

        if(Input.GetButtonDown("Fire1"))
        {
            RaycastHit isHit;

            //Coordenadas del ratón
            Vector3 mouse = Input.mousePosition;

            //Posición global del objeto bajo el ratón
            Ray cameraRay = Camera.main.ScreenPointToRay(mouse);

            //Dirección del dron a la posición del objeto (si hemos impactado en algo)
            if (Physics.Raycast(cameraRay, out isHit, float.PositiveInfinity))
            {
                Vector3 dirMouseFromDrone = (isHit.point - transform.position).normalized;

                //Comprobamos si alcanzamos al objetivo con nuestro dron (alcance, 1000)
                if (Physics.Raycast(transform.position, dirMouseFromDrone, out isHit, 1000))
                {
                    //Determinamos si el objeto está dentro del ángulo frontal del dron
                    float angle = Vector3.Angle(transform.forward, dirMouseFromDrone);
                    if (angle < _maxAngleShoot)
                    {
                        //Instanciamos el prefab de impacto en el lugar donde hemos golpeado, normalizándolo con la cara impactada
                        GameObject bulletHole =Instantiate(_impactPrefab, isHit.point, Quaternion.LookRotation(isHit.normal));

                        Rigidbody rb = isHit.collider.GetComponent<Rigidbody>();
                        if (rb)
                        {
                            //Hemos impactado en algo con RigidBody, le damos un empujón (con la fuerza de impacto de nuestro disparo)
                            rb.AddForceAtPosition(dirMouseFromDrone * _forceBullet, isHit.point, ForceMode.Impulse);
                            bulletHole.transform.SetParent(rb.transform);
                        }
                    }
                }
            }



        }
    }

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(_ray.origin, _ray.direction * 1000);
    }

    private void FixedUpdate()
    {
        _rig.MovePosition(transform.position + _move * Time.fixedDeltaTime);
        _rig.MoveRotation(Quaternion.Euler(0, _rotationY, 0));
    }
}
