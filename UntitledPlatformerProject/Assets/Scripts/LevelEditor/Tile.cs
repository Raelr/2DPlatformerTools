using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Tile : MonoBehaviour{

    public Vector3 WorldPosition { get; set; }

    public int index;

	public Tile(Vector3 worldPosition) {

        WorldPosition = worldPosition;
    }
}
