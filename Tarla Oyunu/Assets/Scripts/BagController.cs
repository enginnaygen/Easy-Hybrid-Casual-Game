using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagController : MonoBehaviour
{

    [SerializeField] Transform bagTransform;

    [SerializeField] List<ProductData> productDataList;

    Vector3 productSize; // y eksenindeki boyutunu hesaplamak icin

    [SerializeField] TextMeshPro maxText;

    int maxBagCapacity;

    void Start()
    {
        maxBagCapacity = LoadBagCapacity();
    }


    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawn")
        {
            //other.transform.parent = bagTransform;
            AddProductToBag(other.gameObject);
            //Destroy(other.gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ShopPoint")
        {
            PlayshopSound();
            for (int i = productDataList.Count - 1; i >=0 ; i--) //en yukarýdan asagiya dogru siliyor
            {
                Destroy(bagTransform.GetChild(i).gameObject);

                //UIManage.instance.CoinManage(productDataList[i].productPrice);
                //CashManager.instance.AddCoins(productDataList[i].productPrice);
                //yani aslýnda CashManager a gerek yoktu, iki yýkarýdaki satirla da tüm bu iþlemler hallolurdu

                SellProduct(productDataList[i]);
                productDataList.RemoveAt(i);
                
            }

            ControlBagCapacity(); //burasý sadece yazý çýkýp çýkmamasýyla alakalý

        }

        if(other.tag == "UnlockBakeryUnit")
        {         

            UnlockBakeryUnitController unlockBakeryUnitController = other.GetComponent<UnlockBakeryUnitController>();

            ProductData.ProductType neededType = unlockBakeryUnitController.NeededType();

            for (int i = productDataList.Count - 1; i >= 0; i--)
            {
               
                if (productDataList[i].productType == neededType && unlockBakeryUnitController.StoreProduct())
                {
                    
                    Destroy(bagTransform.GetChild(i).gameObject);
                    productDataList.RemoveAt(i);
                }
                /*else   böyle olursa for dongusunden cikar yanlis olur
                {
                    break;
                }*/

            }

            StartCoroutine(PutProductInOrder());
            ControlBagCapacity();
            
        }





    }

    

    void SellProduct(ProductData productData)
    {
        CashManager.instance.ExchangeProduct(productData);

    }

    public void AddProductToBag(ProductData productData)
    {
        //if (productList.Count > 5) return;
        //if (!IsAnySpace()) return;
        GameObject box = Instantiate(productData.productPrefabs, Vector3.zero, Quaternion.identity);
        box.transform.SetParent(bagTransform, true);

        CalculateObjectSize(productData.productPrefabs);
        float YPosition = NewPositionOfBox();

        box.transform.localRotation = Quaternion.identity;
        box.transform.localPosition = new Vector3(0, YPosition, 0);
        //box.transform.localPosition = Vector3.zero;

        productDataList.Add(productData);

        ControlBagCapacity();  //burasý sadece yazý çýkýp çýkmamasýyla alakalý
    }

    

    float NewPositionOfBox() // y ekseninde yukarýya doðru artacak
    {
        float newYPos = productSize.y * productDataList.Count; //0.35*0,1*0.35, 2*0.35 ... diye gidiyor yaklaþýk
        return newYPos;
        
    }

    void CalculateObjectSize(GameObject gameObject) //en baþta 0, kasanýn boyutunu burda alýyor, meshRenderer kullanarak hesaplanacak
    {
        if(productSize == Vector3.zero)
        {
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            productSize = renderer.bounds.size;
        }
        
    }

    public int ReturnBoxCount()
    {
        return productDataList.Count;
    }

    public int ReturnMaxBxCount()
    {
        return maxBagCapacity;
    }
    void ControlBagCapacity()
    {
        if(productDataList.Count == maxBagCapacity)
        {
            maxText.text = "MAX";
            maxText.gameObject.SetActive(true);
        }
        else
        {
            maxText.gameObject.SetActive(false);

        }
    }

    IEnumerator PutProductInOrder()
    {

        yield return new WaitForSeconds(0.2f);

        for(int i=0; i< bagTransform.childCount; i++)
        {
            float newPosY = productSize.y * i;
            bagTransform.GetChild(i).localPosition = new Vector3(0, newPosY, 0);

        }
    }

    void PlayshopSound()
    {
        if(productDataList.Count>0)
        {
            AudioManager.instance.PlayAudio(AudioClipType.shopClip);
        }
    }

    public void BagBoostCapacity(int boostCount)
    {
        maxBagCapacity += boostCount;
        PlayerPrefs.SetInt("bagCapacityKey", maxBagCapacity);
        ControlBagCapacity();
    }

    int LoadBagCapacity()
    {
        int maxBag = PlayerPrefs.GetInt("bagCapacityKey",5);
        return maxBag;
    }

    /*private bool IsAnySpace()
    {
        if (productDataList.Count == maxBagCapacity)
        {
            return false;
        }
        else
            return true;
    }*/
}
