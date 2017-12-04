using UnityEngine;
using System.Collections;

/// <summary>
/// Spline bezier.
/// </summary>
public class SplineBezier : BaseSpline
{
    public Transform m_p0;
    public Transform m_c0;
    public Transform m_c1;
    public Transform m_p1;

    [Header("Debug")]
    public SplineBezier m_adjustSpline;

    /// <summary>
    /// Raises the draw gizmos selected event.
    /// </summary>
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Color c = Gizmos.color;

        Gizmos.color = new Color(1, 0, 1, 0.25f);
        Gizmos.DrawWireSphere(m_p0.position, 1f);
        Gizmos.DrawWireSphere(m_c0.position, 1f);
        Gizmos.DrawWireSphere(m_c1.position, 1f);
        Gizmos.DrawWireSphere(m_p1.position, 1f);

        Gizmos.color = new Color(1, 1, 1, 0.25f);
        Gizmos.DrawLine(m_p0.position, m_c0.position);
        Gizmos.DrawLine(m_p1.position, m_c1.position);

        Gizmos.color = c;
    }

    /// <summary>
    /// Determines whether this instance can be drawn in editor.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    public override bool CanBeDrawnInEditor()
    {
        return m_showSplineInEditor && (m_p0 != null) && (m_c0 != null) && (m_c1 != null) && (m_p1 != null);
    }

    /// <summary>
    /// Gets the point on spline at a position t.
    /// </summary>
    /// <returns>The point along the spline.</returns>
    /// <param name="t">T value along the spline.</param>
    public override Vector3 GetPoint(float t)
    {
        float d = 1f - t;
        return d * d * d * m_p0.position
            + 3f * d * d * t * m_c0.position
            + 3f * d * t * t * m_c1.position
            + t * t * t * m_p1.position;
    }

    /// <summary>
    /// Gets the derivative at t.
    /// </summary>
    /// <returns>The derivative.</returns>
    /// <param name="t">The parameter t between 0 and 1.</param>
    public override float GetDerivative(float t)
    {
        // http://gamedev.stackexchange.com/a/27138
        // If we want to advance uniformly, we need to calculate the derivative.
        // We use the tangent vector to approximate the curve at t.
        // dM/dt = t²(-3*P0 + 9*P1 - 9*P2 + 3*P3) + t(6*P0 - 12* P1 + 6*P2) + (-3*P0 + 3*P1)
        Vector3 v1 = (-3 * m_p0.position) + (9 * m_c0.position) + (-9 * m_c1.position) + (3 * m_p1.position);
        Vector3 v2 = (6 * m_p0.position) + (-12 * m_c0.position) + (6 * m_c1.position);
        Vector3 v3 = (-3 * m_p0.position) + (3 * m_c0.position);

        return ((t * t * v1) + (t * v2) + v3).magnitude;
//        return increment / ((t * t * v1) + (t * v2) + v3).magnitude;
    }

    [ContextMenu("AdjustSpline")]
    public void AdjustSpline()
    {
        if (m_adjustSpline == null)
        {
            return;
        }

        m_p0.position = m_adjustSpline.m_p1.position;
        m_c0.position = m_p0.position + (m_adjustSpline.m_p1.position - m_adjustSpline.m_c1.position);
    }

    /// <summary>
    /// Setup the points if pre-calculation is necessary.
    /// </summary>
    protected override void SetupPoints()
    {
    }
}

