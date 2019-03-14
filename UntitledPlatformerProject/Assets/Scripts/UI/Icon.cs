using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Icon : MonoBehaviour
{
    bool IsInteractable { get { return isInteractable; } }

    bool isInteractable;

    public abstract void OnClicked();

}
