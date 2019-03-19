using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    [SerializeField]
    LayerMask toolSelectionMask;

    public abstract void OnClick();
}
