using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spline3D : BaseSpline
{
    public Transform[] m_knots;

    private List<Vector3> m_points;
    private List<Cubic> m_xCubics;
    private List<Cubic> m_yCubics;
    private List<Cubic> m_zCubics;

    public void Awake()
    {
        SetupPoints ();
    }

    public void OnDrawGizmos()
    {
        if ((!Application.isPlaying) && (m_knots.Length > 0))
        {
            SetupPoints ();
        }

        for (int i = 0; i < 20; i++)
        {
            Gizmos.DrawLine(GetPoint(1.0f * i / 20), GetPoint(1.0f * (i + 1) / 20));
        }
    }

    public Vector3 GetPoint(float position)
    {
        position = position * m_xCubics.Count;
        int cubicNum = (int)position;
        float cubicPos = (position - cubicNum);

        if (cubicNum >= m_xCubics.Count)
        {
            cubicNum -= 1;
            cubicPos = 1.0f;
        }

        return new Vector3(m_xCubics[cubicNum].Eval(cubicPos),
                   m_yCubics[cubicNum].Eval(cubicPos),
                   m_zCubics[cubicNum].Eval(cubicPos));
    }

    private void CalcSpline()
    {
        CalcNaturalCubic(m_points, m_xCubics, 0);
        CalcNaturalCubic(m_points, m_yCubics, 1);
        CalcNaturalCubic(m_points, m_zCubics, 2);
    }

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
}