using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class TranslatorShould
{
    GameObject go;
    Rigidbody2D rb;
    Translator trans;
    Vector3 oldPos;

    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        rb = go.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        trans = go.AddComponent<Translator>();
        oldPos = go.transform.position;
        trans.speed = 2f;
    }

    [UnityTest]
    public IEnumerator Move_ZeroMovementVector_TheGameObjectStaysInPlace()
    {
        yield return null; //Wait for Start Event

        /// Act
        trans.Move(Vector2.zero);

        yield return new WaitForSeconds(1f);

        /// Assert
        Assert.AreEqual(oldPos, go.transform.position);
    }

    [UnityTest]
    public IEnumerator Move_NonZeroMovementVector_TheGameObjectMovesAsExpected()
    {
        /// Arrange
        /// Preguntar:
        /// Al comenzar a escribir los test
        /// el expected siempre fue una constante, esto no tenia en cuenta
        /// que al prender el juego en Awake seteariamos una posicion aleatoria
        /// por lo tanto el expected tendria que ser la posicion actual luego 
        /// setear la posicion aleatoria, mas el incremento a testear
        /// En que parte del ciclo de la creacion de Tests debia tenerse en cuenta
        /// el desplazamiento en unidades ?
        /// o se deberia haber creado un nuevo test ?
        /// que deberia pasar con este Test que consideraba el desplazamiento solo
        /// en una unidad basada en el World Space ?
        /// La misma pregunta aplica a la variacion cuando se le aplica una velocidad
        /// que multiplica el movimiento
        yield return null;
        var expected = oldPos.x + Vector2.right.x * trans.speed;

        /// Act
        trans.Move(Vector2.right);

        yield return new WaitForSeconds(1f);

        /// Assert
        Assert.IsTrue(Mathf.Approximately(expected, go.transform.position.x), $"transform.position.x is not {expected} as expected, instead was {go.transform.position.x}");
    }

    [UnityTest]
    public IEnumerator Move_DiagonalMovementVector_TheGameObjectMoveAsExpected()
    {
        /// Arrange
        /// /// Preguntar:
        /// Al comenzar a escribir los test
        /// el expected siempre fue una constante, esto no tenia en cuenta
        /// que al prender el juego en Awake seteariamos una posicion aleatoria
        /// por lo tanto el expected tendria que ser la posicion actual luego 
        /// setear la posicion aleatoria, mas el incremento a testear
        /// En que parte del ciclo de la creacion de Tests debia tenerse en cuenta
        /// el desplazamiento en unidades ?
        /// o se deberia haber creado un nuevo test ?
        /// que deberia pasar con este Test que consideraba el desplazamiento solo
        /// en una unidad basada en el World Space ?
        /// La misma pregunta aplica a la variacion cuando se le aplica una velocidad
        /// que multiplica el movimiento
        yield return null;
        var expected = new Vector2(1f * trans.speed + oldPos.x, 1f * trans.speed + oldPos.y);

        /// Act
        trans.Move(new Vector2(1f, 1f));

        yield return new WaitForSeconds(1f);

        /// Assert
        Assert.IsTrue(Mathf.Approximately(expected.x, go.transform.position.x), $"transform.position X is not {expected.x} as expected, instead was {go.transform.position.x}");
        Assert.IsTrue(Mathf.Approximately(expected.y, go.transform.position.y), $"transform.position Y is not {expected.y} as expected, instead was {go.transform.position.y}");
    }

    #region This Tests Validates that after N Seconds the velocity does not change
    //static float[] seconds = new float[] { 1f, 5f, 6f };
    //[UnityTest]
    //public IEnumerator GivenNonZeroMovementVectorAfterNSecondsTheGameObjectVelocityDontChange([ValueSource("seconds")] float seconds)
    //{
    //    yield return null;

    //    trans.Move(Vector2.right);

    //    yield return new WaitForSeconds(1f);

    //    float xVel = rb.velocity.x;

    //    yield return new WaitForSeconds(seconds);

    //    Assert.AreEqual(xVel, rb.velocity.x);
    //}
    #endregion

    static float[] seconds = new float[] { 2f, 5f, 8f };
    [UnityTest]    
    public IEnumerator Move_NonZeroMovementVector_AfterNSecondsTheGameObjectVelocityDroppedDownToZero([ValueSource("seconds")] float seconds)
    {
        /// Arrange
        yield return null;
        trans.Move(Vector2.left);

        /// Act
        trans.StartDroppingDownVelocityToZeroAfterSeconds(seconds);

        yield return new WaitForSeconds(seconds);
        
        /// Assert
        Assert.IsTrue(Mathf.Approximately(rb.velocity.x,0f), $"After {seconds} seg the velocity was {rb.velocity.x}");
    }
}
