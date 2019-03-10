using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    public Vector3 WorldPosition { get; set; }

	public Tile(Vector3 worldPosition) {

        WorldPosition = worldPosition;
    }
}
