using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (RagDoll))]
public class Soldier : MonoBehaviour
{

    [SerializeField] Transform _skull;
    [SerializeField] Transform _boobies;

    RagDoll _rag;
    protected NavMeshAgent _agent;
    protected Animator _ani;

    protected bool _isDead = false;

    protected virtual void Awake()
    {
        _rag = GetComponent<RagDoll>();
        _agent = GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
    }

    /// <summary>
    /// Determina si tenemos a un Soldado dentro de nuestro ángulo de tiro (de la cabeza al pecho)
    /// </summary>
    /// <param name="Target"></param>
    /// <returns></returns>
    protected virtual bool HasLineOfSightToSoldier(Soldier Target)
    {
        Vector3 rayCastOrigin = _skull.position;
        Vector3 rayCastDirection = Target._boobies.position -_skull.position;

        RaycastHit hitInfo;
        Debug.DrawLine(rayCastOrigin, Target._boobies.position, Color.red);

        if (Physics.Raycast(rayCastOrigin, rayCastDirection, out hitInfo))
        {
            //Debug.Log(hitInfo.collider.name + " of " + hitInfo.collider.transform.root.name);
            Soldier gotYouSoldier = hitInfo.collider.GetComponentInParent<Soldier>();

            if (gotYouSoldier != null && gotYouSoldier == Target)
            {
                return true;
            }
        }

        return false;

    }

    protected virtual void Update()
    {

    }

    public virtual void OnDeath()
    {
        _isDead = true;
        _agent.enabled = false;
        _rag.ActivateRagDoll();
    }
}
