using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static void UpdateTilePositioningText(TextMeshPro tilePositioningText, string text) {

        tilePositioningText.text = text;
    }
}
