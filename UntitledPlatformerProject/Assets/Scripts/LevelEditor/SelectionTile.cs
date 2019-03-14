using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionTile : Icon {

    public SpriteRenderer Renderer { get { return renderer; } set { renderer = value; } }

    public Vector3 WorldPosition { get { return worldPosition; } set { worldPosition = value; } }

    [SerializeField, ReadOnly]
    SpriteRenderer renderer;

    Vector3 worldPosition;

    private void Awake() {

        renderer = GetComponent<SpriteRenderer>();
    }


    public override void OnClicked() {
        

    }
}
