using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookieDisplay : MonoBehaviour
{
    public void UpdateCookieText(double cookieCount, TextMeshProUGUI textToChange, string optionalEndText = null)
    {
        string[] suffixes = { "", "k", "M", "B", "T", "Q" };
        int index = 0;

        while (cookieCount >= 1000 && index < suffixes.Length - 1)
        {
            cookieCount /= 1000;
            index++;

            if (index >= suffixes.Length - 1 && cookieCount >= 1000)
            {
                break;
            }
        }

        string formattedText;

        if (index == 0)
        {
            formattedText = cookieCount.ToString();
        }

        else
        {
            formattedText = cookieCount.ToString("F1") + suffixes[index];
        }

        textToChange.text = formattedText + optionalEndText;
    }
}
