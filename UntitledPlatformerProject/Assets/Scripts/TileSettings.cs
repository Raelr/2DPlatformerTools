using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Tile", menuName = "Tiles")]
public class TileSettings : ScriptableObject
{
    public int id;
    public enum TilePositioning{

        Background,
        MidGround,
        Foreground
    }
}
