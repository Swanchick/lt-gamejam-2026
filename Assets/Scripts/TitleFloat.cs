using UnityEngine;

public class TitleFloat : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float floatAmount = 0.15f;
    public float rotationAmount = 5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = startPos + new Vector3(0, yOffset, 0);

        transform.localRotation = Quaternion.Euler(
            0,
            Mathf.Sin(Time.time * floatSpeed) * rotationAmount,
            0
        );
    }
}
