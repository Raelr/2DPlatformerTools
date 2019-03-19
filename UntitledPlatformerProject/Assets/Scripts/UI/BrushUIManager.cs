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

    private void Awake() {

        InitialiseAllButtons();

    }

    void InitialiseAllButtons() {

        standardBrushButton.onClicked += StandardTileSelected;
    }

    public void StandardTileSelected() {

        Debug.Log("Standard Brush Chosen.");

        toolSet.CurrentTool = standardBrushTool;
    }
}
