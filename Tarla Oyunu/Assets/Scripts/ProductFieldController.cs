using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProductFieldController : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isReadyToPick = true;

    BagController bagController;
    [SerializeField] ProductData productData;
    //[SerializeField] GameObject boxGO;

    Vector3 originalScale;
    void Start()
    {
        originalScale = transform.localScale;
    }
        
  

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && isReadyToPick)
        {
            bagController = other.GetComponent<BagController>();

            if (bagController.ReturnBoxCount() >= bagController.ReturnMaxBxCount())
            {
                return; 
            }
            AudioManager.instance.PlayAudio(AudioClipType.grabClip);
            bagController.AddProductToBag(productData);
            isReadyToPick = false;
            ProductPicked();
        }
    }

    void ProductPicked()
    {
        transform.localScale = originalScale / 3;
        StartCoroutine(WaitForGrow());
    }

    WaitForSeconds wait = new WaitForSeconds(2f);
    IEnumerator WaitForGrow()
    {
        /*float duration = 1f;
        float timer = 0f;

        Vector3 targetScale = originalScale / 3;

        while (timer<duration)
        {
            float t = timer / duration;
            Vector3 newScale = Vector3.Lerp(originalScale, targetScale, t);
            transform.localScale = newScale;
            timer += Time.deltaTime;
            yield return null;

        }
        yield return wait;
        timer = 0f;
        float growBackDuration = 1f;

        while(timer < growBackDuration)
        {
            float t = timer / duration;
            Vector3 newScale = Vector3.Lerp(targetScale, originalScale, t);
            transform.localScale = newScale;
            timer += Time.deltaTime;
            yield return null;
        }

        isReadyToPick = true;
        yield return null;*/

        yield return wait;
        transform.localScale = originalScale / 1.5f;
        yield return wait;
        transform.localScale = originalScale;

        isReadyToPick = true;
    }
}
