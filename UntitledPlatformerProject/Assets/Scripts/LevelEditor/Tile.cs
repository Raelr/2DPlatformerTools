using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Tile : MonoBehaviour {

    public Vector3 WorldPosition { get; set; }

    public int Id { get { return id; } }

    public SpriteRenderer Renderer { get { return renderer; } set { renderer = value; } }

    public bool IsPlaced { get {return isPlaced;}}

    int id;

    public TilePosition position;

    [SerializeField]
    SpriteRenderer renderer;

    bool isPlaced;

    public enum TilePosition {

        Background,
        MiddleGround,
        Foreground
    }

    private void Awake() {

        renderer = GetComponent<SpriteRenderer>();
        
    }

}
