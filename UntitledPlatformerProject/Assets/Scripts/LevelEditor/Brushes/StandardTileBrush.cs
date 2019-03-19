using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTileBrush : Tool
{
    public int Identification { get; set; } 

    [SerializeField, ReadOnly]
    SelectionTile tile;

    [SerializeField, ReadOnly]
    Collider2D currentTileCollider;

    TileSettings tileSettings;

    public override void OnClick() {
        

    }
}
