using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        Instance = this;
    }

    public static void GameOver()
    {
        UiManager manager = Instance;

        if (manager == null)
        {
            return;
        }

        manager.animator.SetTrigger("GameOver");
    }


}
