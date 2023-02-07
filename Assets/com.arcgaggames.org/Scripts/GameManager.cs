using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get => FindObjectOfType<GameManager>(); }

    public static int score = 0;

    private GameObject LevelPrefab { get; set; }
    private GameObject LevelRef { get; set; }

    private Transform Parent { get; set; }

    private void Start()
    {
        LevelPrefab = Resources.Load<GameObject>("level");
        Parent = GameObject.Find("Environment").transform;
    }

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
        if(!IsCaught)
        {
            SFXManager.PlayGoalReaction();
            score++;
        }
        else
        {
            
        }
    }

    public void StartGame()
    {
        score = 0;

        if (LevelRef)
        {
            Destroy(LevelRef);
        }

        LevelRef = Instantiate(LevelPrefab, Parent);
    }

    public void EndGame()
    {
        Destroy(LevelRef);
    }
}
