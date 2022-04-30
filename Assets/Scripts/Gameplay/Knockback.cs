using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private KnifeMovement movementScript;
    private float knockbackDuration;
    
    private void Awake() {
        movementScript = transform.parent.gameObject.GetComponent<KnifeMovement>();
    }

    private void OnCollisionEnter(Collision other) {
        print("a");
        movementScript.RotateBlade(knockbackDuration, true);
    }
}
