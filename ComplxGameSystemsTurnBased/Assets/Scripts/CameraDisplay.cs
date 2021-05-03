using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisplay : MonoBehaviour
{
    [SerializeField]private Transform m_player = null;
    [SerializeField] private Vector3 m_offset = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(m_player.position.x + m_offset.x,
            m_player.position.y + m_offset.y, m_offset.z);
    }
}
