using UnityEngine;

public class PlayerMaskManager : MonoBehaviour
{
    [SerializeField]
    private Mask playerMask;

    [SerializeField]
    private Mask uiMask;

    public static PlayerMaskManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static void SetMask(string maskName)
    {
        PlayerMaskManager pm = Instance;
        if (pm == null)
        {
            return;
        }

        pm.playerMask.SetMask(maskName);
        pm.uiMask.SetMask(maskName);
    }
}