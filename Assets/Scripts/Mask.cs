using UnityEngine;
using System.Collections.Generic;

public class Mask : MonoBehaviour
{
    public string MaskName => currentMask;

    [SerializeField]
    private string currentMask;

    [SerializeField]
    private List<string> maskNames = new();

    [SerializeField]
    private List<GameObject> maskObjects = new();

    private Dictionary<string, GameObject> masks;

    private void Awake()
    {
        BuildDictionary();
    }

    private void Start()
    {
        HideMasks();
        ShowMask(currentMask);
    }

    private void BuildDictionary()
    {
        masks = new Dictionary<string, GameObject>(maskNames.Count);

        int count = Mathf.Min(maskNames.Count, maskObjects.Count);

        for (int i = 0; i < count; i++)
        {
            if (string.IsNullOrEmpty(maskNames[i]) || maskObjects[i] == null)
                continue;

            if (!masks.ContainsKey(maskNames[i]))
                masks.Add(maskNames[i], maskObjects[i]);
        }
    }

    public void HideMasks()
    {
        foreach (var mask in masks.Values)
        {
            mask.SetActive(false);
        }
    }

    public void SetMask(string maskName)
    {
        if (!masks.TryGetValue(maskName, out var mask))
            return;

        HideMasks();
        currentMask = maskName;
        mask.SetActive(true);
    }

    private void ShowMask(string maskName)
    {
        if (!masks.TryGetValue(maskName, out var mask))
            return;

        mask.SetActive(true);
    }
}