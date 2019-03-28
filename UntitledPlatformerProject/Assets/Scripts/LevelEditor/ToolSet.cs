using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolSet : MonoBehaviour {

    public Tool CurrentTool { get { return currentTool; } set { currentTool = value; } }

    Collider2D currentBrushCollider;

    SelectableButton currentButton;

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

    public delegate void BrushClickHandler();

    public BrushClickHandler brushUsed;

    public delegate void BrushSelectionHandler();

    public BrushSelectionHandler onBrushSelection;

    private void Awake() {

        isClicked = false;
        isHoveringOverLevel = false;
    }

    void CheckForBrushButtonSelection() {

        RaycastHit2D hit;

        hit = Physics2D.Raycast(Utilities.GetMousePosition(), Vector2.up * rayLength, 1, selectionLayerMask);

        if (hit) {

            if (isClicked) {

                if (currentBrushCollider != hit.collider) {

                    if (currentButton != null) {
                        currentButton.IsActivated = false;
                    }

                    RemoveBrushDelegates();

                    currentBrushCollider = hit.collider;
                    currentButton = hit.transform.GetComponent<SelectableButton>();

                    currentButton.onClicked?.Invoke();
                    currentButton.IsActivated = true;

                    onBrushSelection += currentTool.SelectTile;
                    brushUsed += currentTool.OnClick;
                }
            }
        } 
    }

    void RemoveBrushDelegates() {

        if (currentTool != null) {

            if (onBrushSelection == null) {
                onBrushSelection -= currentTool.SelectTile;
            }

            if (brushUsed == null) {
                brushUsed -= currentTool.OnClick;
            }
        }
    }

    void MouseHoverLevel() {

        RaycastHit2D hit;

        hit = Physics2D.Raycast(Utilities.GetMousePosition(), Vector2.up * rayLength, 1, levelGridMask);

        if (!hit) {

            isHoveringOverLevel = true;

        } else {

            isHoveringOverLevel = false;
        }
    }

    public void CheckMouseinput() {

        isClicked = false;

        Vector2 currentMousePosition = Utilities.GetMousePosition();

        if (!Utilities.MouseIsOutOfBounds()) {

            MouseHoverLevel();

            if (Input.GetKey(KeyCode.Mouse0)) {

                isClicked = true;

                CheckForBrushButtonSelection();

                onBrushSelection?.Invoke();

                if (isHoveringOverLevel) {

                    brushUsed?.Invoke();
                }

                isClicked = false;

            } else if (Input.GetKeyDown(KeyCode.Mouse1)) {

                RemoveTile();
                ResetBrush();
            }
        }
    }

    // This should go in eraser.
    void RemoveTile() {

        RaycastHit2D hit;

        hit = Physics2D.Raycast(Utilities.GetMousePosition(), Vector2.up * rayLength, 1, tileLayerMask);

        if (hit) {

            PlaceHolderTile tile = hit.collider.GetComponent<PlaceHolderTile>();

            if (tile != null) {

                TileSettings settings = selection.GetSettingsByIndex(tile.SettingsId);

                if (settings != null) {

                    Vector3 coordinates = Utilities.GetMousePosition();

                    Vector3 roundedMouseCoordinates = new Vector3(Mathf.RoundToInt(coordinates.x), Mathf.RoundToInt(coordinates.y), 2);

                    LevelGrid.instance.RemoveTile(roundedMouseCoordinates, settings);
                }
            }
        }
    }

    void ResetBrush() {

        if (onBrushSelection != null) {
            onBrushSelection -= currentTool.SelectTile;
        }
        
        if (brushUsed != null) {
            brushUsed -= currentTool.OnClick;
        }

        if (currentButton != null) {
            currentButton.IsActivated = false;
        }

        currentBrushCollider = null;
        currentTool = null;
        currentButton = null;
        
    }
}
