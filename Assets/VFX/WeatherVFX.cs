using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class WeatherVFX : MonoBehaviour
{
    public Sprite cloudSprite;
    public Weather.WeatherType wheatherType;
    [SerializeField] float fadeSpeed = 1f;

    static readonly ExposedProperty sprite = "Sprite";
    static readonly ExposedProperty spawnRate = "Spawn rate";
    static readonly ExposedProperty speed = "Speed";
    static readonly ExposedProperty lifeTime = "Lifetime";
    static readonly ExposedProperty alpha = "Alpha";
    static readonly ExposedProperty xSpawn = "X_Spawn";

    private VisualEffect effect;
    private const float m_ppu = 64f;
    private Camera m_camera;

    public bool IsTransitioning { get; private set; } = false;

    private void Awake()
    {
        effect = GetComponent<VisualEffect>();
        effect.enabled = false;
        effect.SetFloat(alpha, 0f);
        IsTransitioning = false;
    }

    private void Start()
    {
        m_camera = Camera.main;
        float size = m_camera.orthographicSize;
        float width = size * m_camera.aspect;

        Texture tex = effect.GetTexture(sprite);
        float pos = width + tex.width / (2 * m_ppu) ;

        float m_speed = effect.GetFloat(speed);

        effect.SetFloat(xSpawn, Mathf.Sign(m_speed) > 0 ? pos * -1f : pos);
        effect.SetFloat(lifeTime, 2 * pos / m_speed);

    }

    public bool IsRunning { get; private set; }

    public IEnumerator Activate() 
    {        
        IsTransitioning = true;

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
