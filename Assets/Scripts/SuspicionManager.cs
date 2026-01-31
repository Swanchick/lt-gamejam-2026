using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections.Generic;

public class SuspicionManager : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string npcTag = "Enemy";

    [Header("Suspicion Settings")]
    [SerializeField] private float maxSuspicion = 100f;
    [SerializeField] private float suspicionAloneRate = 5f;
    [SerializeField] private float suspicionEnemyRate = 25f;
    [SerializeField] private float suspicionDecayRate = 10f;

    [Header("Player State")]
    private bool isDancing = false;

    [HideInInspector]
    public float currentSuspicion = 0f;

    private GameObject playerObj;
    private Transform playerTransform;
    private GameObject[] npcObjects;


    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            playerTransform = playerObj.transform;
        else
            Debug.LogError("No GameObject with Player tag found in scene!");

        npcObjects = GameObject.FindGameObjectsWithTag(npcTag);
    }

    void Update()
    {
        if (playerTransform != null && npcObjects.Length > 0)
            UpdateSuspicion();
    }

    private void UpdateSuspicion()
    {
        float deltaSuspicion = 0f;

        bool seenByEnemy = false;
        bool seenByAlly = false;

        foreach (var npc in npcObjects)
        {
            if (npc.GetComponent<FOVLogic>().CanSeePlayer(playerTransform))
            {
                Mask npcMask = npc.GetComponentInChildren<Mask>();
                Debug.Log($"NPC Mask: {npcMask.MaskName}");
                Mask playerMask = playerObj.GetComponentInChildren<Mask>();
                Debug.Log($"Player Mask: {playerMask.MaskName}");

                if (MaskTypesMatches(npcMask.MaskName, playerMask.MaskName))
                    seenByAlly = true;
                else
                    seenByEnemy = true;
            }
        }

        // Apply suspicion rules
        if (seenByEnemy)
        {
            deltaSuspicion += suspicionEnemyRate * Time.deltaTime;
        }
        else if (!seenByAlly)
        {
            deltaSuspicion += suspicionAloneRate * Time.deltaTime;
        }

        if (isDancing)
            deltaSuspicion -= suspicionDecayRate * Time.deltaTime;


        currentSuspicion = Mathf.Clamp(currentSuspicion + deltaSuspicion, 0f, maxSuspicion);

        Debug.Log($"Current Suspicion: {currentSuspicion}");

        if (currentSuspicion >= maxSuspicion)
            OnSuspicionMax();
    }

    private void OnSuspicionMax()
    {
        Debug.Log("Player caught!");
        // TODO: trigger game over
    }

    private bool MaskTypesMatches(string mask1, string mask2)
    {
        return mask1 == mask2;
    }

    public void AddSuspicion(float amount)
{
    currentSuspicion = Mathf.Clamp(currentSuspicion + amount, 0f, maxSuspicion);
}

    public bool IsPlayerSeenByAnyNPC()
    {
        foreach (var npc in npcObjects)
        {
            if (npc.GetComponent<FOVLogic>().CanSeePlayer(playerTransform))
                return true;
        }
        return false;
    }

}
