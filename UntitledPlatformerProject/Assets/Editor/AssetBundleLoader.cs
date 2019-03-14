using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetBundleLoader : Editor
{

    [MenuItem("Tools/Generate Bundles")] 
    static void BuildAllAssetBundles() {

        string path = Application.dataPath + "/AssetBundles";

        Debug.Log(path);

        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        
    }

}
