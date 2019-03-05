using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : RayCastUser {

    [SerializeField]
    BoxCollider2D collider;
	
	public override void Start () {

        base.Start();
	}
	
	void Update () {
		
	}

    void DetectPlayersBelow() {

        

    }
}
