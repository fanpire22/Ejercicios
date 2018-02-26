using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Empty Item", menuName = "NonGravityItem")]
public class NonGravityItem : Item {

public override void ApplyEffects()
    {
        Rigidbody2D rb = GameManager.instance.getSimonSimon().GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.1f;
        rb.velocity = Vector2.up * 2.0f;
    }
}
