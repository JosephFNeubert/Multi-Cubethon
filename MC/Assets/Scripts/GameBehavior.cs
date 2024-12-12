using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameBehavior : MonoBehaviourPun
{
    // Game Manager variables
    bool gameEnded = false;
    public float restartDelay = 1f;
    public GameObject CompleteLevelUI;

    public string playerPrefabLocation;
    public PlayerBehavior[] players;
    public Transform[] spawnpoints;
    private int playersInGame;

    public static GameBehavior instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        players = new PlayerBehavior[PhotonNetwork.PlayerList.Length];
        photonView.RPC("InGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void InGame()
    {
        playersInGame++;

        if (PhotonNetwork.IsMasterClient && playersInGame == PhotonNetwork.PlayerList.Length)
        {
            photonView.RPC("SpawnPlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    void SpawnPlayer()
    {
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabLocation, spawnpoints[Random.Range(0, spawnpoints.Length)].position, Quaternion.identity);
        playerObj.GetComponent<PlayerBehavior>().photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    public PlayerBehavior GetPlayer(int playerId)
    {
        return players.First(x => x.id == playerId);
    }

    public PlayerBehavior GetPlayer(GameObject playerObject)
    {
        return players.First(x => x.gameObject == playerObject);
    }

    // Check for completed level
    public void completeLevel()
    {
        CompleteLevelUI.SetActive(true);
        photonView.RPC("WinGame", RpcTarget.All, PhotonNetwork.NickName);
    }

    [PunRPC]
    void WinGame(string winner)
    {
        Debug.Log("Winner: " + winner);
        Invoke("GoBackToMenu", 5.0f);
    }

    void GoBackToMenu()
    {
        NetworkManager.instance.ChangeScene("StartScreen");
    }

    // End the game
    public void EndGame()
    {
        if(gameEnded == false)
        {
            gameEnded = true;
            Invoke("Restart", restartDelay);
        }

    }

    // Restart function
    void Restart()
    {
        gameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
