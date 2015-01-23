using UnityEngine;
using System.Collections;

public class PingPongShaderColor : MonoBehaviour
{
    // Source (from) color
    public Color source = Color.white;

    // Destination (to) color
    public Color dest = Color.white;

    // Total time in seconds to transition from source to dest
    public float transitionTime = 0.5f;

    private MeshRenderer[] meshRenderers = null;

    // Use this for initialization
    void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }
    
    public void Play()
    {
        // Stop all running coroutines
        StopAllCoroutines();

        // Start new sequence
        StartCoroutine(PlayLerpColors());
    }

    private IEnumerator PlayLerpColors()
    {
        yield return StartCoroutine(LerpColor(source, dest));
        yield return StartCoroutine(LerpColor(dest, source));
    }

    private IEnumerator LerpColor(Color from, Color to)
    {
        float elapsedTime = 0.0f;

        // Loop for transition time
        while (elapsedTime <= transitionTime) {
            elapsedTime += Time.deltaTime;

            // Set mesh renderer colors
            foreach(MeshRenderer render in meshRenderers) {
                if (render == null) {
                    yield break;
                }

                render.material.color = Color.Lerp(from, to, Mathf.Clamp(elapsedTime/transitionTime, 0f, 1f));
            }

            // Wait until next frame
            yield return null;
        }

        foreach(MeshRenderer render in meshRenderers) {
            render.material.color = to;
        }
    }
}
