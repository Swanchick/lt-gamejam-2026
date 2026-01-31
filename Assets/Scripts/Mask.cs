using UnityEngine;
using System.Collections.Generic;

public class Mask : MonoBehaviour
{
    public string MaskName { get => currentMask; }

    [SerializeField]
    private string currentMask;

    [SerializeField]
    private Dictionary<string, GameObject> masks = new();

    private void Start()
    {
        HideMasks();
        ShowMask(currentMask);
    }

    public void HideMasks()
    {
        foreach (GameObject mask in masks.Values)
        {
            mask.SetActive(false);
        }
    }

    public void SetMask(string maskName)
    {
        if (!masks.ContainsKey(maskName))
        {
            return;
        }

        currentMask = maskName;
        ShowMask(currentMask);
    }

    private void ShowMask(string maskName)
    {
        GameObject mask = masks[maskName];
        if (mask == null)
        {
            return;
        }

        mask.SetActive(true);
    }
}
