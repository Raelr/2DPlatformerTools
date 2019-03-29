using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {

    /// <summary>
    /// Compares two vectors and determine whether they are approximately equal.
    /// </summary>
    /// <param name="vector1"> The first vector being compared </param>
    /// <param name="vector2"> The second vector being compared </param>
    /// <returns> A boolean determining whether they are pproximately equal </returns>

    public static bool Vector3Equals(Vector3 vector1, Vector3 vector2) {

        bool equals = false;

        float distance = (vector1 - vector2).sqrMagnitude;

        equals = distance < 0.00000001 ? true : false;

        return equals;
    }

    public static bool Vector2Equals(Vector2 vector1, Vector2 vector2) {

        bool equals = false;

        float distance = (vector1 - vector2).sqrMagnitude;

        equals = distance < 0.00000001 ? true : false;

        return equals;
    }

    public static Vector2 GetMousePosition() {

        Vector2 MousePosition = Vector2.zero;

        MousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        return MousePosition;

    }

    public static bool MouseIsOutOfBounds() {

        return Input.mousePosition.x <= 0 || Input.mousePosition.y <= 0 || Input.mousePosition.x > Screen.width - 10 || Input.mousePosition.y > Screen.height - 10;
    }

    public static Vector2 GetRoundedMousePosition() {

        return new Vector3(Mathf.RoundToInt(GetMousePosition().x), Mathf.RoundToInt(GetMousePosition().y));
    }
}

public class ReadOnlyAttribute : PropertyAttribute { }
