using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    [Header("Dialog Parameters")]
    public List<string> Blurbs;

    [Header("Statics")]
    public static float CharGap = 0.02f;
    public static float PeriodGap = 0.04f;
    public static float PauseGap = 0.25f;
    public static KeyCode ContinueCode = KeyCode.Return;

    [Header("References")]
    public AudioSource BlurbSoundGenerator;
    public TextMeshProUGUI Display;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayDialog());
    }

    public IEnumerator DisplayDialog()
    {
        foreach (string s in Blurbs)
        {
            Display.text = "";
            yield return (DisplayBlurb(s));
            while (!Input.GetKeyDown(ContinueCode))
                yield return null;
        }
    }

    private IEnumerator DisplayBlurb(string blurb)
    {
        foreach (char c in blurb)
        {
            if (c == '#')
            {
                yield return new WaitForSeconds(PauseGap);
            }
            else
            {
                Display.text += c;
                BlurbSoundGenerator.Play();
                if (c == '.')
                {
                    yield return new WaitForSeconds(PeriodGap);
                }
                else
                {
                    yield return new WaitForSeconds(CharGap);
                }
            }
        }
    }
}
