using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Cookie Upgrade/Cookies Per Click", fileName ="Cookie Per Click")]
public class CookieUpgradePerClick : CookieUpgrade
{
    public override void ApplyUpgrade()
    {
        CookieManager.instance.CookiesPerClickUpgrade += UpgradeAmount;
    }
}
