using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    void SetRandomColor()
    {
        this.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
