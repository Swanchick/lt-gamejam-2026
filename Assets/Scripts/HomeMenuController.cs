using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class HomeMenuController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject titleText;
    public Image homeBackground;
    public Image introBackground;
    public Button playButton;
    public Button nextButton;
    public RectTransform backstoryImage;

    [Header("Backstory Settings")]
    public float scrollSpeed = 200f;

    private Vector2 backstoryStartPos;
    private Vector2 backstoryTargetPos;

    void Start()
    {
        // Hide elements at start
        nextButton.gameObject.SetActive(false);
        backstoryImage.gameObject.SetActive(false);
        introBackground.gameObject.SetActive(false);

        // Cache positions
        backstoryTargetPos = new Vector2(0, 0);
    }

    public void OnPlayPressed()
    {
        playButton.gameObject.SetActive(false);


        // Change menu to intro
        titleText.SetActive(false);
        introBackground.gameObject.SetActive(true);
        homeBackground.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);

        // Prepare backstory image
        backstoryImage.gameObject.SetActive(true);

        backstoryStartPos = new Vector2(
            0,
            -Screen.height
        );

        backstoryImage.anchoredPosition = backstoryStartPos;

        StartCoroutine(ScrollBackstory());
    }

    IEnumerator ScrollBackstory()
    {
        while (Vector2.Distance(backstoryImage.anchoredPosition, backstoryTargetPos) > 1f)
        {
            backstoryImage.anchoredPosition = Vector2.MoveTowards(
                backstoryImage.anchoredPosition,
                backstoryTargetPos,
                scrollSpeed * Time.deltaTime
            );

            yield return null;
        }

        backstoryImage.anchoredPosition = backstoryTargetPos;


    }

    public void OnNextPressed()
    {
        SceneManager.LoadScene("Level1");
    }
}
