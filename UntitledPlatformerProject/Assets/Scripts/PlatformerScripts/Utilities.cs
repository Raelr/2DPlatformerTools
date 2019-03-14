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

    public static bool VectorEquals(Vector3 vector1, Vector3 vector2) {

        bool equals = false;

        float distance = (vector1 - vector2).sqrMagnitude;

        equals = distance < 0.00000001 ? true : false;

        return equals;
    }  
}

public class ReadOnlyAttribute : PropertyAttribute { }
