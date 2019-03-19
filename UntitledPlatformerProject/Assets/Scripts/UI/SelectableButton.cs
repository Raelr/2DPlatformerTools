using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableButton : MonoBehaviour
{
    public bool IsActivated { get { return isActivated; } set { isActivated = value; OnActivation(); } }

    [Header("Renderer")]
    [SerializeField, ReadOnly]
    SpriteRenderer renderer;

    [Header("Activation Sprite (Required)")]
    [SerializeField]
    Sprite activatedButtonSprite;

    [Header("Default Sprite (Required)")]
    [SerializeField]
    Sprite DefaultButtonSprite;

    public delegate void OnClickedHandler();

    public OnClickedHandler onClicked;

    bool isActivated;

    void Awake()
    {
        if (renderer == null) {
            renderer = GetComponent<SpriteRenderer>();
        }
    }

    void OnActivation() {

        renderer.sprite = isActivated ? activatedButtonSprite : DefaultButtonSprite;
    }
}
