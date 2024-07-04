using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockBakeryUnitController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI bakeryText;
    [SerializeField] int maxStoredProductCount = 20;
    [SerializeField] ProductData.ProductType productType;
    //[SerializeField] ProductType productType;
    int storedProductCount;

    [SerializeField] float useProductInSeconds = 10f;
    [SerializeField] Transform createdCoinTransform;
    [SerializeField] GameObject CoinGO;
    [SerializeField] ParticleSystem smokeEffect;
    float time;



    private void Update()
    {
        if(storedProductCount > 0)
        {
            time += Time.deltaTime;

            if(time >= useProductInSeconds)
            {
                time = 0;
                UseProduct();
                CreateCoin();
            }
        }
    }

    void DisplayProductCount()
    {
        bakeryText.text = storedProductCount + "/" + maxStoredProductCount;
        ControlSmoke();
    }

    public ProductData.ProductType NeededType() //editorden atanan degeri donduruyor
    {
        return productType;
    }

    public bool StoreProduct() // teker teker soruyor boþ yer var mý diye bos yer varsa ver mantiginda
    {
        if(storedProductCount == maxStoredProductCount)
        {
            return false;
        }

        storedProductCount++;
        DisplayProductCount();
        return true;
    }

    void UseProduct()
    {
        storedProductCount--;
    }

    void CreateCoin()
    {
        Vector3 position = Random.insideUnitSphere * 1f;
        Vector3 instansiatePos = createdCoinTransform.position + position;

        Instantiate(CoinGO, instansiatePos, Quaternion.identity);
        DisplayProductCount();
    }


    void ControlSmoke()
    {
        if(storedProductCount ==0  && smokeEffect.isPlaying)
        {
            smokeEffect.Stop();
        }
        else
        {
            if(!smokeEffect.isPlaying)
            {
                smokeEffect.Play();
            }
        }
    }
   
}
