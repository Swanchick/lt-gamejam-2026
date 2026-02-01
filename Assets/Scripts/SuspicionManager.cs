using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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

    [SerializeField] private Slider slider;

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
        if (playerTransform != null)
            UpdateSuspicion();
        
        slider.value = currentSuspicion / 100f;
    }

    private void UpdateSuspicion()
    {
        float deltaSuspicion = 0f;

        bool seenByEnemy = false;
        bool seenByAlly = false;

        foreach (var npc in npcObjects)
        {
            if (npc.GetComponentInChildren<FOVLogic>().CanSeePlayer(playerTransform))
            {
                Mask npcMask = npc.GetComponentInChildren<Mask>();
                Mask playerMask = playerObj.GetComponentInChildren<Mask>();

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
        else if (isDancing && seenByAlly)
        {
            deltaSuspicion -= suspicionDecayRate * Time.deltaTime;
        }

        currentSuspicion = Mathf.Clamp(currentSuspicion + deltaSuspicion, 0f, maxSuspicion);

        //Debug.Log($"Current Suspicion: {currentSuspicion}");

        if (currentSuspicion >= maxSuspicion)
            OnSuspicionMax();
    }

    private void OnSuspicionMax()
    {
        //Debug.LogError("Player caught!");
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
