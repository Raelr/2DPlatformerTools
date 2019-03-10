using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {


    [System.Serializable]
    public class Row {

        public Tile[] Tiles { get { return gridTiles; } }

        public Row(int rowSize) {

            gridTiles = new Tile[rowSize];
        }

        Tile[] gridTiles;

        public Tile GetTile(int index) {
            return gridTiles[index] != null ? gridTiles[index] : null;
        }

    }

    [Header("WorldGrid")]
    [SerializeField]
    Row[] worldGrid;

    [Header("Grid Configuration")]

    [SerializeField]
    bool showGrid;

    [SerializeField]
    [ReadOnly] float tileArea;

    [SerializeField]
    Vector2 gridDimensions;

    [SerializeField]
    float tileRadius;

    [SerializeField]
    [ReadOnly]int rowCount;

    [SerializeField]
    [ReadOnly]int rowSize;



    // Use this for initialization
    void Awake () {

        CalulateTilePlacement();
        CreateGrid();

    }

    void CreateGrid() {

        worldGrid = new Row[rowCount];

        Vector3 gridBottomLeft = transform.position - Vector3.right * gridDimensions.x / 2 - Vector3.up * gridDimensions.y / 2;

        for (int i = 0; i < worldGrid.Length; i++) {
            worldGrid[i] = new Row(rowSize);
            for (int j = 0; j < worldGrid[i].Tiles.Length; j++) {
                Vector3 worldPoint = gridBottomLeft + Vector3.right * (i * tileArea + tileRadius) + Vector3.up * (j * tileArea + tileRadius);
                worldGrid[i].Tiles[j] = new Tile(worldPoint);
            }
        }
    }

    void CalulateTilePlacement() {

        tileArea = tileRadius * 2;

        rowCount = Mathf.RoundToInt(gridDimensions.x / tileArea);
        rowSize = Mathf.RoundToInt(gridDimensions.y / tileArea);
    }

    private void OnDrawGizmos() {

        Gizmos.DrawWireCube(transform.position, new Vector3(gridDimensions.x, gridDimensions.y, 0));

        if (showGrid) {
            if (worldGrid.Length > 0) {
                for (int i = 0; i < worldGrid.Length; i++) {
                    if (worldGrid[i].Tiles.Length > 0) {
                        for (int j = 0; j < worldGrid[i].Tiles.Length; j++) {
                            Gizmos.DrawWireCube(worldGrid[i].Tiles[j].WorldPosition, Vector3.one);
                        }
                    }
                }
            }
        } 
    }
}
