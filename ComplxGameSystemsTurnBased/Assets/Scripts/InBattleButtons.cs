using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InBattleButtons : MonoBehaviour
{
    [SerializeField] private Animator m_anim = null;
    [SerializeField] private GameObject m_mainScene = null;
    [SerializeField] private GameObject m_battleScene = null;
    [SerializeField] private GameObject m_selectionButtons = null;
    [SerializeField] private GameObject m_attackButtons = null;
    [SerializeField] private Button button = null;
    //private List<Button> m_attackButton;
    [SerializeField] private Text m_mainLevel;
    [SerializeField] private Text m_mainHealth;
    //[SerializeField] private Slider m_exp;
    [SerializeField] private Slider m_mainHealthSlider;
    [SerializeField] private Slider m_defendHealthSlider;
    [SerializeField] private Text m_defendLevel;
    private Moves move;
    [SerializeField] private Entity m_defendEntity;
    [SerializeField] private Entity m_mainEntity;
    bool hasBattled = false;
    private double saveHealthPlayer;
    private double saveDefendPlayerHealth;
    private void OnEnable()
    {
        m_mainLevel.text = m_mainEntity.GetLevel().ToString();
        m_mainHealth.text = m_mainEntity.GetHealth().ToString() + "/" + saveHealthPlayer.ToString();
        m_defendLevel.text = m_defendEntity.GetLevel().ToString();
        saveHealthPlayer = m_mainEntity.GetHealth();
        saveDefendPlayerHealth = m_mainEntity.GetHealth();
        m_mainHealthSlider.maxValue = (int)saveHealthPlayer;
        m_defendHealthSlider.maxValue = (int)saveDefendPlayerHealth;
    }
    private void Update()
    {
        if (m_selectionButtons != null || m_attackButtons != null)
        {
            if (!m_selectionButtons.activeInHierarchy && !m_attackButtons.activeInHierarchy)
                m_selectionButtons.SetActive(true);
            if (m_selectionButtons.activeInHierarchy)
            {
                m_attackButtons.SetActive(false);
            }
            else if (m_attackButtons.activeInHierarchy)
            {
                m_selectionButtons.SetActive(false);
            }
            if ((m_mainEntity.GetHealth() <= 0 || m_defendEntity.GetHealth() <= 0) && hasBattled == false)
            {
                m_attackButtons.SetActive(false);
                hasBattled = true;
                GameStateOver();
            }
            m_mainHealth.text = m_mainEntity.GetHealth().ToString() + "/" + saveDefendPlayerHealth.ToString();
            m_mainHealthSlider.value = (int)m_mainEntity.GetHealth();
            m_defendHealthSlider.value = (int)m_defendEntity.GetHealth();
        }
    }
    private void GameStateOver() 
    {
        if (m_mainEntity.GetHealth() > 0)
        {
            m_mainEntity.ReturnLevelUp(m_mainEntity.level, m_mainEntity.GainExpMath(m_defendEntity.GetLevel(), (float)m_defendEntity.GetBaseEXPYield()));
        }

        StartCoroutine(TimeBetweenBattle());
        hasBattled = false;
        m_mainEntity.m_health = saveHealthPlayer;
        m_defendEntity.m_health = saveDefendPlayerHealth;
        RunButton();
    }
    IEnumerator TimeBetweenBattle() 
    {
        yield return new WaitForSeconds(5);
    }
    public void RunButton() 
    {
        m_anim.SetTrigger("FadeOut");
        Time.timeScale = 0;
    }
    public void AttackButton() 
    {
        m_attackButtons.SetActive(true);
        if(move == null)
            move = m_mainEntity.GetCurrentMoveset("Water Gun");
        button.GetComponentInChildren<Text>().text = move.name;
        m_selectionButtons.SetActive(false);
    }
    public void ReturnButton() 
    {
        m_selectionButtons.SetActive(true);
        m_attackButtons.SetActive(false);
    }
    public void ButtonAttackPressed() 
    {
        //if (BattleCalc.battleCalc.CompareStatsLHSisLarger(m_mainEntity.GetPrimStats("Speed"), m_defendEntity.GetPrimStats("Speed")))
        //{
        double damage = 0;
        double damageEnemy = 0;
            damage = BattleCalc.battleCalc.AttackMove(move, m_mainEntity, m_defendEntity);
            m_defendEntity.m_health -= damage;
        damageEnemy = BattleCalc.battleCalc.AttackMove(m_defendEntity.GetCurrentMoveset("Fire Punch"), m_defendEntity, m_mainEntity);
            m_mainEntity.m_health -= damageEnemy;
        //}
        //else
        //{
        //    double damageEnemy = BattleCalc.battleCalc.AttackMove(m_defendEntity.GetCurrentMoveset("Fire Punch"), m_defendEntity, m_mainEntity);
        //    m_mainEntity.m_health -= damageEnemy;
        //    double damage = BattleCalc.battleCalc.AttackMove(move, m_mainEntity, m_defendEntity);
        //    m_defendEntity.m_health -= damage;
        //}
    }
    public void UseItems() 
    {
        //Use when the modular game system is in place.
    }
    public void OnFadeComplete() 
    {
        if (m_mainScene.activeInHierarchy)
        {
            m_mainScene.SetActive(false);
            m_battleScene.SetActive(true);
            m_anim.ResetTrigger("FadeOut");
            m_anim.SetTrigger("FadeIn");
        }
        else
        {
            m_mainScene.SetActive(true);
            m_battleScene.SetActive(false);
            m_anim.ResetTrigger("FadeOut");
            m_anim.SetTrigger("FadeIn");
        }
        Time.timeScale = 1;
    }
}
