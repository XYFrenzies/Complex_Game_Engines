using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 5.0f;
    [HideInInspector] public Animator m_anim = null;
    private Vector2 m_movement = Vector2.zero;
    private Rigidbody2D m_rb = null;
    public static PlayerMovement m_player;
    // Start is called before the first frame update
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_player = this;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            m_movement.x = Input.GetAxisRaw("Horizontal");
            m_movement.y = Input.GetAxisRaw("Vertical");
            m_anim.SetFloat("Horizontal", m_movement.x);
            m_anim.SetFloat("Vertical", m_movement.y);
            if (m_movement.sqrMagnitude > 1)
            {
                m_anim.SetFloat("Speed", 1);
            }
            else
                m_anim.SetFloat("Speed", m_movement.sqrMagnitude);
        }

    }
    private void FixedUpdate()
    {
        m_rb.MovePosition(m_rb.position + m_movement * m_moveSpeed * Time.deltaTime);
    }
}
