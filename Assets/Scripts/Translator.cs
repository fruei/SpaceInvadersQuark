using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Translator : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    public float speed;

    private void Awake()
    {
        transform.position = new Vector2(Random.Range(-7f, 7f), 0);
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        var movement = new Vector2(moveHorizontal, 0f);
        /// Como deberia plantear mi test en caso de que
        /// la multiplicacion del movimiento por la velocidad se diera aqui ?
        /// Move(movement * speed)
        /// Hasta donde creo esto se saltearia los test
        if(moveHorizontal > 0f || moveHorizontal < 0f) Move(movement);
    }

    public void Move(Vector2 velocity)
    {
        /// La multiplicacion del movimiento por la velocidad
        /// para que los test pasen teniendo en cuenta la variacion
        /// del desplazamiento
        /// deberia aplicarse aqui
        _rigidBody.velocity = velocity * speed;
    }

    public void StartDroppingDownVelocityToZeroAfterSeconds(float seconds)
    {
        StartCoroutine(DroppingDownVelocityToZeroAfterSeconds(seconds));
    }

    IEnumerator DroppingDownVelocityToZeroAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _rigidBody.velocity = Vector2.zero;
    }
}