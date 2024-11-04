using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    [SerializeField] private float _startingVelocity = 750f;
    [SerializeField] private float _velocityDecayRate = 1500f;
    [SerializeField] private float _timeBeforeFadeStarts = 0.6f;
    [SerializeField] private float _fadeSpeed = 3f;

    private TextMeshProUGUI _ClickAmountText;

    private Vector2 _currentVelocity;

    private Color _startColor;
    private float _timer;
    private float _textAlpha;


    private void OnEnable()
    {
        _ClickAmountText = GetComponent<TextMeshProUGUI>();

        Color newColor = _ClickAmountText.color;
        newColor.a = 1f;
        _ClickAmountText.color = newColor;

        _startColor = newColor;
        _timer = 0f;
        _textAlpha = 1f;

    }


    public static PopupText Create(double amount)
    {
        GameObject popupObj = ObjectPoolManager.SpawnObject(CookieManager.instance.CookieTextPopup, CookieManager.instance.MainGameCanvas.transform);
        popupObj.transform.position = CookieManager.instance.MainGameCanvas.transform.position;

        PopupText cookiePopup = popupObj.GetComponent<PopupText>();
        cookiePopup.Init(amount);

        return cookiePopup;
    }

    public void Init(double amount)
    {
        _ClickAmountText.text = "+" + amount.ToString("0");

        float randomX = Random.Range(-300f, 300f);
        _currentVelocity = new Vector2(randomX, _startingVelocity);
    }

    private void Update()
    { 
        //move
        _currentVelocity.y -= Time.deltaTime * _velocityDecayRate;
        transform.Translate(_currentVelocity * Time.deltaTime);

        //change color
        _timer += Time.deltaTime;
        if (_timer > _timeBeforeFadeStarts)
        {
            _textAlpha -= Time.deltaTime * _fadeSpeed;
            _startColor.a = _textAlpha;
            _ClickAmountText.color = _startColor;

            if (_textAlpha <= 0f)
            {
                ObjectPoolManager.ReturnObjectToPool(gameObject);
            }
        }
    }
}
