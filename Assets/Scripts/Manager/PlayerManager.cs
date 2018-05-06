using System;
using UnityEngine;

[Serializable]
public class PlayerManager
{

    [HideInInspector] public int playerNum;
    public Transform m_SpawnPoint;
    [HideInInspector] public GameObject m_Instance;


    private PlayerMovement m_Movement;


    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<PlayerMovement>();
        m_Movement.playerNum = playerNum;
    }
}
