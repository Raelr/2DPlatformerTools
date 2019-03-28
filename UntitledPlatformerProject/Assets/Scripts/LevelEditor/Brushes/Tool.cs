using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour {
    [Header("Selection Mask")]
    [SerializeField]
    protected LayerMask toolSelectionMask;

    [Header("Tile")]
    [SerializeField, ReadOnly]
    protected SelectionTile selectedTile;

    [Header("Tile Collider")]
    [SerializeField, ReadOnly]
    protected Collider2D currentTileCollider;

    protected TileSettings tileSettings;

    public virtual void OnLeftClick() { }

    public virtual void OnRightClick() { }

    public virtual void OnHover(bool isActive) { }

    public virtual void SelectTile() {

        RaycastHit2D hit;

        hit = Physics2D.Raycast(Utilities.GetMousePosition(), Vector2.up * 1, 1, toolSelectionMask);

        if (hit) {

            if (currentTileCollider != hit.collider) {

                currentTileCollider = hit.collider;

                selectedTile = hit.transform.GetComponent<SelectionTile>();

                tileSettings = selectedTile.tileSettings;
            }
        }
    }
}

