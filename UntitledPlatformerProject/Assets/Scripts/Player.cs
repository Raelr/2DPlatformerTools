using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

    Controller2D controller;

    delegate void HorizontalMovementHandler();

    [SerializeField]
    event HorizontalMovementHandler MovedHorizontal;

    


	void Start () {

        controller = GetComponent<Controller2D>();
	}

	void Update () {

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisionInformation.isBelow) {

            controller.Jump();

        }

        controller.MoveHorizontal(input);
    }
}
