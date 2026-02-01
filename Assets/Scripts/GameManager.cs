using UnityEngine;
using System.Collections;

public enum GameState
{
    Active,
    Paused,
    Won
}

public class GameManager : MonoBehaviour
{
    
    
    public static AudioSource AudioSource
    {
        get
        {
            GameManager instance = Instance;
            return instance.audioSource;
        }
    }

    [SerializeField]
    private AudioSource audioSource;

    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; }

    public static bool IsPaused
    {
        get
        {
            var gameManager = GameManager.Instance;
            return gameManager.State == GameState.Paused || gameManager.State == GameState.Won;
        }
    }

    [Header("Win Setup")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private float zoomDuration = 3f;
    [SerializeField] private float zoomMultiplier = 0.5f;

    private Vector3 cameraOffset;

    private void Start()
    {
        Instance = this;
        State = GameState.Active;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnTargetEliminated()
    {
        if (State != GameState.Active)
            return;

        State = GameState.Won;
        StartCoroutine(WinSequence());
    }

    private IEnumerator WinSequence()
    {
        if (mainCamera == null || player == null)
        {
            Debug.LogError("Main camera or player is null!");
            yield break;
        }

        cameraOffset = mainCamera.transform.position - player.position;

        Vector3 startOffset = cameraOffset;
        Vector3 endOffset = cameraOffset * zoomMultiplier;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / zoomDuration;
            mainCamera.transform.position = player.position + Vector3.Lerp(startOffset, endOffset, t);
            yield return null;
        }

        Time.timeScale = 0f;
        if (winPanel != null)
            winPanel.SetActive(true);
    }
}
