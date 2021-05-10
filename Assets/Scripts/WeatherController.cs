using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private Material sky; //references material in project view
    [SerializeField] private Light sun;

    private float _fullIntensity;
    private float _cloudValue = 0f;

    void Awake() {
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    void Start()
    {
        _fullIntensity = sun.intensity; //initial intensity of light is considered "full" intensity
    }

    private void OnWeatherUpdated() {
        SetOvercast(Managers.Weather.cloudValue);
    }

    private void SetOvercast (float value) //adjust material's blend value and light intensity
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}