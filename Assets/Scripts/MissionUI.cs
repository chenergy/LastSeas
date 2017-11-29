using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MissionUI : MonoBehaviour
{
    public Image m_healthImage;
    public MissionUIItem[] m_itemImages;

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
        Debug.Log(items.Count);
        for (int i = 0; i < m_itemImages.Length; i++)
        {
            m_itemImages[i].gameObject.SetActive(false);
        }

        int j = 0;
        foreach (KeyValuePair<ItemId, int> kvp in items)
        {
            Debug.Log(kvp.Key.ToString());
            m_itemImages[j].gameObject.SetActive(true);
            m_itemImages[j].m_icon.sprite = Database.Instance.GetItemData(kvp.Key).m_icon;
            m_itemImages[j].m_num.text = kvp.Value.ToString();
            j++;
        }
    }
}

