using UnityEngine;
using System.Collections;

public class IngameUI : MonoBehaviour
{
    public UnityEngine.UI.Image m_healthImage;

    public void Start()
    {
        m_healthImage.fillAmount = 1.0f;
    }

    public void SetPlayerHealthFill(float fill)
    {
        m_healthImage.fillAmount = fill;
    }
}

