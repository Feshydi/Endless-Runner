using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackingBehaviour : IKnockbackable
{
    public void Knockback(Rigidbody2D rigidbody2D, Vector2 direction, float strength)
    {
        rigidbody2D.AddForce(direction * strength, ForceMode2D.Impulse);
    }
}
