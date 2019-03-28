using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTileBrush : Tool {

    public int Identification { get; set; }

    [Header("PlaceHolder Tile")]
    [SerializeField]
    PlaceHolderTile placeHolder;

    public override void OnClick() {

        CreateTile();
    }

    // This should go in standardTileBrush.
    void CreateTile() {

        if (selectedTile != null) {

            Vector3 coordinates = Utilities.GetMousePosition();

            Vector3 roundedMouseCoordinates = new Vector3(Mathf.RoundToInt(coordinates.x), Mathf.RoundToInt(coordinates.y), 2);
            PlaceHolderTile tile = Instantiate(placeHolder, roundedMouseCoordinates, Quaternion.identity);
            tile.Renderer.sprite = selectedTile.Renderer.sprite;
            tile.SettingsId = tileSettings.id;

            AssignSortingLayer(ref tile, tileSettings.tilePositioning);
            tile.gameObject.name = tile.gameObject.name.Split('(')[0];
            LevelGrid.instance.AddTile(roundedMouseCoordinates, tile, tileSettings);
        }
    }

    void AssignSortingLayer(ref PlaceHolderTile tile, TileSettings.TilePositioning positioning) {

        tile.Renderer.sortingOrder = (int)positioning;
    }
}
