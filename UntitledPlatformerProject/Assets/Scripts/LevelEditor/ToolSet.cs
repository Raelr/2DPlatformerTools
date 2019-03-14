using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolSet : MonoBehaviour {

    Collider2D currentTileCollider;

    [SerializeField]
    SelectionTile currentTile;

    [SerializeField]
    TextMeshPro text;

    [SerializeField]
    LayerMask tileLayerMask;

    [SerializeField]
    LayerMask levelGridMask;

    [SerializeField]
    int rayLength;

    [SerializeField]
    SelectionBar selection;

    [SerializeField]
    LevelGrid grid;

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

        hit = Physics2D.Raycast(GetMousePosition(), Vector2.up * rayLength, 1, tileLayerMask);

        if (hit) {

            UpdateText(hit.transform.name);

            if (isClicked) {

                if (currentTileCollider != hit.collider) {

                    currentTileCollider = hit.collider;
                    currentTile = hit.transform.GetComponent<SelectionTile>();

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

        } else if (Input.GetKeyDown(KeyCode.Mouse1)) {

            ResetBrush();
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

                    AssignSortingLayer(ref tile, currentTile.Positioning);
                    tile.gameObject.name = tile.gameObject.name.Split('(')[0];
                    grid.AddTile(roundedMouseCoordinates, tile);
                }
            }
        }
    }

    bool IsSameTile(Collider2D collider) {

        if (currentTile != null) {

            string currentTileName = currentTile.gameObject.name;

            string shortenedCurrentName = currentTileName.Split('(')[0];

            //Debug.Log(shortenedCurrentName);

            string tileName = collider.gameObject.name;

            string shortenedTileName = tileName.Split('(')[0];

            return currentTileName == shortenedTileName;

            //Debug.Log(currentTileName);

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
