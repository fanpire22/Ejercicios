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

    [SerializeField] float _viewAngle = 45;
    [SerializeField] float _viewDistance = 4;

    [SerializeField] Color _chillColor = new Color(0.2594f, 1, 0.2594f, 1);
    [SerializeField] Color _waryColor = new Color(1, 0.8078f, 0, 1);
    [SerializeField] Color _alertColor = new Color(1, 0.2594f, 0.2594f, 1);

    Projector _vCone;

    private void Awake()
    {
        base.Awake();
        _vCone = GetComponentInChildren<Projector>();
    }

    private void Start()
    {
        InitializeProjector();
    }

    // Update is called once per frame
    private void Update()
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
                    if (!IsInvoking("ShootPlayer"))
                    {
                        Invoke("ShootPlayer", 1);
                    }
                }
                else
                {
                    SetViewColor(eStatus.wary);
                    CancelInvoke("ShootPlayer");
                }
            }
            else
            {
                SetViewColor(eStatus.chill);
                CancelInvoke("ShootPlayer");
            }
        }
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
}
