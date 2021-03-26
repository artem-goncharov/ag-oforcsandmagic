using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderManage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.gameObject.name + " destroyed.");
        Destroy(collision.gameObject);
    }
}
