using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissionUI : MonoBehaviour
{
    public Image m_healthImage;

    public void Start()
    {
        m_healthImage.fillAmount = 1.0f;
    }

    public void SetPlayerHealthFill(float fill)
    {
        m_healthImage.fillAmount = fill;
    }
}

