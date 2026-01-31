using UnityEngine;

public class Dance : MonoBehaviour
{
    public bool isDancing = false;
    void Update()
    {
        HandleDancingInput();
        if (isDancing)
        {
            Debug.Log("Dancing...");
        }
    }

    private void HandleDancingInput()
    {
        isDancing = Input.GetMouseButton(1);

        // Optional: play animation here
        // animator.SetBool("IsDancing", isDancing);
    }
}
