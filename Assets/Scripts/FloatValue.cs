using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Floatvalue", menuName = "Floatvalue", order = 4)]
public class FloatValue : ScriptableObject
{
    public float[] theFloats;
    public string[] theStrings;
    public Thingyboyo boy;
}

[System.Serializable]
public class Thingyboyo
{[SerializeField]
    public float aFloat;
}

