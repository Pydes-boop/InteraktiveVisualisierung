using System.Collections;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public KeyCode key = KeyCode.Space; // Which key should trigger the fade?
    public float speedScale = 1f;
    public Color fadeColor = Color.black;
    // Rather than Lerp or Slerp, we allow adaptability with a configurable curve
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1),
        new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public bool startFadedOut = false;

    private bool startDrawing = false;
    private float alpha = 0f;
    private Texture2D texture;
    private int direction = 0;
    private float time = 0f;

    public delegate void FadeDone();
    public event FadeDone OnFadeDone;

    private void Start()
    {
        if (startFadedOut)
        {
            direction = 1;
            alpha = 1f;
            time = 0f;
            StartFade();
        }
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.5f);
        startDrawing = true;
    }

    public void OnGUI()
    {
        if (alpha >= 0f) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        if (direction != 0 && startDrawing == true)
        {
            time += direction * Time.deltaTime * speedScale;
            alpha = Curve.Evaluate(time);
            texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
            texture.Apply();
            if (alpha <= 0f || alpha >= 1f)
            {
                direction = 0;
                OnFadeDone?.Invoke();
            }
        }
    }

    public void StartFade()
    {
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();
        startDrawing = false;
        StartCoroutine(Fade());
    }

    public void setDirection(int dir)
    {
        this.direction = dir;
    }

    public void setTime(float t)
    {
        this.time = t;
    }

    public void setAlpha(float alpha)
    {
        this.alpha = alpha;
    }
}