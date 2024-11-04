using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookiePerSecondTimer : MonoBehaviour
{
    public float TimerDuration = 1f;

    public double CookiePerSecond { get; set; }

    private float _counter;

    private void Update()
    {
        _counter += Time.deltaTime;
        if (_counter >= TimerDuration)
        {
            CookieManager.instance.SimpleCookieIncrease(CookiePerSecond);

            _counter = 0;
        }
    }
}
