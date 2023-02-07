using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Loading : MonoBehaviour
{
    [SerializeField] Button getStarted;
    [SerializeField] Image filled;

    public static Action OnLoadingStarted { get; set; }
    public static Action OnLoadingFinished { get; set; }

    private void Awake()
    {
        getStarted.onClick.AddListener(() =>
        {
            OnLoadingFinished?.Invoke();
            UIManager.Instance.StartGameOnClick();
            Destroy(gameObject);
        });
    }

    private IEnumerator Start()
    {
        OnLoadingStarted?.Invoke();
        getStarted.gameObject.SetActive(false);

        float loadingTime = 2.5f;
        float et = 0.0f;
        while(et < loadingTime)
        {
            filled.fillAmount = et / loadingTime;
            et += Time.deltaTime;
            yield return null;
        }

        getStarted.gameObject.SetActive(true);
    }
}
