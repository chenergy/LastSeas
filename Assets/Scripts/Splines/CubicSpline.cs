using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubicSpline : MonoBehaviour
{
    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
    }
}

public class Cubic
{
    private float a, b, c, d;

    public Cubic(float a, float b, float c, float d)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
    }

    public float Eval(float u)
    {
        return (((d * u) + c) * u + b) * u + a;
    }
}

