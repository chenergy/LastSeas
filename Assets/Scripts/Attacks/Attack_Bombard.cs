using UnityEngine;
using System.Collections;

public class Attack_Bombard : Attack
{
    public float m_maxRange = 1.0f;
    public GameObject m_targetObject;

    private BattleManager m_battleManager;

    public void Awake()
    {
        m_battleManager = FindObjectOfType<BattleManager>();
    }

    public void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("GroundHit")))
        {
            m_targetObject.transform.position = m_targetObject.transform.position +
                Vector3.ClampMagnitude(hit.point - m_targetObject.transform.position, m_maxRange);
            if (Input.GetMouseButtonDown(0))
            {
                Execute();
            }
        }
    }

    public override void Execute()
    {
        StartCoroutine(_TargetAttack());
    }

    private IEnumerator _TargetAttack()
    {
        float timer = 0.0f;
        float duration = 1.0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        m_battleManager.m_hasAttacked = true;
    }
}

