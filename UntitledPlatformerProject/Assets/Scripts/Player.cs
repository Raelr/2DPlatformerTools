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

        if (controller.collisionInformation.isAbove || controller.collisionInformation.isBelow) {
            playerVelocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisionInformation.isBelow) {

            playerVelocity.y = controller.JumpVelocity;

        }

        float targetVelocityX = input.x * controller.MoveSpeed;
        float xSmoothing = controller.VelocityXSmoothing;
        playerVelocity.x = Mathf.SmoothDamp(playerVelocity.x, targetVelocityX, ref xSmoothing, controller.collisionInformation.isBelow ? controller.AccelerationTimeGrounded: controller.AccelerationTimeAirborn);
        playerVelocity.y += controller.Gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

	}
}
