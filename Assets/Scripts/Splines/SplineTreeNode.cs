using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A node use in spline tree.
/// </summary>
[RequireComponent(typeof(BaseSpline))]
public class SplineTreeNode : MonoBehaviour
{
    /// <summary>
    /// The spline for this node.
    /// </summary>
    public BaseSpline m_spline;

    /// <summary>
    /// The children for this node.
    /// </summary>
    public SplineTreeNode[] m_children;
}

