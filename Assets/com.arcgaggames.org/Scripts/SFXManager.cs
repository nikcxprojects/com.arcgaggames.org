using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static AudioSource SFXSource { get; set; }

    private void Awake()
    {
        SFXSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
    }

    public static void PlayOnClick()
    {
        if(SFXSource.isPlaying)
        {
            SFXSource.Stop();
        }

        SFXSource.Play();
    }

    public static void PlayGoalReaction()
    {
        if (SFXSource.isPlaying)
        {
            SFXSource.Stop();
        }

        SFXSource.PlayOneShot(Resources.Load<AudioClip>("goal sfx"));
    }
}
