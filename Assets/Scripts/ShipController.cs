using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Vector2 movement;

    public float speed;
    public float maxSpeed;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(Random.Range(-7f,7f), 0);
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        movement = new Vector2(moveHorizontal, 0f);
    }

    private void FixedUpdate()
    {
        if(rigidBody.velocity.magnitude <= maxSpeed)
            rigidBody.AddForce(movement * speed * 5f);
    }

}
