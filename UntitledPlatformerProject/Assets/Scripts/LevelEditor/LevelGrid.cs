using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour {

    public Dictionary<Vector2, TileData> Tiles { get { return tiles; } }
    public float TileSpacing { get { return tileSpacing; } }

    Dictionary<Vector2, TileData> tiles = new Dictionary<Vector2, TileData>();

    [SerializeField]
    float tileSpacing;

    public static LevelGrid instance;

    private void Awake() {
        
        if (instance == null) {
            instance = this;
        }
    }

    public void AddTile(Vector2 coordinates, PlaceHolderTile tile, TileSettings settings) {

        TileData newTileData = new TileData(settings, tile.Renderer.sprite, tile, coordinates);

        Vector2 newCoordinates = GetHashedVector(coordinates, (int)settings.tilePositioning);

        if (tiles.ContainsKey(newCoordinates)) {

            TileData data = tiles[newCoordinates];

            if (settings.tilePositioning == data.settings.tilePositioning) {
                RemoveTile(coordinates, settings);
            }
        }

        tiles.Add(newCoordinates, newTileData);
    }

    public void RemoveTile(Vector2 coordinates, TileSettings settings) {

        Vector2 newCoordinates = GetHashedVector(coordinates, (int)settings.tilePositioning);

        if (tiles.ContainsKey(newCoordinates)) {
            TileData oldTile = tiles[newCoordinates];
            Destroy(oldTile.placeHolder.gameObject);
            tiles.Remove(newCoordinates);
        }
    }

    Vector2 GetHashedVector(Vector2 coordinates, int position) {

        Vector2 newCoordinates;

        newCoordinates.x = coordinates.x + position * 100111;
        newCoordinates.y = coordinates.y + position * 100111;

        return newCoordinates;
    }

    public struct TileData {

        public TileData(TileSettings settings, Sprite sprite, PlaceHolderTile placeHolder, Vector2 tileCoordinates) {

            this.settings = settings;
            this.sprite = sprite;
            this.placeHolder = placeHolder;
            this.tileCoordinates = tileCoordinates;
        }

        public TileSettings settings;
        public Sprite sprite;
        public PlaceHolderTile placeHolder;
        public Vector2 tileCoordinates;

    }
}
