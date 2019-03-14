using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour {

    public Dictionary<Vector2, PlaceHolderTile> Tiles { get { return tiles; } }
    public float TileSpacing { get { return tileSpacing; } }

    Dictionary<Vector2, PlaceHolderTile> tiles = new Dictionary<Vector2, PlaceHolderTile>();

    [SerializeField]
    float tileSpacing;

    public void AddTile(Vector2 coordinates, PlaceHolderTile tile) {

        if (tiles.ContainsKey(coordinates)) {
            RemoveTile(coordinates);
        }

        tiles.Add(coordinates, tile);
    }

    public void RemoveTile(Vector2 coordinates) {


        if (tiles.ContainsKey(coordinates)) {
            PlaceHolderTile oldTile = tiles[coordinates];
            Destroy(oldTile.gameObject);
            tiles.Remove(coordinates);
        }
    }
}
