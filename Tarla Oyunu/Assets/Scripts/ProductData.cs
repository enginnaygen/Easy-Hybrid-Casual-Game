using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ProductData", menuName = "ScriptibleObject/Product Data")]
public class ProductData : ScriptableObject
{
    public enum ProductType {tomato,cabbage}

    public GameObject productPrefabs;
    public ProductType productType;
    public int productPrice;
}
