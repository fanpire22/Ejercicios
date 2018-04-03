using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAudios : MonoBehaviour {

    [SerializeField] AudioClip _defaultStepAudio;

    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Step(AnimationEvent animEvent)
    {   
        // Booleano para saber si el clip de animación que ha generado 
        // el evento tiene más peso que el resto de clips del blend tree
        bool eventClipHasMoreWeight = true;

        // Accedo a todos los clips de animación que tiene el blend tree actualmente
        AnimatorClipInfo[] currentClipsArray = _animator.GetCurrentAnimatorClipInfo(0);
        for(int i=0; i<currentClipsArray.Length; i++)
        {
            AnimatorClipInfo currentClip = currentClipsArray[i];

            // Si algún clip de animación del blendTree tiene más peso
            // que el clip de animación que generó el evento, pongo el booleano a false
            if(currentClip.weight > animEvent.animatorClipInfo.weight)
            {
                eventClipHasMoreWeight = false;
                break;
            }
        }

        // Si ningún otro clip de animación tenía más peso que el clip 
        // que generó el evento, entonces, reproduzco el audio
        if (eventClipHasMoreWeight)
        {
            Debug.Log("AUDIO PASO " + animEvent.animatorClipInfo.clip.name + "/" + animEvent.animatorClipInfo.weight);

            // Compruebo si el evento lleva asociado un clip de audio como parámetro
            AudioClip stepAudioClip = animEvent.objectReferenceParameter as AudioClip;
            if(stepAudioClip != null)
            {
                AudioSource.PlayClipAtPoint(stepAudioClip, this.transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(_defaultStepAudio, this.transform.position);
            }            
        }
    }
}
