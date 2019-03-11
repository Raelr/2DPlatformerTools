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
    LayerMask layerMask;

    [SerializeField]
    int rayLength;

    bool clicked;

    Vector2 mousePosition;

    private void Update() {

        CheckMouseinput();
    }

    void MouseHover() {

        RaycastHit2D hit;

        hit = Physics2D.Raycast(mousePosition, Vector2.up * rayLength, layerMask);

        if (hit) {

            UpdateText(hit.transform.name);

            if (clicked) {

                if (currentTileCollider != hit.collider) {

                    currentTileCollider = hit.collider;
                    currentTile = hit.transform.GetComponent<Tile>();
                }
            }

            clicked = false;

        } else {
            UpdateText("None");
        }
    }

    void CheckMouseinput() {

        Vector2 currentMousePosition = GetMousePosition();

        if (currentMousePosition != mousePosition) {

            mousePosition = currentMousePosition;

            if (Input.GetKeyDown(KeyCode.Mouse0)) {

                clicked = true;
            }


            MouseHover();
        }
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
