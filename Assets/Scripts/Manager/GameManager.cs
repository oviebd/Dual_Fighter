using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerManager[] players;
    public GameObject playerPrefab;




    void Start()
    {
        SpawnAllPlayer();
    }

    private void SpawnAllPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {

            players[i].m_Instance =
                Instantiate(playerPrefab, players[i].m_SpawnPoint.position, players[i].m_SpawnPoint.rotation) as GameObject;
            players[i].playerNum = i + 1;
            players[i].Setup();
            //  players[i].m_Instance.name = i + 1 + " Player ";
        }
    }


    void Update()
    {

    }
}
