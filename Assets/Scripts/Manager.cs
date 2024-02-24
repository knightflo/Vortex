using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class responsible for managing events and providing access to singleton classes through a centralized interface.
/// </summary>
public class Manager : MonoBehaviour
{
    public static UserInput userInput;

    private void Start()
    {
        userInput = new UserInput();
        userInput.Player.Movement.Enable();
        userInput.Player.Aiming.Enable();

        // userInput.Player.Movement.ReadValue<Vector2>();
    }
}
