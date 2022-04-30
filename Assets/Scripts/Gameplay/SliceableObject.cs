using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableObject : MonoBehaviour
{
    [SerializeField] private float launchForce, launchForceMax;
    [SerializeField] private float scoreReward;
    [SerializeField] private GameObject floatingTextPrefab;

    private Rigidbody[] childRb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cutter"))
        {
            other.transform.parent.GetComponent<KnifeMovement>().ResetAngularVelocity();

            bool forward = false;
            foreach (Transform item in this.transform)
            {
                LaunchChild(forward, item);

                forward = true;

                GetComponent<BoxCollider>().enabled = false;
            }
        }

        SetScore();
        FindObjectOfType<BaseAudioPlayer>().PlayCutSound();

        Rigidbody rb = other.transform.parent.GetComponent<Rigidbody>();

        if (rb.velocity.x >.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x - .5f, rb.velocity.y);
        }
    }

    private void LaunchChild(bool forward, Transform item)
    {
        item.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        float force = Random.Range(launchForce, launchForceMax);

        if (!forward)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
        }
        else
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            rb.GetComponent<Rigidbody>().AddForce(Vector3.back * force, ForceMode.Impulse);
        }
    }

    private void SetScore()
    {
        FindObjectOfType<ScoreManager>().AddScore(scoreReward);

        var text = Instantiate(floatingTextPrefab, this.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        text.GetComponent<FloatingText>().SetText(scoreReward);
    }
}
