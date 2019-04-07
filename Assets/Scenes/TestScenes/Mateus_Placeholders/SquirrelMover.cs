using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelMover : MonoBehaviour
{
    public ItemHandler itemHandler;
    public InteractionHandler interactionHandler;


    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * 10 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * 10 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            interactionHandler.InteractAttempt(itemHandler);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (itemHandler.currentItem != null)
            {
                itemHandler.UseItem(itemHandler.currentItem);
            }
            else
            {
                Debug.Log("Item usage without item");
            }
        }

    }


    Pickup getPickupInRange()
    {
        var newitem = GameObject.FindGameObjectWithTag("Pickup");
        return newitem.GetComponent<Pickup>();
    }

}
