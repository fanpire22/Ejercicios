using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Soldier
{


    #region Editor properties

    [SerializeField] float _alertSpeedScale = 2;

    [SerializeField] Transform _attackIcon;
    [SerializeField] Transform _forbiddenIcon;

    [SerializeField] GameObject _aimLaser;

    #endregion

    #region Component references

    Projector _alertAreaProjector;
    SphereCollider _alertAreaCollider;

    #endregion

    #region State variables

    bool _isIdle;
    bool _isAiming;
    Enemy _targetEnemy;
    bool _isInteracting;
    bool _canInteractCancel;
    Interactive _targetInteract;
    InteractionIK _interactionIK;

    #endregion

    #region Events

    public Action OnShoot;
    public Action OnDie;

    #endregion

    protected override void Awake()
    {
        base.Awake();

        _alertAreaProjector = GetComponentInChildren<Projector>();
        _alertAreaCollider = _alertAreaProjector.GetComponent<SphereCollider>();
        _interactionIK = GetComponent<InteractionIK>();

        _aimLaser.SetActive(false);
        _attackIcon.gameObject.SetActive(false);
        _forbiddenIcon.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        BeginIdle();
    }

    #region Update

    protected override void Update()
    {
        base.Update();

        UpdateIdle();
        UpdateAiming();

        UpdateAlertArea();
        UpdateInput();
    }

    void UpdateAlertArea()
    {
        _alertAreaProjector.orthographicSize = _agent.velocity.magnitude * _alertSpeedScale;
        _alertAreaCollider.radius = _agent.velocity.magnitude * _alertSpeedScale;
    }


    void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animator.SetTrigger("crouch");
        }
    }

    #endregion

    #region State: Idle

    void BeginIdle()
    {
        if (!_isIdle)
        {
            _isIdle = true;

            EndAiming();
        }
    }

    void UpdateIdle()
    {
        if (_isIdle)
        {

        }
    }

    void EndIdle()
    {
        if (_isIdle)
        {
            _isIdle = false;
        }
    }

    #endregion

    #region State: Aiming

    void BeginAiming()
    {
        if (!_isAiming)
        {
            _isAiming = true;

            EndIdle();

            _agent.SetDestination(this.transform.position);
            _aimLaser.SetActive(true);
            _animator.SetBool("aiming", true);
        }
    }

    void UpdateAiming()
    {
        if (_isAiming && _targetEnemy != null)
        {
            Vector3 directionToEnemy = _targetEnemy.transform.position - this.transform.position;
            Quaternion desiredRotation = Quaternion.LookRotation(directionToEnemy);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, desiredRotation, _agent.angularSpeed * Time.deltaTime);

            float angle = Vector3.Angle(this.transform.forward, directionToEnemy);

            RaycastHit hitInfo;
            if (angle < 1 && RaycastToSoldier(_targetEnemy, out hitInfo))
            {


                Soldier hittedSoldier = hitInfo.collider.GetComponentInParent<Soldier>();
                if (hittedSoldier != null && hittedSoldier == _targetEnemy)
                {
                    Vector3 crosshairPosition = _targetEnemy.crosshairTarget.position;
                    Vector3 screenPoint = Camera.main.WorldToScreenPoint(crosshairPosition);

                    _forbiddenIcon.gameObject.SetActive(false);
                    _attackIcon.gameObject.SetActive(true);
                    _attackIcon.transform.position = screenPoint;
                }
                else
                {
                    Vector3 hitPoint = hitInfo.point;
                    Vector3 screenPoint = Camera.main.WorldToScreenPoint(hitPoint);

                    _attackIcon.gameObject.SetActive(false);
                    _forbiddenIcon.gameObject.SetActive(true);
                    _forbiddenIcon.transform.position = screenPoint;
                }
            }
            else
            {
                _attackIcon.gameObject.SetActive(false);
                _forbiddenIcon.gameObject.SetActive(false);
            }


        }
    }

    void EndAiming()
    {
        if (_isAiming)
        {
            _isAiming = false;
            _aimLaser.SetActive(false);

            _attackIcon.gameObject.SetActive(false);
            _forbiddenIcon.gameObject.SetActive(false);
            _targetEnemy.DeactivateSelection();

            _animator.SetBool("aiming", false);

            _targetEnemy = null;
        }
    }

    #endregion

    #region Input actions

    public void GoToDestination(Vector3 destination)
    {
        if (_isDead) { return; }

        BeginIdle();

        _agent.SetDestination(destination);
    }

    public void AimAtTargetEnemy(Enemy enemy)
    {
        if (_isDead) { return; }

        if (_targetEnemy != null)
        {
            _targetEnemy.DeactivateSelection();
        }
        _targetEnemy = enemy;
        _targetEnemy.ActivateSelection();

        BeginAiming();
    }

    public void ShootAtTarget()
    {
        if (_isDead) return;

        if (HasLineOfSightToSoldier(_targetEnemy) && _attackIcon.gameObject.activeInHierarchy)
        {
            _targetEnemy.Die();

            BeginIdle();
        }
        else
        {
            // Dar feedback sobre el hecho de no poder disparar
        }
    }

    public override void Die()
    {
        EndAiming();
        base.Die();

        //Si el evento está siendo escuchado, lo lanzamos
        if (OnDie != null)
        {
            OnDie.Invoke();
        }
    }

    #endregion

    #region Interactive

    public void InteractWithItem(Interactive item)
    {
        _targetInteract = item;

        BeginInteracting();
    }

    void BeginInteracting()
    {
        if (!_isInteracting)
        {
            _isInteracting = true;

            EndIdle();
            EndAiming();

            StartCoroutine(UpdateInteractingCoroutine());
        }

    }

    IEnumerator UpdateInteractingCoroutine()
    {
        //Nos acercamos a la posición deseada
        _agent.SetDestination(_targetInteract.InteractionPoint.position);
        while (_agent.pathPending || _agent.remainingDistance > 0.1)
        {
            yield return null;
        }

        //Hemos llegado al objeto: Ya no podemos cancelar
        _canInteractCancel = false;

        //Determinamos hacia dónde queremos girar y vamos rotando
        Vector3 desiredForward = Vector3.ProjectOnPlane(_targetInteract.InteractionPoint.forward, Vector3.up);
        Vector3 myForward = Vector3.ProjectOnPlane(this.transform.forward, Vector3.up);
        float angle = Vector3.Angle(myForward, desiredForward);
        while (angle > 0.1)
        {
            this.transform.forward = Vector3.RotateTowards(myForward, desiredForward, Mathf.Deg2Rad * _agent.angularSpeed * Time.deltaTime, 0);

            yield return null;

            myForward = Vector3.ProjectOnPlane(this.transform.forward, Vector3.up);
            angle = Vector3.Angle(myForward, desiredForward);
        }

        _interactionIK.ActivateIK(_targetInteract.Handler);
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(_targetInteract.ActivateInteractionCoRoutine());
        EndInteracting();
    }

    void EndInteracting()
    {
        StopCoroutine(UpdateInteractingCoroutine());

        if(_isInteracting)
        {
            _isInteracting = false;
            _interactionIK.DisableIK();

            BeginIdle();
        }
    }

    #endregion



}
