using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Walking,
        Jumping,
        Falling
    }

    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private float movementSpeed = 500;
    [SerializeField] private float jumpForce = 10;
    private bool isGrounded = false;
    private PlayerState playerState = PlayerState.Idle; // Can be used for animating the player later.

    private void Update()
    {
        // Move the player horizontally based on user input direction (left, right, or stationary).
        // Adjust velocity using the movement speed and the x component of user input.
        // Change the player state to Idle or Walking based on the user input direction.
        var userMovementInput = Manager.userInput.Player.Movement.ReadValue<Vector2>();
        float horizontalMovement = Mathf.Sign(transform.right.x);
        rigidbody.velocity = new Vector2(horizontalMovement * movementSpeed * userMovementInput.x * Time.deltaTime, rigidbody.velocity.y);
        playerState = userMovementInput.x != 0 ? PlayerState.Walking : PlayerState.Idle;

        // Determine jump force direction based on current velocity.
        float userMovementInputUp = Mathf.Clamp(userMovementInput.y, 0, 1); // Restrict user input to upward movement only.
        Vector2 jumpUpwardForce = new Vector2(rigidbody.velocity.x, jumpForce);
        Vector2 jumpDownwardForce = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y);

        // Apply upward force when user pressed up key and when grounded, otherwise apply downward force or stop movement.
        // Change the player state to Jumping, Falling or Walking based on grounded or not, and user input.
        if (userMovementInputUp != 0 && isGrounded)
        {
            rigidbody.velocity = jumpUpwardForce;
            playerState = PlayerState.Jumping;
        }
        else if (!isGrounded)
        {
            rigidbody.velocity = jumpDownwardForce;
            playerState = PlayerState.Falling;
        }

        Debug.Log(playerState);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = collision.gameObject.CompareTag("Ground");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = !collision.gameObject.CompareTag("Ground");
    }
}
