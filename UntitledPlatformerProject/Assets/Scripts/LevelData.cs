using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {

    Dictionary<Collider2D, Platform> platforms = new Dictionary<Collider2D, Platform>();

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

            Collider2D collider = allObstacles[i].GetComponent<Collider2D>();

            platforms.Add(collider, platformScript);
        }
    }

    void RenameObject(ref string name, int amount) {

        name = name + amount;
    }

    public Platform GetPlatformScript(Collider2D collider) {

        return platforms.ContainsKey(collider) == true ? platforms[collider] : null;
    }
}
