using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {

    public Vector2 GridDimensions { get { return gridDimensions; } set { gridDimensions = value; CalulateTilePlacement();} }

    public int SpotsToBeFilled { get { return spotsToBeFilled; } }

    [System.Serializable]
    public class Row {

        [SerializeField]
        public Tile[] Tiles { get { return gridTiles; } }

        public Row(int rowSize) {

            gridTiles = new Tile[rowSize];
        }

        Tile[] gridTiles;

        public Tile GetTile(int index) {
            return gridTiles[index] != null ? gridTiles[index] : null;
        }

    }

    public static TileGrid instance;

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

    [SerializeField]
    [ReadOnly] int spotsToBeFilled;

    void AddRows() {

        if (worldGrid.Length > 0) {
            for (int i = 0; i < rowCount; i++) {
                worldGrid[i] = new Row(rowSize);
            }
        } else {
            Debug.LogError("There are no rows in the grid!");
        } 
    }

    public Tile GetTile(Vector3 coordinates) {

        Tile gridTile = null;

        if (coordinates.x < rowCount && coordinates.y < rowSize) {

            int coordinateX = Mathf.RoundToInt(coordinates.x);
            int coordinateY = Mathf.RoundToInt(coordinates.y);

            if (worldGrid[coordinateX] != null) {
                if (worldGrid[coordinateX].Tiles[coordinateY] != null) {
                    gridTile = worldGrid[coordinateX].Tiles[coordinateY];
                }
            }
        }

        return gridTile;
    }

    public void AddTile(Tile tile) {

        Vector3 gridBottomLeft = transform.position - Vector3.right * gridDimensions.x / 2 - Vector3.up * gridDimensions.y / 2;

        if (worldGrid.Length > 0) {
            for (int i = 0; i < worldGrid.Length; i++) {
                Row currentRow = worldGrid[i];
                if (currentRow != null && currentRow.Tiles.Length > 0) {
                    for (int x = 0; x < currentRow.Tiles.Length; x++) {
                        if (currentRow.Tiles[x] == null) {

                            Vector3 worldPosition = gridBottomLeft + Vector3.right * (i * tileArea + tileRadius) + Vector3.up * (x * tileArea + tileRadius);
                            Tile inputTile = Instantiate(tile, worldPosition, Quaternion.identity);
                            inputTile.WorldPosition = worldPosition;
                            currentRow.Tiles[x] = inputTile;
                            spotsToBeFilled--;
                            return;

                        } else {
                            continue;
                        }
                    }
                }
            }
        }
    }

    void CalulateTilePlacement() {

        tileArea = tileRadius * 2;

        rowCount = Mathf.RoundToInt(gridDimensions.x / tileArea);
        rowSize = Mathf.RoundToInt(gridDimensions.y / tileArea);

        worldGrid = new Row[rowCount];

        spotsToBeFilled = rowSize * rowCount;

        AddRows();
    }

    private void OnDrawGizmos() {

        Gizmos.DrawWireCube(transform.position, new Vector3(gridDimensions.x, gridDimensions.y, 0));

        if (showGrid) {
            if (worldGrid.Length > 0) {
                for (int i = 0; i < worldGrid.Length; i++) {
                    if (worldGrid[i].Tiles.Length > 0) {
                        for (int j = 0; j < worldGrid[i].Tiles.Length; j++) {
                            if (worldGrid[i].Tiles[j] != null) {
                                Gizmos.DrawWireCube(worldGrid[i].Tiles[j].WorldPosition, Vector3.one);
                            }
                        }
                    }
                }
            }
        } 
    }
}
