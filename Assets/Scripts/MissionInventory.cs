using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionInventory
{
    public Dictionary<ItemId, int> m_equippedItems;

    // Use this for initialization
    public MissionInventory(int[] items)
    {
        m_equippedItems = new Dictionary<ItemId, int>();
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] > 0)
            {
                m_equippedItems.Add((ItemId)i, items[i]);
                Debug.Log(string.Format("Adding {0} of {1} to inventory.", items[i], (ItemId)i));
            }
        }
    }
}


