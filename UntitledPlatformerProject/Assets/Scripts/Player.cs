using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

    Controller2D controller;

	void Start () {

        controller = GetComponent<Controller2D>();
	}

	void Update () {

        controller.ApplyGravity();

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        controller.MoveHorizontal(input);

        if (Input.GetKeyDown(KeyCode.Space)) {

            controller.Jump();

        }
    }
}
