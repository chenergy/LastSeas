using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIBattleOrder : MonoBehaviour
{
    public GameObject m_characterUIPrefab;

    private RectTransform m_rectTransform;

    private List<UIBattleCharacterOrder> m_battleCharacterOrder;

    public void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_battleCharacterOrder = new List<UIBattleCharacterOrder>();
    }

    public void AddBattleCharacterUI(BattleCharacter character)
    {
        UIBattleCharacterOrder characterUI = Instantiate(m_characterUIPrefab).GetComponent<UIBattleCharacterOrder>();
        characterUI.m_icon.sprite = character.m_icon;
        characterUI.m_name.text = character.m_name;

        RectTransform rectTransform = characterUI.GetComponent<RectTransform>();
        characterUI.transform.SetParent(m_rectTransform);
        rectTransform.anchoredPosition = new Vector2(0, -100 * m_battleCharacterOrder.Count);

        m_battleCharacterOrder.Add(characterUI);
    }
}

