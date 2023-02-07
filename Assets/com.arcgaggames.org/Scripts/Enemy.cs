using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] float force;

    [Space(10)]
    [SerializeField] Sprite middle;
    [SerializeField] Sprite left;
    [SerializeField] Sprite right;


    private static Rigidbody2D Rigidbody { get; set; }
    private SpriteRenderer Renderer { get; set; }


    private GameObject[] Targets { get; set; }

    public static Action<bool> OnBallGaught { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Ball.OnPressed += OnBallPressdEventHandler;
    }

    private void OnDestroy()
    {
        Ball.OnPressed -= OnBallPressdEventHandler;
    }

    private void OnBallPressdEventHandler(Transform target)
    {
        Transform rv = Random.Range(0, 100) > 75 ? target : Targets[Random.Range(0, Targets.Length)].transform;
        Vector2 direction = rv.transform.position - transform.position;

        StartCoroutine(CheckResult(rv, target));
        Renderer.sprite = target.position.x > transform.position.x ? right : left;

        Rigidbody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        Invoke(nameof(ResetMe), 0.5f);
    }

    private IEnumerator CheckResult(Transform rv, Transform target)
    {
        yield return new WaitForSeconds(0.35f);
        OnBallGaught?.Invoke(rv == target);
    }

    private void ResetMe()
    {
        Renderer.sprite = middle;

        Rigidbody.velocity = Vector2.zero;
        Rigidbody.angularVelocity = 0;

        transform.position = new Vector2(0, -0.52f);
        Rigidbody.Sleep();
    }

    private void Update()
    {
        if(Targets != null && Targets.Length > 0)
        {
            return;
        }

        Targets = GameObject.FindGameObjectsWithTag("target");
    }
}
