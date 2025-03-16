using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeUtils : MonoBehaviour
{

    //Fade the weight of a post-processing volume
    public void FadeVolumeWeight(Volume volume, float targetWeight, float fadeSpeed, Action onComplete = null)
    {
        StartCoroutine(FadeVolumeWeightCoroutine(volume, targetWeight, fadeSpeed, onComplete));
    }

    private IEnumerator FadeVolumeWeightCoroutine(Volume volume, float targetWeight, float fadeSpeed, Action onComplete = null)
    {
        float currentWeight = volume.weight;
        while (Mathf.Abs(currentWeight - targetWeight) > 0.01f)
        {
            currentWeight = Mathf.Lerp(currentWeight, targetWeight, fadeSpeed * Time.deltaTime);
            volume.weight = currentWeight;
            yield return null;
        }
        volume.weight = targetWeight;
        onComplete?.Invoke();
    }
}
