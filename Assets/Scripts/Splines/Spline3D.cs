using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spline3D : BaseSpline
{
    public bool m_showSplineInEditor = true;
    public int m_divisions = 20;
    public Transform[] m_knots;

    private List<Vector3> m_points;
    [SerializeField]
    private List<Cubic> m_xCubics;
    [SerializeField]
    private List<Cubic> m_yCubics;
    [SerializeField]
    private List<Cubic> m_zCubics;

    public void Awake()
    {
        SetupPoints();
    }

    public void OnDrawGizmos()
    {
        if (!CanBeDrawn())
        {
            return;
        }

        SetupPoints();
        for (int i = 0; i < m_divisions; i++)
        {
            Gizmos.DrawLine(GetPoint(1.0f * i / m_divisions), GetPoint(1.0f * (i + 1) / m_divisions));
        }
    }

    public bool CanBeDrawn()
    {
        return (m_showSplineInEditor && (m_xCubics != null) && !Application.isPlaying || (m_knots.Length > 0));
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