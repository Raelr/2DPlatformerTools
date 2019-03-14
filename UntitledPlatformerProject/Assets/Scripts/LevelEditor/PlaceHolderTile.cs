using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderTile : MonoBehaviour
{
    public SpriteRenderer Renderer { get { return renderer; } }

    SpriteRenderer renderer;

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }
}
