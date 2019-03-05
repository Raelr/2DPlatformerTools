using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player Controller")]
    [SerializeField]
    Controller2D controller;

    Vector3 input;

    // Delegate for anything which needs to know whether the player is moving
    public delegate void PlayerMovedHandler();

    public event PlayerMovedHandler playerMoved;

    void Start() {

        controller = GetComponent<Controller2D>();
    }

    void Update() {

        MoveByInput();
    }

    /// <summary>
    /// Maps player inputs into movement and calls appropriate commands from the controller.
    /// </summary>

    void MoveByInput() {

        // Get the current axis values and add them to a vector.
        FindAxes();

        if (IsStill() && !SpacePressed()) {
            // If we arent moving then just apply gravity normally.
            controller.ApplyGravity(ref input, true);

        } else {

            controller.ApplyGravity(ref input);

            if (SpacePressed()) {

                controller.Jump(ref input);
            }

            controller.ApplyMovement(input);
        }

        // If anything is listening for player movement then invoke the delegate.
        if (playerMoved != null) {

            playerMoved.Invoke();

        }
    }

    /// <summary>
    /// Modifies the input vector to have the horizontal and vertical axes values.
    /// </summary>

    void FindAxes() {

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.z = 0;
    }

    bool SpacePressed() {
        return Input.GetKeyDown(KeyCode.Space);
    }

    bool IsStill() {
        return input.x == 0 && input.y == 0;
    }
}
