using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Vector2 InitScale { get; set; } = Vector3.one * 0.46165f;
    private Vector2 TargetScale { get; set; } = Vector3.one * 0.2724736f;

    private Transform Center { get; set; }

    public float totalDistance = 4.0f;
    private const float force = 25;

    private AudioSource Source { get; set; }

    private static Rigidbody2D Rigidbody { get; set; }
    private static Vector2 Velocity { get; set; }

    private bool EndTravel { get; set; }
    public static Action OnTravelled { get; set; }
    public static Action<Transform> OnPressed { get; set; }

    public void Pull()
    {
        if(Source.isPlaying)
        {
            Source.Stop();
        }

        Source.Play();

        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");
        Rigidbody.WakeUp();

        Transform target = targets[Random.Range(0, targets.Length)].transform;
        Vector2 direction = target.transform.position - transform.position;

        Rigidbody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        Invoke(nameof(ResetMe), 2.5f);

        OnPressed?.Invoke(target);
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();

        Source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ResetMe();
    }

    private void Update()
    {
        if(!Center)
        {
            Center = GameObject.Find("center").transform;
        }

        float distanceToGoal = Mathf.Abs(Center.position.y - transform.position.y);
        if(distanceToGoal <= 1 && !EndTravel)
        {
            EndTravel = true;
            OnTravelled?.Invoke();
        }

        transform.localScale = Vector2.Lerp(TargetScale, InitScale, distanceToGoal / totalDistance);
    }

    private void ResetMe()
    {
        EndTravel = false;

        Rigidbody.velocity = Vector2.zero;
        Rigidbody.angularVelocity = 0;

        transform.position = new Vector2(-1.4f, -3.68f);
        Rigidbody.Sleep();
    }
}
