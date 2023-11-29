using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class WeatherVFX : MonoBehaviour
{
    public Weather.WeatherType wheatherType;
    [SerializeField] float fadeSpeed = 1f;

    static readonly ExposedProperty spawnRate = "Spawn rate";
    static readonly ExposedProperty speed = "Speed";
    static readonly ExposedProperty lifeTime = "Lifetime";
    static readonly ExposedProperty alpha = "Alpha";

    private VisualEffect effect;

    public bool IsTransitioning { get; private set; } = false;

    private void Awake()
    {
        effect = GetComponent<VisualEffect>();
        effect.enabled = false;
        effect.SetFloat(alpha, 0f);
        IsTransitioning = false;
    }

    public bool IsRunning { get; private set; }

    public IEnumerator Activate() 
    {
        
        IsTransitioning = true;
        Debug.Log("Activating");
        IsRunning = true;
        effect.enabled = true;

        float alphaValue = effect.GetFloat(alpha);
        while (alphaValue < 1)
        {
            yield return null;
            alphaValue += fadeSpeed * Time.deltaTime;
            effect.SetFloat(alpha, alphaValue);
        }
        IsTransitioning = false;
        yield break;
    }

    public IEnumerator Deactivate() 
    {
        IsTransitioning = true;

        float alphaValue = effect.GetFloat(alpha);
        while (alphaValue > 0)
        {
            yield return null;
            alphaValue -= fadeSpeed * Time.deltaTime;
            effect.SetFloat(alpha, alphaValue);
        }

        effect.enabled = false;
        IsRunning = false;

        IsTransitioning = false;
        yield break;
    }   

}
