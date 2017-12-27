using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Cubic function containing four parameters.
/// </summary>
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

/// <summary>
/// Spline interpolation along a list of points.
/// </summary>
public class SplineInterpolation : BaseSpline
{
    public Transform[] m_knots;

    private List<Vector3> m_points;
    [SerializeField]
    private List<Cubic> m_xCubics;
    [SerializeField]
    private List<Cubic> m_yCubics;
    [SerializeField]
    private List<Cubic> m_zCubics;

    /// <summary>
    /// The delta t per frame to move uniformly.
    /// </summary>
    private float m_deltaT;

    /// <summary>
    /// The number of divisions in the spline used to calculate length.
    /// </summary>
    private int m_divisions = 1000;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    public void Awake()
    {
        SetupPoints();

        // Get the incremental distance to move per frame.
        float length = 0;
        for (int i = 0; i < m_divisions; i++)
        {
            length += Vector3.Distance(GetPoint((i + 1) * 1.0f / m_divisions), GetPoint(i * 1.0f / m_divisions));
        }

        m_deltaT = 1.0f / length;
    }

    /// <summary>
    /// Raises the draw gizmos selected event.
    /// </summary>
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        for (int i = 0; i < m_knots.Length; i++)
        {
            Gizmos.DrawWireSphere(m_knots[i].position, 1f);
        }
    }

    /// <summary>
    /// Determines whether this instance can be drawn in editor.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    public override bool CanBeDrawnInEditor()
    {
        if ((!m_showSplineInEditor) || (m_knots.Length == 0))
        {
            return false;
        }

        if (m_xCubics == null)
        {
            SetupPoints();
        }

        return true;
    }

    /// <summary>
    /// Gets the point on spline at a position t.
    /// </summary>
    /// <returns>The point along the spline.</returns>
    /// <param name="t">T value along the spline.</param>
    public override Vector3 GetPoint(float t)
    {
        t = t * m_xCubics.Count;
        int cubicNum = (int)t;
        float cubicPos = (t - cubicNum);

        if (cubicNum >= m_xCubics.Count)
        {
            cubicNum -= 1;
            cubicPos = 1.0f;
        }

        return new Vector3(m_xCubics[cubicNum].Eval(cubicPos),
                   m_yCubics[cubicNum].Eval(cubicPos),
                   m_zCubics[cubicNum].Eval(cubicPos));
    }
        
    /// <summary>
    /// Gets the derivative at t.
    /// </summary>
    /// <returns>The derivative.</returns>
    /// <param name="t">The parameter t between 0 and 1.</param>
    public override float GetDerivative(float t)
    {
        return 1f / m_deltaT;
    }

    /// <summary>
    /// Calculates the spline.
    /// </summary>
    private void CalcSpline()
    {
        CalcNaturalCubic(m_points, m_xCubics, 0);
        CalcNaturalCubic(m_points, m_yCubics, 1);
        CalcNaturalCubic(m_points, m_zCubics, 2);
    }

    /// <summary>
    /// Setup the points if pre-calculation is necessary.
    /// </summary>
    private void SetupPoints()
    {
        this.m_points = new List<Vector3>();
        this.m_xCubics = new List<Cubic>();
        this.m_yCubics = new List<Cubic>();
        this.m_zCubics = new List<Cubic>();

        if (m_knots.Length > 0)
        {
            foreach (Transform knot in m_knots)
            {
                m_points.Add(knot.position);
            }
        }

        CalcSpline();
    }

    private void CalcNaturalCubic(List<Vector3> valueCollection, List<Cubic> cubicCollection, int vectorIndex)
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