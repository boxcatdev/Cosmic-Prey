using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerWithShip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            other.gameObject.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.transform.SetParent(null);
    }
}