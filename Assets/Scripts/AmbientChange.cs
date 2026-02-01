using UnityEngine;

public class AmbientChange : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "player")
        {
            return;
        }

        GameManager.AudioSource.Play();
        GameManager.AudioSource.clip = clip;
        GameManager.AudioSource.Stop();

        Destroy(gameObject);
    }
}
