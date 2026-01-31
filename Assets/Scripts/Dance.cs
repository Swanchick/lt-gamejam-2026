using UnityEngine;

public class Dance : MonoBehaviour
{
    public bool isDancing = false;

    [SerializeField]
    private Animator animator;

    private SuspicionManager suspicionManager;

    private void Start()
    {
        suspicionManager = FindObjectOfType<SuspicionManager>();
    }

    void Update()
    {
        HandleDancingInput();
        animator.SetBool("Dance", isDancing);

        if (isDancing)
        {
            Debug.Log("Dancing...");
            suspicionManager.AddSuspicion(-Time.deltaTime * 3f);


        }
    }

    private void HandleDancingInput()
    {
        isDancing = Input.GetMouseButton(1);

        // Optional: play animation here
        // animator.SetBool("IsDancing", isDancing);
    }
}
