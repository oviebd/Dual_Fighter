using System;
using UnityEngine;

[Serializable]
public class PlayerManager
{

    [HideInInspector] public int playerNum;
    public Transform m_SpawnPoint;
    [HideInInspector] public GameObject m_Instance;


    private PlayerMovement _movement;
    private PlayerWeaponManager _playerWeaponManager;


    public void Setup()
    {
        _movement = m_Instance.GetComponent<PlayerMovement>();
        _playerWeaponManager = m_Instance.GetComponent<PlayerWeaponManager>();

        _movement.playerNum = playerNum;
        _playerWeaponManager.playerNum = playerNum;
        m_Instance.name = "Palyer  " + playerNum;
    }
}
