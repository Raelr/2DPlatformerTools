﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTileBrush : Tool {

    public int Identification { get; set; }

    [Header("PlaceHolder Tile")]
    [SerializeField]
    PlaceHolderTile placeHolder;

    PlaceHolderTile hoverTile;

    [Header("Tile Spawn Offset")]
    [SerializeField]
    Vector3 tileSpawnOffset;

    Vector2 oldMousePosition;

    private void Awake() {

        hoverTile = Instantiate(placeHolder, transform.position + tileSpawnOffset, Quaternion.identity);
        hoverTile.gameObject.SetActive(false);
    }

    public override void OnLeftClick() {

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

    public override void SelectTile() {
        base.SelectTile();
    }

    void AssignSortingLayer(ref PlaceHolderTile tile, TileSettings.TilePositioning positioning) {

        tile.Renderer.sortingOrder = (int)positioning;
    }

    public override void OnHover(bool isActive) {

        if (isActive) {

            if (selectedTile != null) {

                hoverTile.gameObject.SetActive(true);

                if (hoverTile.Renderer.sprite != selectedTile.Renderer.sprite) {
                    hoverTile.Renderer.sprite = selectedTile.Renderer.sprite;
                }

                Vector2 roundedMousePosition = Utilities.GetRoundedMousePosition();

                if (roundedMousePosition != oldMousePosition) {

                    oldMousePosition = roundedMousePosition;
                    hoverTile.transform.position = new Vector3(roundedMousePosition.x, roundedMousePosition.y, tileSpawnOffset.z);
                }
            }

        } else {

            hoverTile.gameObject.SetActive(false);
        }
    }
}
