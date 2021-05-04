using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBattleButtons : MonoBehaviour
{
    [SerializeField] private Animator m_anim = null;
    [SerializeField] private GameObject m_mainScene = null;
    [SerializeField] private GameObject m_battleScene = null;
    public void RunButton() 
    {
        m_anim.SetTrigger("FadeOut");
        Time.timeScale = 0;
    }
    public void AttackButton() 
    {
        //Use when the project has the modular game system in place.
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
