using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image fadeImage;

    public float fadeTime = 2f;

    private float increaseValue = 0f;

    private Color fadeColor = Color.clear;

    public bool isChange = false;
    private bool isFadeOut = true;

    // Start is called before the first frame update
    void Start()
    {
        fadeColor = fadeImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange)
        {
            if (isFadeOut)
            {
                FadeOut();
            }
            else
            {
                FadeIn();
            }
        }
    }

    public void FadeIn()
    {
        if (fadeImage.color.a > 0f)
        {
            increaseValue -= Time.deltaTime / fadeTime;

            fadeColor.a = Mathf.Lerp(0, 1, increaseValue);

            fadeImage.color = fadeColor;
        }
        else
        {
            isChange = false;
            isFadeOut = true;
        }
    }

    public void FadeOut()
    {
        if (fadeImage.color.a < 1f)
        {
            increaseValue += Time.deltaTime / fadeTime;

            fadeColor.a = Mathf.Lerp(0, 1, increaseValue);

            fadeImage.color = fadeColor;
        }
        else
        {
            isFadeOut = false;
            fadeImage.color = Color.black;
        }
    }
}
