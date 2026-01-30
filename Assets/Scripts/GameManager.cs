using UnityEditor.PackageManager;
using UnityEngine;

public enum GameState
{
    Active,
    Paused,
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; }

    public static bool IsPaused 
    { 
        get
        {
            var gameManager = GameManager.Instance;
            return gameManager.State == GameState.Paused;
        }  
    }

    private void Start()
    {
        Instance = this;
    }
}