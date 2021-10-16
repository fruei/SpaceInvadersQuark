using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Translator : MonoBehaviour
{
    Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public void StartDroppingDownVelocityToZeroAfterSeconds(float seconds)
    {
        StartCoroutine(DroppingDownVelocityToZeroAfterSeconds(seconds));
    }

    IEnumerator DroppingDownVelocityToZeroAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        rb.velocity = Vector2.zero;
    }
}