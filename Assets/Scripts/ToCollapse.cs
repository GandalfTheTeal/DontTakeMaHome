using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToCollapse : MonoBehaviour, ICollapsible
{

	private static float _MaxForce = 200;

    public void Collapse()
    {
        Rigidbody2D _RB = GetComponent<Rigidbody2D>() != null ? GetComponent<Rigidbody2D>() : gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
		Lifetime time = gameObject.AddComponent(typeof(Lifetime)) as Lifetime;

		foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
		{
			col.enabled = false;
		}

		foreach (var item in GetComponentsInChildren<SpriteRenderer>())
		{
			item.sortingLayerID = 0;
		}

		_RB.AddRelativeForce(Vector2.up * Random.Range(10,_MaxForce));
    }
}
