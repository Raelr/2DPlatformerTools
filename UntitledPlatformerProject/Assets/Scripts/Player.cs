using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player Controller")]
    [SerializeField]
    Controller2D controller;

	void Start () {

        controller = GetComponent<Controller2D>();
	}

	void Update () {
        // Apply gravity by default
        controller.ApplyGravity();

        // Get the current axis values and add them to a vector.
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        if (Input.GetKeyDown(KeyCode.Space)) {

            controller.Jump();
        }

        // Use the input vector to move the player.
        controller.MoveHorizontal(input);
    }
}
