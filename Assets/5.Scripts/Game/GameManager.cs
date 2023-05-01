using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;


public class GameManager : MonoBehaviourPun
{
    #region Componets

    public static GameManager Instance;
    [Header("Player Vars")]
    public string playerPrefabLocation;
    public PlayerController[] players;
    public Transform[] spawnPoints;
    public List<Transform> tempSPs;
    public int playersAlive;

    private int playersInGame;




    #endregion



    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        players = new PlayerController[PhotonNetwork.PlayerList.Length];
        playersAlive = players.Length;
        foreach(Transform Point in spawnPoints)
        {
            tempSPs.Add(Point);
        }

        photonView.RPC("imInGame", RpcTarget.AllBuffered);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void imInGame()
    {
        playersInGame++;
        if(PhotonNetwork.IsMasterClient && playersInGame == PhotonNetwork.PlayerList.Length)
        {
            photonView.RPC("spawnPlayer", RpcTarget.All);
        }
        spawnPlayer();
    }

    public Transform randomSpawnpoint()
    {
      
        int rng = Random.Range(0,tempSPs.Count);
        Transform spawnPoint = tempSPs[rng];
        tempSPs.Remove(spawnPoint);
        return spawnPoint;
    }

    [PunRPC]
    void spawnPlayer()
    {
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabLocation, randomSpawnpoint().position, Quaternion.identity);
        playerObj.GetComponent<PlayerController>().photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
