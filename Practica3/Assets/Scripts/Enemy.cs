using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eStatus
{
    chill,
    wary,
    alert
}

public class Enemy : Soldier
{ 
    [Header("Propiedades de Alerta")]
    [SerializeField] float _viewAngle = 45;
    [SerializeField] float _viewDistance = 4;

    [SerializeField] float _alertTime = 5;
    [SerializeField] Color _chillColor = new Color(0.2594f, 1, 0.2594f, 1);
    [SerializeField] Color _waryColor = new Color(1, 0.8078f, 0, 1);
    [SerializeField] Color _alertColor = new Color(1, 0.2594f, 0.2594f, 1);

    [Header("Propiedades de Movimiento")]
    [SerializeField] Transform _pathRoot;
    Transform[] _pathPoints;
    int _curPathPoint = 0;
    
    private Projector _vCone;
    private Quaternion _OriginalRotation;
    private Vector3 _lastKnownPlayerLoc;
    private float _alertEndTime;
    float _agentSpeed;

    protected override void Awake()
    {
        base.Awake();

        _agentSpeed = _agent.speed;

        _vCone = GetComponentInChildren<Projector>();

        if (_pathRoot != null && _pathRoot.childCount > 0)
        {
            _pathPoints = new Transform[_pathRoot.childCount];
            for (int i = 0; i < _pathPoints.Length; i++)
            {
                _pathPoints[i] = _pathRoot.GetChild(i);
            }
        }
    }

    private void Start()
    {
        _OriginalRotation = transform.rotation;
        InitializeProjector();
        StartPath();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        SetViewColor(eStatus.chill);
        UpdatePath();
        UpdatePlayerRotation();
        UpdatePlayerLOS();
    }

    private void StartPath()
    {
        if(_pathPoints != null)
        {
            _agent.SetDestination(_pathPoints[_curPathPoint].position);
        }
    }
    
    public void SetLastKnownPlayerPos(Vector3 position)
    {
        _lastKnownPlayerLoc = position;
        _alertEndTime = Time.time + _alertTime;

        _agent.speed = 0;
    }

    private void SetViewColor(eStatus status)
    {
        switch (status)
        {
            case eStatus.chill:
                _vCone.material.SetColor("_Color", _chillColor);
                break;
            case eStatus.wary:
                _vCone.material.SetColor("_Color", _waryColor);
                break;
            case eStatus.alert:
                _vCone.material.SetColor("_Color", _alertColor);
                break;
            default:
                _vCone.material.SetColor("_Color", _chillColor);
                break;
        }
    }

    private void InitializeProjector()
    {
        //Creamos una versión del material por instancia de enemigo. Esto nos permite cambiar conos si se alertan, 
        //pero consume muchos recursos si hay muchos enemigos. En ese caso, puede ser más eficiente introducir 
        //un material por cada variación posible y asignarlos en un array
        _vCone.material = new Material(_vCone.material);
        _vCone.orthographicSize = _viewDistance;
        _vCone.material.SetFloat("_Angle", _viewAngle / 2);
        SetViewColor(eStatus.chill);
    }

    private void ShootPlayer()
    {
        GameManager.GetInstance.player.OnDeath();
    }


    private void OnTriggerStay(Collider other)
    {
        SetLastKnownPlayerPos(other.bounds.center);
    }

    private void UpdatePlayerRotation()
    {
        if (_agent.speed == 0)
        {
            if (Time.time < _alertEndTime)
            {
                SetViewColor(eStatus.wary);
                RotateTowardsPosition(_lastKnownPlayerLoc);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _OriginalRotation, _agent.angularSpeed * Time.deltaTime);


                _agent.speed = _agentSpeed;
            }
        }

    }

    private void UpdatePlayerLOS()
    {

        Player player = GameManager.GetInstance.player;
        Vector3 playerPosition = player.transform.position;
        Vector3 directionToPlayer = playerPosition - transform.position;

        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle < _viewAngle / 2)
        {
            if (directionToPlayer.magnitude < _viewDistance)
            {
                //Tenemos al jugador dentro del cono. Si lo vemos, pasamos el cono a otro color
                if (HasLineOfSightToSoldier(GameManager.GetInstance.player))
                {
                    SetViewColor(eStatus.alert);
                    SetLastKnownPlayerPos(player.transform.position);

                    if (!IsInvoking("ShootPlayer"))
                    {
                        _ani.SetBool("Shoot", true);
                        Invoke("ShootPlayer", 3);
                    }
                }
                else
                {
                    _ani.SetBool("Shoot", false);
                    SetViewColor(eStatus.wary);
                    CancelInvoke("ShootPlayer");
                }
            }
            else
            {
                _ani.SetBool("Shoot", false);
                CancelInvoke("ShootPlayer");
            }
        }
    }

    void UpdatePath()
    {
        if (_agent.speed == 0) return;

        if(!_agent.pathPending && _agent.remainingDistance < 0.2f)
        {
            _curPathPoint = (_curPathPoint + 1) % _pathPoints.Length;
            _agent.SetDestination(_pathPoints[_curPathPoint].position);
        }
    }

    void RotateTowardsPosition(Vector3 position)
    {
        Vector3 directionToPosition = position - this.transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(directionToPosition);
        this.transform.rotation = Quaternion.RotateTowards(
            this.transform.rotation,
            desiredRotation,
            _agent.angularSpeed * Time.deltaTime);
    }
}
