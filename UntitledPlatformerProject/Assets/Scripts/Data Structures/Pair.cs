using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pair<T, V> : System.Object
{
    public T First { get { return first; } set { first = value; } }

    public T Second { get { return second; } set { second = value; } }

    T first;
    T second;

    public Pair(T first, T second) {

        this.first = first;
        this.second = second;
    }
}
