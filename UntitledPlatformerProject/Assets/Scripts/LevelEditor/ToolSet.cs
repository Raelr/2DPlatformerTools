using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolSet : MonoBehaviour {

    Collider2D currentTileCollider;

    [SerializeField]
    [ReadOnly] Tile currentTile;

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
                    currentTile = hit.transform.GetComponent<Tile>();

                    isClicked = false;
                    isHoveringOverLevel = false;
                }
            }
        } else {

            if (currentTile == null) {
                UpdateText("None");
            }

            isClicked = false;
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

        Vector2 currentMousePosition = GetMousePosition();

        MouseHoverLevel();

        if (Input.GetKeyDown(KeyCode.Mouse0)) {

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
                    Tile tile = Instantiate(currentTile, roundedMouseCoordinates, Quaternion.identity);
                    grid.AddTile(roundedMouseCoordinates, tile);
                }
            }
        }
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
