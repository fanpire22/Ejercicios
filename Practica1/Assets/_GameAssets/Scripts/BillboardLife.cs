using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardLife : HUDLifeBar {

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update () {

        Vector3 direction = player.position - transform.position;

        transform.rotation = Quaternion.LookRotation(direction.normalized);
    }
}
