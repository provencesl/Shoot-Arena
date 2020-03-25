using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchTracker : MonoBehaviour
{
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private PauseMenuController pauseMenuController;

    private HashSet<GameObject> playersAlive = new HashSet<GameObject>();
    private HashSet<GameObject> playersKilled = new HashSet<GameObject>();

    private bool trackingMatch = false;
    

    void Start() {
        SpawnAllPlayers();
    }

    void Update() {
        //CheckJoiningPlayers();
        CheckMatchState();
    }

    void SpawnAllPlayers() {
        SpawnPlayer(player1Prefab);
        SpawnPlayer(player2Prefab);
    }

    void CheckJoiningPlayers() {
        if(Input.GetButtonDown("Start1") && !HasJoined(player1Prefab)) {
            SpawnPlayer(player1Prefab);
        } else if(Input.GetButtonDown("Start2") && !HasJoined(player2Prefab)) {
            SpawnPlayer(player2Prefab);
        } else if(Input.GetButtonDown("Start3")) {
            Debug.Log("Player Three Joined");
        } else if(Input.GetButtonDown("Start4")) {
            Debug.Log("Player Four Joined");
        }
    }

    bool HasJoined(GameObject player) {
        return GameObject.Find(player.name + "(Clone)");
    }

    void CheckMatchState() {
        if(trackingMatch) {
            foreach (GameObject player in playersAlive) {
                if(player.GetComponent<PlayerController>().lives <= 0) {
                    playersKilled.Add(player);
                }
            }

            foreach(GameObject killedPlayer in playersKilled) {
                playersAlive.Remove(killedPlayer);
            }

            playersKilled.Clear();

            if(playersAlive.Count <= 1 && trackingMatch) {
                foreach (GameObject player in playersAlive) {
                    EndMatch(player);   
                    trackingMatch = false;
                }
            }
        }
    }

    void SpawnPlayer(GameObject player) {
        playersAlive.Add(Instantiate(player));
        if(playersAlive.Count >= 2) {
            trackingMatch = true;
        }
    }

    void EndMatch(GameObject victoriousPlayer) {
        StartCoroutine(pauseMenuController.EndMatch(victoriousPlayer.GetComponent<PlayerController>().playerName));
    }
}
