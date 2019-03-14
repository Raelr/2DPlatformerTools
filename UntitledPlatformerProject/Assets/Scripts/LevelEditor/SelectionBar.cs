using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBar : MonoBehaviour {

    [Header("UI Grids")]
    [SerializeField]
    TileGrid[] tileGrid;

    [Header("UI Element")]
    [SerializeField]
    Transform selectionSprite;

    [Header("Grid Used")]
    TileGrid currentGrid;

    public static SelectionBar instance;

    [Header("Tiles Used by Grid")]
    [SerializeField]
    TileSettingsConfig[] tileIcons;

    [Header("AssetBundle Url")]
    [SerializeField]
    string assetUrl;

    [Header("Icon Prefab")]
    [SerializeField]
    SelectionTile tilePrefab;

    private void Awake() {

        currentGrid = tileGrid[0];

        SetGridDimensions();
        PopulateGrid();

        if (instance == null) {
            instance = this;
        }

    }

    void SetGridDimensions() {

        int xMax = Mathf.RoundToInt(selectionSprite.transform.localScale.x - 1);
        int yMax = Mathf.RoundToInt(selectionSprite.transform.localScale.y - 1);

        currentGrid.GridDimensions = new Vector2(xMax, yMax);
    }

    void PopulateGrid() {

        if (tileIcons.Length > 0) {
            for (int i = 0; i < tileIcons.Length; i++) {
                TileSettingsConfig settings = tileIcons[i];
                if (settings.tiles.Length > 0) {
                    for (int x = 0; x < settings.tiles.Length; x++) {
                        SelectableTile tile = settings.tiles[x];
                        currentGrid.AddTile(tilePrefab, tile.sprite);
                    }
                }
            }
        }
    }

    [System.Serializable]
    public struct SelectableTile {

        public enum TileType {

            Solid,
            OneWay,
            Background,
            Foreground
        }

        public float id;
        public Sprite sprite;
    }

    [System.Serializable]
    public struct TileSettingsConfig {

        [Header("Settings")]
        public TileSettings settings;

        [Header("Tile Sprites")]
        public SelectableTile[] tiles;
    }
}
