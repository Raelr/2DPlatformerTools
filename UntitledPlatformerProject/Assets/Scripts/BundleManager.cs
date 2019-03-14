using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleManager : MonoBehaviour
{
    AssetBundle currentBundle;

    public static BundleManager instance;

    private void Awake() {

        if (instance == null) {
            instance = this;
        }
    }

    public void LoadAssetBundle(string loadUrl) {

        string path = Application.dataPath + loadUrl;

        Debug.Log(path);

        currentBundle = AssetBundle.LoadFromFile(loadUrl);

        Debug.Log(currentBundle == null ? "Incorrect file path!" : "Bundle loaded!");

    }

    public Sprite[] GetSpriteArray(string Url) {

        LoadAssetBundle(Url);

        Sprite[] spriteArray = null;

        spriteArray = currentBundle.LoadAllAssets<Sprite>();

        return spriteArray;
    }
}
