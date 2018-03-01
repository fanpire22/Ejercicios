using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FDamageWeapon
{
    public EDamageTypes type;
    public int amount;
}

public class WeaponBase : MonoBehaviour
{
    public List<FDamageWeapon> damages = new List<FDamageWeapon>();
    [SerializeField] LayerMask LayerDetection;
    [SerializeField] bool bCanDamageOwner;
    [SerializeField] float LifeDuration = 10f;
    public float SpawnOffset = 0.5f;

    Collider2D _detectionCollider;
    GameObject _owner;
    protected float _direction { get; private set; }

    private void Awake()
    {
        _detectionCollider = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        Destroy(gameObject, LifeDuration);
    }

    /// <summary>
    /// Instanciamos el proyectil, y marcamos su dueño
    /// </summary>
    /// <param name="Owner"></param>
    public void Initialize(GameObject Owner, float Direction = -1.0f)
    {
        _owner = Owner;
        _direction = Direction;
    }

    /// <summary>
    /// Control de las físicas
    /// </summary>
    private void FixedUpdate()
    {
        Collider2D[] colliders = new Collider2D[5];

        //Creamos un sistema de filtros para el tipo "Damageable"
        ContactFilter2D Params = new ContactFilter2D();
        Params.SetLayerMask(LayerDetection);

        int count = Physics2D.OverlapCollider(_detectionCollider, Params, colliders);


        if (count > 0)
        {
            List<HealthManager> aux = new List<HealthManager>();
            for (int i = 0; i < colliders.Length; i++)
            {
                //Si es un enemigo, o bien es el dueño y puedo dañar al dueño, entonces le añado a objetos a herir
                if ((colliders[i] != null) && (colliders[i].gameObject != _owner || (colliders[i].gameObject == _owner && bCanDamageOwner)))
                {
                    //Por si acaso, preguntamos si tiene el componente "HealthManager", que si no, no se le puede dañar
                    HealthManager auxH = colliders[i].GetComponent<HealthManager>();
                    if (auxH)
                        aux.Add(auxH);
                }
            }

            OnHealthOverlap(aux);
        }
    }

    /// <summary>
    /// Función que se llama cuando se colisiona con un objetivo
    /// </summary>
    protected virtual void OnHealthOverlap(List<HealthManager> colliders)
    {
    }

    /// <summary>
    /// Las clases hijas deben implementar el comportamiento específico de cada arma aquí
    /// </summary>
    protected virtual void Update()
    {

    }
}
