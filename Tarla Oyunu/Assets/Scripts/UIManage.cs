using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManage : MonoBehaviour
{

    public static UIManage instance;

    [SerializeField] TextMeshProUGUI coinText;

    //int totalCoin;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void CoinManageOnScreen(int coins)
    {
        //totalCoin += coin;
        //coinText.text = totalCoin.ToString();
        coinText.text = coins.ToString();
    }
}
