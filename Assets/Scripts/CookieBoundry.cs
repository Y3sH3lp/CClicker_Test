using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookieBoundry : MonoBehaviour
{
    private Image _cookieImage;

    private void Awake()
    {
        _cookieImage = GetComponent<Image>();
        _cookieImage.alphaHitTestMinimumThreshold = 0.001f;
    }
}
