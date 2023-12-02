using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [Flags]
    public enum WeatherType
    {
        Sun = 1,
        Cloudy = 2,
        Rain = 4,
        Thunder = 8
    }

    public WeatherType currentWeather;
    
    private List<WeatherVFX> weatherVFXElements;

    // Start is called before the first frame update
    void Start()
    {
        weatherVFXElements = new(GameObject.FindObjectsOfType<WeatherVFX>(true));
    }

    private void Update()
    {
        RefreshWeather();
    }

    private void RefreshWeather()
    {
        foreach (WeatherVFX weather in weatherVFXElements)
        {
            if (currentWeather != 0)
            {
                if (currentWeather.HasFlag(weather.wheatherType))
                {
                    if (!weather.IsTransitioning && !weather.IsRunning)
                    {
                        StartCoroutine(weather.Activate());
                    }
                }
                else
                {
                    if (!weather.IsTransitioning && weather.IsRunning)
                    {
                        StartCoroutine(weather.Deactivate());
                    }
                }
            }
            else
            {
                if (!weather.IsTransitioning && weather.IsRunning)
                {
                    StartCoroutine(weather.Deactivate());
                }
            }
        }
    }

}
