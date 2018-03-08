using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour {

    [SerializeField] string _targetScene;
    [SerializeField] Vector3 FinalLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        SceneManager.LoadSceneAsync(_targetScene,LoadSceneMode.Additive);
    }

    IEnumerator LoadScene_Corrutine()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(_targetScene, LoadSceneMode.Additive);
        ao.completed += Ao_completed;
        yield return new WaitUntil(() => { return ao.isDone; }) ;
    }

    private void Ao_completed(AsyncOperation obj)
    {
        GameManager.instance.getSimonSimon().rig.MovePosition(FinalLocation);
    }
}
