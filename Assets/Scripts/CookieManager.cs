using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class CookieManager : MonoBehaviour
{
    public static CookieManager instance;

     public GameObject MainGameCanvas;
    [SerializeField] private GameObject _upgradeCanvas;
    [SerializeField] private TextMeshProUGUI _cookieCountText;
    [SerializeField] private TextMeshProUGUI _cookiePerSecondText;
    [SerializeField] private GameObject _cookieObj;
    public GameObject CookieTextPopup;
    [SerializeField] private GameObject _background;

    [Space]
    public CookieUpgrade[] CookieUpgrades;
    [SerializeField] private GameObject _upgradeUIToSpawn;
    [SerializeField] private Transform _upgradeUIParent;
    public GameObject CookiesPerSecondObjToSpawn;
    

    public double CurrentCookieCount { get; set; }
    public double CurrentCookiePerSecond { get; set; }

    //Upgrades
    public double CookiesPerClickUpgrade { get; set; }

    private InitializeUpgrades _initializeUpgrades;
    private CookieDisplay _cookieDisplay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _cookieDisplay = GetComponent<CookieDisplay>();

        UpdateCookieUI();
        UpdateCookiesPerSecondUI();

        _upgradeCanvas.SetActive(false);
        MainGameCanvas.SetActive(true);


        _initializeUpgrades = GetComponent<InitializeUpgrades>();
        _initializeUpgrades.Initialize(CookieUpgrades, _upgradeUIToSpawn, _upgradeUIParent);
    }

    #region On Cookie Clicked

    public void OnCookieClicked()
    {
        IncreaseCookie();
        transform.localScale = Vector3.one * 1.5f;

        PopupText.Create(1 + CookiesPerClickUpgrade);
    }

    public void IncreaseCookie()
    {
        CurrentCookieCount += 1 + CookiesPerClickUpgrade;
        UpdateCookieUI();
    }

    #endregion

    #region UI Updates

    private void UpdateCookieUI()
    {
        //_cookieCountText.text = CurrentCookieCount.ToString();
        _cookieDisplay.UpdateCookieText(CurrentCookieCount, _cookieCountText);
    }

    private void UpdateCookiesPerSecondUI()
    {
        //_cookiePerSecondText.text = CurrentCookiePerSecond.ToString() + "P/S";
        _cookieDisplay.UpdateCookieText(CurrentCookiePerSecond, _cookiePerSecondText, "P/S");
    }
    #endregion

    #region Button Presses

    public void OnUpgradeButtonPressed()
    {
        MainGameCanvas.SetActive(false);
        _upgradeCanvas.SetActive(true);
    }

    public void OnResumeButtonPressed()
    {
        _upgradeCanvas.SetActive(false);
        MainGameCanvas.SetActive(true);
    }

    #endregion

    #region Simple Increases

    public void SimpleCookieIncrease(double amount)
    {
        CurrentCookieCount += amount;
        UpdateCookieUI();
    }

    public void SimpleCookiePerSecondIncrease(double amount)
    {
        CurrentCookiePerSecond += amount;
        UpdateCookiesPerSecondUI();
    }

    #endregion

    #region Events

    public void OnUpgradeButtonClick(CookieUpgrade upgrade, UpgradeButtonReferences buttonRef)
    {
        if (CurrentCookieCount >= upgrade.CurrentUpgradeCost)
        {
            upgrade.ApplyUpgrade();

            CurrentCookieCount -= upgrade.CurrentUpgradeCost;
            UpdateCookieUI();

            upgrade.CurrentUpgradeCost = Mathf.Round((float)(upgrade.CurrentUpgradeCost * (1 + upgrade.CostIncreaseMultiplierPerPurchase)));

            buttonRef.UpgradeCostText.text = "Cost: " + upgrade.CurrentUpgradeCost;
        }
    }

    #endregion
}
