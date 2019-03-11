using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBar : MonoBehaviour {

    [SerializeField]
    TileGrid tileGrid;

    [SerializeField]
    Tile[] tiles;

    [SerializeField]
    Transform selectionSprite;

    public static SelectionBar instance;

    private void Awake() {

        SetGridDimensions();
        PopulateGrid();

        if (instance == null) {
            instance = this;
        }
    }

    void SetGridDimensions() {

        int xMax = Mathf.RoundToInt(selectionSprite.transform.localScale.x - 1);
        int yMax = Mathf.RoundToInt(selectionSprite.transform.localScale.y - 1);

        tileGrid.GridDimensions = new Vector2(xMax, yMax);
    }

    void PopulateGrid() {

        if (tiles.Length > 0) {
            for (int i = 0; i < tiles.Length; i++) {
                if (tileGrid.SpotsToBeFilled > 0) {
                    tileGrid.AddTile(tiles[i]);
                } else {
                    return;
                }
            }
        }
    }
}
