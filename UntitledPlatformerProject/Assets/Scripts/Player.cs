using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

    Controller2D controller2D;

	void Start () {

        controller2D = GetComponent<Controller2D>();
	}

	void Update () {
		
	}
}
