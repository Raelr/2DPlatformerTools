using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolSet : MonoBehaviour {

    public Tool CurrentTool { get { return currentTool; } set { currentTool = value; } }

    Collider2D currentTileCollider;

    SelectableButton currentButton;

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

    [Header("Brushes")]
    [SerializeField]
    Tool[] Brushes;

    [Header("Mouse Ray")]
    [SerializeField]
    int rayLength = 0;

    [Header("Bar Containing Icons")]
    [SerializeField]
    SelectionBar selection;

    [Header("World Canvas")]
    [SerializeField]
    LevelGrid grid;

    [Header("current tool")]
    [SerializeField, ReadOnly]
    Tool currentTool;

    bool isClicked;

    bool isHoveringOverLevel;

    Vector2 mousePosition;

    [Header("Placeholder")]
    [SerializeField]
    PlaceHolderTile placeHolder;

    Vector2 velocity;

    public delegate void LevelEditorMovedHandler(Vector2 velocity);

    public event LevelEditorMovedHandler cameraMoved;

    private void Awake() {

        isClicked = false;
        isHoveringOverLevel = false;

        grid = GetComponent<LevelGrid>();
    }

    // This should stay in toolset.
    void MouseHoverSelection() {

        RaycastHit2D hit;

        hit = Physics2D.Raycast(GetMousePosition(), Vector2.up * rayLength, 1, selectionLayerMask);

        if (hit) {

            UpdateText(hit.transform.name);

            if (isClicked) {

                //if (currentTileCollider != hit.collider) {

                //    currentTileCollider = hit.collider;

                //    currentTile = hit.transform.GetComponent<SelectionTile>();

                //    settings = currentTile.tileSettings;

                //    isHoveringOverLevel = false;
                //}

            if (currentTileCollider != hit.collider) {

                    if (currentButton != null) {
                        currentButton.IsActivated = false;
                    }
                    
                    currentTileCollider = hit.collider;
                    currentButton = hit.transform.GetComponent<SelectableButton>();
                    Debug.Log(currentButton);

                    if (currentButton.onClicked != null) {

                        currentButton.onClicked.Invoke();
                        currentButton.IsActivated = true;
                    }
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

    public void CheckMouseinput() {

        isClicked = false;

        Vector2 currentMousePosition = GetMousePosition();

        if (!MouseIsOutOfBounds()) {
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
    }

    // This should go in standardTileBrush.
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

    // This should go in eraser.
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

        Vector2 MousePosition = Vector2.zero;

        mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        return mousePosition;

    }

    bool MouseIsOutOfBounds() {

        return Input.mousePosition.x <= 0 || Input.mousePosition.y <= 0 || Input.mousePosition.x > Screen.width - 10 || Input.mousePosition.y > Screen.height - 10;
    }
}
