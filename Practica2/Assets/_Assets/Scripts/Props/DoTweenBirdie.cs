using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenBirdie : MonoBehaviour
{

    [SerializeField] Transform PositionA;
    [SerializeField] Transform PositionB;
    [SerializeField] Transform Message;
    [SerializeField] bool _bSecuenceMode;

    SpriteRenderer sprtMe;

    private void Start()
    {
        sprtMe = GetComponent<SpriteRenderer>();
        Message.localScale = Vector3.zero;

        ////Movemos el pajarraco por Tweening

        if (_bSecuenceMode)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOMove(PositionB.position, 5).OnComplete(()=> { sprtMe.flipX = true; }))
            .Append(Message.DOScale(Vector3.one, 1))
            .Append(Message.DOScale(Vector3.zero, 1))
            .Append(transform.DOMove(PositionA.position, 5).OnComplete(() => { sprtMe.flipX = false; }));

            sequence.SetLoops(-1).Play();
        }
        else
        {
            transform.DOMove(PositionB.position, 5).OnComplete(() =>
            {
                //Hacemos que el mensaje se incremente desde cero, se muestre un segundo y se vuelva a ocultar con animación
                Message.DOScale(Vector3.one, .5f).SetDelay(1).DOTimeScale(0, .5f).OnComplete(() =>
                    {
                        //Hacemos que vuelva otra vez a la posición inicial. Lo giramos
                        sprtMe.flipX = true;
                        transform.DOMove(PositionA.position, 5).OnComplete(() =>
                        {
                            sprtMe.flipX = false;
                        });
                    });
            }).SetLoops(-1);
        }

    }

}
