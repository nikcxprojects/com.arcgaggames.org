using UnityEngine;
using UnityEngine.UI;

public class Tap : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            FindObjectOfType<Ball>().Pull();
        });
    }
}
