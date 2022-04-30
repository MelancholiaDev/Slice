using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckKnife : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            transform.parent.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
