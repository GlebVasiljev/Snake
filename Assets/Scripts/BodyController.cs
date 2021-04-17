using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AppleCollider");

        if (collision.tag == "Apple" && !SnakeControll.IsApple)
        {
            GameObject.Destroy(collision.gameObject);
            SnakeControll.IsApple = true;
            AppleController.CreateApple();
            // Debug.Log("AppleCollider");
            return;
        }
    }

}
