using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerProjectile : MonoBehaviour
{
	[SerializeField]
	private float _damage = 50f;

	[SerializeField]
	private string _tag = "Enemy"; // Tag for the projectile to hit

    [SerializeField]
    private float _speed = 10f;

	private IHittable _hittable = null; // To store the hittable object

	void FixedUpdate()
	{
		if(_hittable != null)
		{
			_hittable.TakeDamage(_damage); // Sends damage to the TakeDamage function of whatever it hit
		}
	}

    //[Command]
    public void CmdShoot(Vector2 velocity) // Applies the direction the gun is facing as a velocity multiplied a speed
    {
        Rigidbody2D _RB = GetComponent<Rigidbody2D>();
        _RB.velocity = velocity*_speed;
    }

	private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision " + collision.collider.name);

        // Makes sure the collision object is hittable
        if ((collision.gameObject.GetComponent<IHittable>() != null)) //&& (collision.gameObject.CompareTag(_tag)))
        {
            _hittable = collision.gameObject.GetComponent<IHittable>(); // Sets hittable to whatever it colided with if it's a hittable object
            _hittable.TakeDamage(_damage);
            Destroy(gameObject); // Destroys itself on contact with anything
        }

        else
        {
            Destroy(gameObject); // Destroys itself on contact with anything
            _hittable = null;
        }
    }
}
