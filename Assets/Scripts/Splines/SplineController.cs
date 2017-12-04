using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the progression along a set of splines.
/// </summary>
public class SplineController : MonoBehaviour
{
    /// <summary>
    /// The root spline of a tree.
    /// </summary>
    public SplineTreeNode m_treeRoot;

    public BaseSpline FindNextSpline()
    {
        if (m_treeRoot.m_children.Length > 0)
        {
            // TODO: do selection correctly
            int index = Random.Range(0, m_treeRoot.m_children.Length);
            m_treeRoot = m_treeRoot.m_children[index];
            return m_treeRoot.m_spline;
        }

        return null;
    }

//    /// <summary>
//    /// Use this for initialization.
//    /// </summary>
//    public void Start()
//    {
//	    
//    }
//	
//    /// <summary>
//    /// Update is called once per frame.
//    /// </summary>
//    public void Update()
//    {
//	
//    }
}

