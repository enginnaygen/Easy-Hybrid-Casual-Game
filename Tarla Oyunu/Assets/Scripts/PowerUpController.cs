using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    [SerializeField] PowerUpData powerUpData;
    [SerializeField] int LockedUnitID;
    bool isPoweredUp;


    private void Start()
    {
        isPoweredUp = GetPowerUpStatus();
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && powerUpData.powerUpType == PowerUpType.bagCapacity && !isPoweredUp)
        {
            isPoweredUp = true;
            BagController bagController = other.GetComponent<BagController>();
            bagController.BagBoostCapacity(powerUpData.boostCount);
            PlayerPrefs.SetString("powerUpStatusKey", "used");
        }
    }

   

    bool GetPowerUpStatus()
    {
        string status = PlayerPrefs.GetString("powerUpStatusKey", "ready");

        if(status == "ready")
        {
            return false;
        }

        return true;
    }
}


