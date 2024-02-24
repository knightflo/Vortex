using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private float movementSpeed = 5;

    private void Update()
    {
        var userMovementInput = Manager.userInput.Player.Movement.ReadValue<Vector2>();
        float horizontalMovement = Mathf.Sign(transform.right.x);

        rigidbody.velocity = new Vector2(horizontalMovement * movementSpeed * userMovementInput.x * Time.deltaTime, rigidbody.velocity.y);
    }
}
