using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrushUIManager : MonoBehaviour {

    [Header("ToolSet")]
    [SerializeField]
    ToolSet toolSet;

    [Header("Standard Brush UI")]
    [SerializeField]
    SelectableButton standardBrushButton;

    [Header("Standard Brush Tool")]
    [SerializeField]
    Tool standardBrushTool;

    [Header("Standard Eraser UI")]
    [SerializeField]
    SelectableButton eraser;

    [Header("Standard Eraser Tool")]
    [SerializeField]
    Tool standardEraserTool;

    private void Awake() {

        InitialiseAllButtons();

    }

    void InitialiseAllButtons() {

        standardBrushButton.onClicked += StandardTileSelected;
        eraser.onClicked += EraserChosen;
    }

    public void StandardTileSelected() {
        
        toolSet.CurrentTool = standardBrushTool;
    }

    public void EraserChosen() {

        toolSet.CurrentTool = standardEraserTool;
    }
}
