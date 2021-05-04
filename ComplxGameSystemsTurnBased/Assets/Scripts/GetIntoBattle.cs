using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIntoBattle : MonoBehaviour
{
    [SerializeField] private GameObject m_battleScene = null;
    [SerializeField] private GameObject m_mainScene = null;
    [SerializeField] private int maxRange = 1;
    [SerializeField] private int minRange = 0;
    [SerializeField] private int numToGet = 1;
    private int randRange = 0;
    private int saveRange = 0;
    public void OnTriggerStay2D(Collider2D collision)
    {
        randRange = saveRange;
        if (PlayerMovement.m_player.m_anim.GetFloat("Speed") > 0)
        {
            randRange = Random.Range(minRange, maxRange);
        }
        if (randRange == numToGet)
        {
            BattleStage();
        }
        saveRange = randRange;
    }

    private void BattleStage()
    {
        m_battleScene.SetActive(false);
        m_mainScene.SetActive(false);
    }
}
