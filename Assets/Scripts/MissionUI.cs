using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MissionUI : MonoBehaviour
{
    public Image m_healthImage;
    public Image[] m_itemImages;

    public void Start()
    {
        m_healthImage.fillAmount = 1.0f;
    }

    public void SetPlayerHealthFill(float fill)
    {
        m_healthImage.fillAmount = fill;
    }

    public void SetUpdateItems(Dictionary<ItemId, int> items)
    {
        for (int i = 0; i < m_itemImages.Length; i++)
        {
            m_itemImages[i].gameObject.SetActive(false);
        }

//        int j = 0;
//        foreach (KeyValuePair<ItemId, int> kvp in items)
//        {
//            m_itemImages[j].sprite = Database. kvp.Key
//        }
    }
}

