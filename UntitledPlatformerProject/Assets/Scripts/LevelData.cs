using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {

    Dictionary<string, Platform> platforms = new Dictionary<string, Platform>();

    public static LevelData Instance;

    private void Awake() {

        if (Instance == null) {
            Instance = this;
        }
    }

    void Start () {

        InitialiseAllPlatforms();

	}

    void InitialiseAllPlatforms() {

        GameObject[] allObstacles = GameObject.FindGameObjectsWithTag("Platform");

        for (int i = 0; i < allObstacles.Length; i++ ) {

            Platform platformScript = allObstacles[i].GetComponent<Platform>();

            string name = allObstacles[i].name;

            RenameObject(ref name, i);

            allObstacles[i].name = name;

            platforms.Add(name, platformScript);
        }
    }

    void RenameObject(ref string name, int amount) {

        name = name + amount;
    }

    public Platform GetPlatformScript(string name) {

        return platforms.ContainsKey(name) == true ? platforms[name] : null;
    }
}
