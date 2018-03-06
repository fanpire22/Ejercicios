﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MovingPlat : MonoBehaviour {

    [SerializeField] Transform PositionA;
    [SerializeField] Transform PositionB;

    void Start()
    {
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(PositionB.position, 5))
        .Append(transform.DOMove(PositionA.position, 5));

        sequence.SetLoops(-1).Play();
    }

    /// <summary>
    /// Hacemos al jugador hijo de la plataforma para que se mueva respecto a ella
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    /// <summary>
    /// Liberamos al objeto de ser hijo de la plataforma
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
