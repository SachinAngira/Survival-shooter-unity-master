using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    PlayerHealth playerHealth;
    GameObject player;
    [HideInInspector]
    public static Action OnGameLoad;
    [HideInInspector]
    public static Action OnErrorGameLoad;

    /// <summary>
    /// Adds the Player and PlayerHealth objects in runtime
    /// </summary>
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    /// <summary>
    /// This method gets call from UI SAVE button to save the game progress gets all current game progress and stores it in PlayerData and send it to SaveSystem.cs to save it
    /// </summary>
    public void SaveGame()
    {
        Debug.Log("Saving game...");
        Vector3 pos = player.transform.GetChild(2).position;
        PlayerData playerData = new PlayerData()
        {
            health = playerHealth.CurrentHealth,
            score = ScoreManager.score,
            playerPos = new float[]
              {
                player.transform.position.x,
                player.transform.position.y,
                player.transform.position.z
         }
        };
        SaveSystem.SaveTheGame(playerData);
    }

    /// <summary>
    /// This method gets call from UI Load button to get the game progress from SaveSystem.cs and saves it in as current progress and Invokes OnGameLoad Action if there's no saved data
    /// it invokes OnErrorGameLoad.
    /// </summary>
    public void LoadGame()
    {
        Debug.Log("Loading game...");
        PlayerData playerData = SaveSystem.LoadTheGame();
        if (playerData == null)
        {
            OnErrorGameLoad?.Invoke();
            return;
        }
        playerHealth.CurrentHealth = playerData.health;
        ScoreManager.score = playerData.score;
        player.transform.position = new Vector3(playerData.playerPos[0], playerData.playerPos[1] , playerData.playerPos[2]);
        OnGameLoad?.Invoke();
    }


    private void OnDestroy()
    {
        OnGameLoad = null;
        OnErrorGameLoad = null;
    }
}
