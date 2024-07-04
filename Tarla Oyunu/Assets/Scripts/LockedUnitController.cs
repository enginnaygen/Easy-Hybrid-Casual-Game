using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LockedUnitController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int price;

    [Header("Objects")]
    [SerializeField] GameObject unlockPrefab;
    [SerializeField] GameObject lockedPrefab;
    [SerializeField] TextMeshPro priceText;

    [SerializeField] int ID;
    bool isPurchased;
    string keyUnit = "keyUnit";

    void Start()
    {
        priceText.text = price.ToString();

        LoadUnit();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isPurchased)
        {
            UnlockedUnit();
        }
    }

    

    void UnlockedUnit()
    {
        if(CashManager.instance.TryBuyThisUnit(price))
        {
            AudioManager.instance.PlayAudio(AudioClipType.shopClip);
            Unlock();
            SaveUnit();
        }
    }

    void Unlock()
    {
        isPurchased = true;
        lockedPrefab.SetActive(false);
        unlockPrefab.SetActive(true);
    }

    void LoadUnit()
    {
        string key = keyUnit + ID;

        if (PlayerPrefs.GetString(key) == "saved")
        {
            Unlock();
        }
    }

    void SaveUnit()
    {
        string key = keyUnit + ID; //bu sekilde ID yi editorden atýyoruz ve kaydederken kullanýyoruz ki birbirleinden etkilenmesinler
        PlayerPrefs.SetString(key, "saved");
    }
}
