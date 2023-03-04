using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockbackable
{
    public void Knockback(Rigidbody2D rigidbody2D, Vector2 direction, float strength);
}
