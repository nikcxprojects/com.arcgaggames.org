using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get => FindObjectOfType<UIManager>(); }

    [SerializeField] GameObject loading;
    [SerializeField] GameObject game;

    [Space(10)]
    [SerializeField] Text scoreText;

    private void OnEnable()
    {
        Enemy.OnBallGaught += OnBallGaughtEventHandler;
    }

    private void OnDestroy()
    {
        Enemy.OnBallGaught -= OnBallGaughtEventHandler;
    }

    private void OnBallGaughtEventHandler(bool IsCaught)
    {
        if (!IsCaught)
        {
            scoreText.text = $"SCORE: {GameManager.score}";
        }
    }

    private void Awake()
    {
        scoreText.text = $"SCORE: {GameManager.score}";

        Loading.OnLoadingStarted += () =>
        {
            game.SetActive(false);
        };

        Loading.OnLoadingFinished += () =>
        {
            game.SetActive(true);
        };
    }

    private void Start()
    {
        loading.SetActive(true);
    }

    public void StartGameOnClick()
    {
        game.SetActive(true);

        GameManager.Instance.StartGame();
        scoreText.text = $"SCORE: {GameManager.score}";
    }
}
