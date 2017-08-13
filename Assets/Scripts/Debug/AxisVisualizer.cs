using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AxisVisualizer : MonoBehaviour
{
    public Slider m_hSlider;
    public Slider m_vSlider;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    public void Update()
    {
        m_hSlider.value = Input.GetAxis("Horizontal");
        m_vSlider.value = Input.GetAxis("Vertical");
    }
}

