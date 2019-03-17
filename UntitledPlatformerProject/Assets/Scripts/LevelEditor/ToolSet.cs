using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolSet : MonoBehaviour {

    Collider2D currentTileCollider;
    [Header("Tool Settings")]
    [SerializeField, ReadOnly]
    SelectionTile currentTile;

    [SerializeField, ReadOnly]
    TileSettings settings;

    [Header("Tile Name Text")]
    [SerializeField]
    TextMeshPro text;

    [Header("Layer Masks")]
    [SerializeField]
    LayerMask selectionLayerMask;

    [SerializeField]
    LayerMask levelGridMask;

    [SerializeField]
    LayerMask tileLayerMask;

    [Header("Mouse Ray")]
    [SerializeField]
    int rayLength = 0;

    [Header("Bar Containing Icons")]
    [SerializeField]
    SelectionBar selection;

    [Header("World Canvas")]
    [SerializeField]
    LevelGrid grid;

    [Header("Default tool")]
    [SerializeField]
    Tool defaultTool;

    bool isClicked;

    bool isHoveringOverLevel;

    Vector2 mousePosition;

    [SerializeField]
    PlaceHolderTile placeHolder;

    Vector2 velocity;

    public delegate void LevelEditorMovedHandler(Vector2 velocity);

    public event LevelEditorMovedHandler cameraMoved;

    private void Awake() {

        isClicked = false;
        isHoveringOverLevel = false;

        grid = GetComponent<LevelGrid>();

        selection = GetComponent<SelectionBar>();
    }

    private void Update() {

        CheckMouseinput();
    }

    void MouseHoverSelection() {

        RaycastHit2D hit;

        hit = Physics2D.Raycast(GetMousePosition(), Vector2.up * rayLength, 1, selectionLayerMask);

        if (hit) {

            UpdateText(hit.transform.name);

            if (isClicked) {

                if (currentTileCollider != hit.collider) {

                    currentTileCollider = hit.collider;

                    currentTile = hit.transform.GetComponent<SelectionTile>();

                    settings = currentTile.tileSettings;

                    isHoveringOverLevel = false;
                }
            }

        } else {

            if (currentTile == null) {
                UpdateText("None");
            }

            isHoveringOverLevel = false;
        }
    }

    void MouseHoverLevel() {

        RaycastHit2D hit;

        hit = Physics2D.Raycast(GetMousePosition(), Vector2.up * rayLength, 1, levelGridMask);

        if (!hit) {

            isHoveringOverLevel = true;
        } else {

            isHoveringOverLevel = false;
        }
    }

    void CheckMouseinput() {

        isClicked = false;

        Vector2 currentMousePosition = GetMousePosition();

        MouseHoverLevel();

        if (Input.GetKey(KeyCode.Mouse0)) {

            isClicked = true;

            if (isHoveringOverLevel) {

                CreateTile();
            }

        } else if (Input.GetKey(KeyCode.Mouse1)) {

            RemoveTile();
        }

        MouseHoverSelection();
    }

    void CreateTile() {

        if (currentTile != null) {

            RaycastHit2D hit;

            hit = Physics2D.Raycast(GetMousePosition(), Vector2.up * rayLength, 1, levelGridMask);

            if (!hit) {

                if (isClicked) {

                    Vector3 coordinates = GetMousePosition();

                    Vector3 roundedMouseCoordinates = new Vector3(Mathf.RoundToInt(coordinates.x), Mathf.RoundToInt(coordinates.y), 2);
                    PlaceHolderTile tile = Instantiate(placeHolder, roundedMouseCoordinates, Quaternion.identity);
                    tile.Renderer.sprite = currentTile.Renderer.sprite;
                    tile.SettingsId = settings.id;

                    AssignSortingLayer(ref tile, settings.tilePositioning);
                    tile.gameObject.name = tile.gameObject.name.Split('(')[0];
                    grid.AddTile(roundedMouseCoordinates, tile, settings);

                    isClicked = false;
                }
            }
        }
    }

    void RemoveTile() {

        RaycastHit2D hit;

        hit = Physics2D.Raycast(GetMousePosition(), Vector2.up * rayLength, 1, tileLayerMask);

        if (hit) {

            PlaceHolderTile tile = hit.collider.GetComponent<PlaceHolderTile>();

            if (tile != null) {

                TileSettings settings = selection.GetSettingsByIndex(tile.SettingsId);

                if (settings != null) {

                    Vector3 coordinates = GetMousePosition();

                    Vector3 roundedMouseCoordinates = new Vector3(Mathf.RoundToInt(coordinates.x), Mathf.RoundToInt(coordinates.y), 2);

                    grid.RemoveTile(roundedMouseCoordinates, settings);
                }
            }
        }
    }

    bool IsSameTile(Collider2D collider) {

        if (currentTile != null) {

            string currentTileName = currentTile.gameObject.name;

            string shortenedCurrentName = currentTileName.Split('(')[0];

            string tileName = collider.gameObject.name;

            string shortenedTileName = tileName.Split('(')[0];

            return currentTileName == shortenedTileName;

        } else {

            return false;
        }

    }

    void AssignSortingLayer(ref PlaceHolderTile tile, TileSettings.TilePositioning positioning) {

        tile.Renderer.sortingOrder = (int)positioning;
    }

    void ResetBrush() {

        currentTile = null;
        UpdateText("None");
    }

    void UpdateText(string name) {

        text.text = "Tile: " + name;
    }

    Vector2 GetMousePosition() {

        Vector2 mousePosition = Vector2.zero;

        mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        return mousePosition;
    }
}
