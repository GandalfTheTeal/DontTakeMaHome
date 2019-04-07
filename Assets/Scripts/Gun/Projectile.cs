using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _damage = 50f;

    [SerializeField]
    private string[] _tags; // Tag for the projectile to hit

    [SerializeField]
    private float _speed = 10f;

    private IHittable _hittable = null; // To store the hittable object


    public void Shoot(Vector2 velocity) // Applies the direction the gun is facing as a velocity multiplied a speed
    {
        Rigidbody2D _RB = GetComponent<Rigidbody2D>();
        _RB.velocity = velocity * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Makes sure the collision object is hittable
        if ((other.gameObject.GetComponent<IHittable>() != null && _tags.Contains(other.gameObject.tag)))
        {
            _hittable = other.gameObject.GetComponent<IHittable>(); // Sets hittable to whatever it collided with if it's a hittable object
            _hittable.TakeDamage(_damage);
            Destroy(gameObject); // Destroys itself on contact with anything
        }
    }
}
