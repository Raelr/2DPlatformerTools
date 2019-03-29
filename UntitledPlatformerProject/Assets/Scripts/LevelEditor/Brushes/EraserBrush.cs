using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserBrush : Tool {

    public override void OnLeftClick() {

        RemoveTile();
    }

    public override void OnRightClick() {
        
    }

    // This should go in eraser.
    void RemoveTile() {

        RaycastHit2D hit;

        hit = Physics2D.Raycast(Utilities.GetMousePosition(), Vector2.up * 1, 1, toolSelectionMask);

        if (hit) {

            PlaceHolderTile tile = hit.collider.GetComponent<PlaceHolderTile>();

            if (tile != null) {

                TileSettings settings = SelectionBar.instance.GetSettingsByIndex(tile.SettingsId);

                if (settings != null) {

                    Vector3 coordinates = Utilities.GetMousePosition();

                    Vector3 roundedMouseCoordinates = new Vector3(Mathf.RoundToInt(coordinates.x), Mathf.RoundToInt(coordinates.y), 2);

                    LevelGrid.instance.RemoveTile(roundedMouseCoordinates, tile);
                }
            }
        }
    }

    public override void SelectTile() {

    }
}
