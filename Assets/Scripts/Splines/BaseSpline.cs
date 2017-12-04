using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Base spline.
/// </summary>
public abstract class BaseSpline : MonoBehaviour
{
    /// <summary>
    /// Whether to show spline in editor.
    /// </summary>
    public bool m_showSplineInEditor = true;

    /// <summary>
    /// The number of divisions to show in editor.
    /// </summary>
    public int m_divisionsInEditor = 20;

    /// <summary>
    /// Determines whether this instance can be drawn in editor.
    /// </summary>
    /// <returns><c>true</c> if this instance can be drawn; otherwise, <c>false</c>.</returns>
    public abstract bool CanBeDrawnInEditor();

    /// <summary>
    /// Gets the point on spline at a position t.
    /// </summary>
    /// <returns>The point along the spline.</returns>
    /// <param name="t">T value along the spline.</param>
    public abstract Vector3 GetPoint(float position);

    /// <summary>
    /// Gets the derivative at t.
    /// </summary>
    /// <returns>The derivative.</returns>
    /// <param name="t">The parameter t between 0 and 1.</param>
    public abstract float GetDerivative(float t);

    /// <summary>
    /// Setup the points if pre-calculation is necessary.
    /// </summary>
    protected abstract void SetupPoints();

    /// <summary>
    /// Raises the draw gizmos event.
    /// </summary>
    public virtual void OnDrawGizmos()
    {
        if (!CanBeDrawnInEditor())
        {
            return;
        }

        SetupPoints();
        for (int i = 0; i < m_divisionsInEditor; i++)
        {
            Gizmos.DrawLine(GetPoint(1.0f * i / m_divisionsInEditor), GetPoint(1.0f * (i + 1) / m_divisionsInEditor));
        }
    }
}

