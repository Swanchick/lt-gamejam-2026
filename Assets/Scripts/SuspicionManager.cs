using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections.Generic;

public class SuspicionManager : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string npcTag = "NPC";

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
    private List<NPCFOV> allNPCs = new List<NPCFOV>();


    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            playerTransform = playerObj.transform;
        else
            Debug.LogError("No GameObject with Player tag found in scene!");

        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag(npcTag);
        foreach (var npcObj in npcObjects)
        {
            NPCFOV npcFov = npcObj.GetComponent<NPCFOV>();
            if (npcFov != null)
                allNPCs.Add(npcFov);
            else
                Debug.LogWarning($"NPC object {npcObj.name} has no NPCFOV script!");
        }

        if (allNPCs.Count <= 0)
            Debug.LogWarning("No NPCs with NPCFOV found in the scene!");
    }

    void Update()
    {
        if (playerTransform != null && allNPCs.Count > 0)
            UpdateSuspicion();
    }

    private void UpdateSuspicion()
    {
        float deltaSuspicion = 0f;

        bool seenByEnemy = false;
        bool seenByAlly = false;

        foreach (var npc in allNPCs)
        {
            if (npc.CanSeePlayer(playerTransform))
            {
                if (MaskTypesMatches(npc.GetComponent<Mask>().maskType, playerObj.GetComponent<Mask>().maskType))
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
}
