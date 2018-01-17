using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class Catapult : MonoBehaviour
{

    [Header("Game Objects")]
    //Rock thrown
    [SerializeField]
    private GameObject _prefRock;
    [SerializeField] private GameObject _rockSpawnEmission;
    //ThrowCam: Camera that follows the rock when it is thrown
    [SerializeField] private FreeLookCam _throwCam;
    [SerializeField] private ProtectCameraFromWallClip _followCam;
    //The catapult arm
    [SerializeField] private HingeJoint _joint;
    //Rock Spawner (where the rock should spawn)
    [SerializeField] private Transform _rockSpawner;
    //Wheels
    [SerializeField] private FWheels _wheels;

    [Header("Properties")]
    [SerializeField]
    private KeyCode _shootKey = KeyCode.Space;
    [SerializeField] private KeyCode _reload = KeyCode.LeftControl;
    [SerializeField] private float _maxSpeed = 600;
    [SerializeField] private float _maxRotation = 50;


    //Wheels Struct
    [System.Serializable]
    struct FWheels
    {
        public WheelCollider FrontRightCollider;
        public WheelCollider FrontLeftCollider;
        public WheelCollider BackRightCollider;
        public WheelCollider BackLeftCollider;

        public Transform FrontRightTransform;
        public Transform FrontLeftTransform;
        public Transform BackRightTransform;
        public Transform BackLeftTransform;

        public WheelCollider[] GetColliders()
        {
            return new WheelCollider[]
            {
                FrontRightCollider,FrontLeftCollider,BackRightCollider, BackLeftCollider
            };
        }

        public Transform[] GetTransforms()
        {
            return new Transform[]
            {
                FrontRightTransform,FrontLeftTransform,BackRightTransform, BackLeftTransform
            };
        }

    }

    private Rigidbody _catapultRB;
    private GameObject _rock;
    private bool _isReloaded = false;

    private void Awake()
    {
        _catapultRB = _joint.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        RockSpawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_shootKey))
        {
            Shoot();
        }
        if (Input.GetKeyDown(_reload))
        {
            Reload();
        }
        float currentAngleX = _joint.transform.localEulerAngles.x;
        if (currentAngleX < 0.1f && !_isReloaded)
        {
            //Tenemos la pala colocada, y se puede recargar, recargamos
            RockSpawn();
        }
        if (currentAngleX > 55 && currentAngleX < 70)
        {
            //La roca ha salido, ya se puede inicializar la recarga
            _isReloaded = false;
        }

        float vertical = Input.GetAxis("Vertical") * _maxSpeed;
        float horizontal = Input.GetAxis("Horizontal") * _maxRotation;

        _wheels.FrontRightCollider.steerAngle = horizontal;
        _wheels.FrontLeftCollider.steerAngle = horizontal;


        WheelCollider[] colliders = _wheels.GetColliders();
        Transform[] transforms = _wheels.GetTransforms();

        for (int i = 0; i < colliders.Length; i++)
        {
            WheelCollider w = colliders[i];
            Transform t = transforms[i];

            w.motorTorque = vertical;

            Vector3 wheelPosition;
            Quaternion wheelQuaternion;

            //Obtenemos la posición del collider
            w.GetWorldPose(out wheelPosition, out wheelQuaternion);
            t.position = wheelPosition;
            t.rotation = wheelQuaternion;
        }




    }

    private void Shoot()
    {

        float limitMax = _joint.limits.max;
        float currentAngleX = _joint.transform.localEulerAngles.x;

        if ((currentAngleX - limitMax) < 0.1f)
        {
            //El brazo se puede disparar
            _followCam.closestDistance = 5;
            _throwCam.SetTarget(_rock.transform);
            _joint.useMotor = true;


            Invoke("Reload", 7);
        }
    }

    private void Reload()
    {
        if (!_isReloaded)
        {
            //Como lo podremos llamar desde otros puntos de la aplicación, como un botón reload, cancelamos el invoke para que no salte dos veces
            CancelInvoke();

            _joint.useMotor = false;
            _joint.useSpring = true;
            _followCam.closestDistance = 20;
            _throwCam.SetTarget(transform);
        }
    }

    private void RockSpawn()
    {
        _joint.useSpring = false;

        //Cargamos el efecto visual del spawneo de la roca, y hacemos que se elimine al cabo de un rato
        GameObject emission = Instantiate(_rockSpawnEmission);
        emission.transform.position = _rockSpawner.position;
        Destroy(emission, 2);


        //Recargamos la roca, e indicamos que ya se ha recargado
        _rock = Instantiate(_prefRock);
        _rock.transform.position = _rockSpawner.position;
        _rock.GetComponent<FixedJoint>().connectedBody = _catapultRB;
        _isReloaded = true;
    }
}
