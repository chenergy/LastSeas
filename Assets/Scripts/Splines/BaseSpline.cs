using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseSpline : MonoBehaviour
{
    protected void CalcNaturalCubic(List<Vector3> valueCollection, List<Cubic> cubicCollection, int vectorIndex)
    {
        int num = valueCollection.Count - 1;

        float[] gamma = new float[num + 1];
        float[] delta = new float[num + 1];
        float[] D = new float[num + 1];

        int i;
        /*
           We solve the equation
          [2 1       ] [D[0]]   [3(x[1] - x[0])  ]
          |1 4 1     | |D[1]|   |3(x[2] - x[0])  |
          |  1 4 1   | | .  | = |      .         |
          |    ..... | | .  |   |      .         |
          |     1 4 1| | .  |   |3(x[n] - x[n-2])|
          [       1 2] [D[n]]   [3(x[n] - x[n-1])]
          
          by using row operations to convert the matrix to upper triangular
          and then back sustitution.  The D[i] are the derivatives at the knots.
        */
        gamma[0] = 1.0f / 2.0f;
        for (i = 1; i < num; i++)
        {
            gamma[i] = 1.0f / (4.0f - gamma[i - 1]);
        }

        gamma[num] = 1.0f / (2.0f - gamma[num - 1]);

        float p0 = valueCollection[0][vectorIndex];
        float p1 = valueCollection[1][vectorIndex];

        delta[0] = 3.0f * (p1 - p0) * gamma[0];
        for (i = 1; i < num; i++)
        {
            p0 = valueCollection[i - 1][vectorIndex];
            p1 = valueCollection[i + 1][vectorIndex];

            delta[i] = (3.0f * (p1 - p0) - delta[i - 1]) * gamma[i];
        }

        p0 = valueCollection[num - 1][vectorIndex];
        p1 = valueCollection[num][vectorIndex];

        delta[num] = (3.0f * (p1 - p0) - delta[num - 1]) * gamma[num];

        D[num] = delta[num];
        for (i = num - 1; i >= 0; i--)
        {
            D[i] = delta[i] - gamma[i] * D[i + 1];
        }

        // Now compute the coefficients of the cubics.
        cubicCollection.Clear();

        for (i = 0; i < num; i++)
        {
            p0 = valueCollection[i][vectorIndex];
            p1 = valueCollection[i + 1][vectorIndex];

            cubicCollection.Add(new Cubic(
                p0,
                D[i],
                3 * (p1 - p0) - 2 * D[i] - D[i + 1],
                2 * (p0 - p1) + D[i] + D[i + 1]
            )
            );
        }
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