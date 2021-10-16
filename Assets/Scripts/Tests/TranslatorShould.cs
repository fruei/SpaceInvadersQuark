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
        yield return null;

        /// Act
        trans.Move(Vector2.right);

        yield return new WaitForSeconds(1f);

        /// Assert
        Assert.AreEqual(oldPos.y, go.transform.position.y);
        Assert.IsTrue(Mathf.Approximately(1f, go.transform.position.x), "transform.position.x is not 1f as expected");
    }

    [UnityTest]
    public IEnumerator Move_DiagonalMovementVector_TheGameObjectMoveAsExpected()
    {
        yield return null;

        /// Act
        trans.Move(new Vector2(1f, 1f));

        yield return new WaitForSeconds(1f);

        /// Assert
        bool result = Mathf.Approximately(1f, go.transform.position.y) && Mathf.Approximately(1f, go.transform.position.x) ? true : false;
        Assert.IsTrue(result, $"transform.position is not 1f, 1f as expected, instead was {go.transform.position.x} {go.transform.position.y}");
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
