using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

    Controller2D controller;

    Vector3 playerVelocity;

	void Start () {

        controller = GetComponent<Controller2D>();
	}

	void Update () {

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        playerVelocity.x = input.x * controller.MoveSpeed * Time.deltaTime;
        playerVelocity.y += controller.Gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
	}
}
