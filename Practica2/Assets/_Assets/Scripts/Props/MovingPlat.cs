using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat : MonoBehaviour {

    [SerializeField] Transform PositionA;
    [SerializeField] Transform PositionB;

    void Start()
    {
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        Sequence sequence = DOTween.Sequence();

        sequence.Append(rig.DOMove(PositionB.position, 5))
        .Append(rig.DOMove(PositionA.position, 5));

        sequence.SetLoops(-1).Play();
    }
}
