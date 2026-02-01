using UnityEngine;

public class AmbientChange : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        GameManager.AudioSource.Stop();
        GameManager.AudioSource.clip = clip;
        GameManager.AudioSource.Play();

        Destroy(gameObject);
    }
}
