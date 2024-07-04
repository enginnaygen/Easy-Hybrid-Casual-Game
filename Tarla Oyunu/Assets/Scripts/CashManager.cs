using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{

    public static CashManager instance;

    int coins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        LoadCash();
        DisplayCoins();
    }

    public void ExchangeProduct(ProductData productData)
    {
        AddCoins(productData.productPrice);
    }
    public void AddCoins(int price)
    {
        coins += price;
        DisplayCoins();
    }

    void DisplayCoins()
    {
        UIManage.instance.CoinManageOnScreen(coins);

        //her para degistiginde burasi calisiyor o zaman kaydetmeleri buralarda yapabiliriz
        SaveCash();
    }

    void SpendMoney(int price)
    {
        coins -= price;
        DisplayCoins();
    }
    public bool TryBuyThisUnit(int price)
    {
        if(coins >= price)
        {
            SpendMoney(price);
            return true;
        }

        return false;

    }

    public int GetCoins()
    {
        return coins;
    }


    void LoadCash()
    {
        coins = PlayerPrefs.GetInt("keyCoins", 0); // ,0 kýsmý da default deðeri ifade ediyor
    }

    void SaveCash()
    {
        PlayerPrefs.SetInt("keyCoins", coins);
    }
}
