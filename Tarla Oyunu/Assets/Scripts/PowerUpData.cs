using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PowerUpType { bagCapacity }
[CreateAssetMenu(fileName = "PowerUpData", menuName = "ScriptibleObject/PowerUp Data")]
public class PowerUpData : ScriptableObject
{
    public PowerUpType powerUpType;
    public int boostCount;


}
