using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderTile : MonoBehaviour
{
    public SpriteRenderer Renderer { get { return renderer; } }

    public int SettingsId { get { return settingsId; } set { settingsId = value; } } 

    SpriteRenderer renderer;

    [SerializeField, ReadOnly]
    int settingsId;

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }
}
