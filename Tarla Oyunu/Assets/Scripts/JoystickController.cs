using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{

    [SerializeField] RectTransform JSOutline;
    [SerializeField] RectTransform JSButton;
    [SerializeField] float moveFactor;
    

    public bool canControlJS;

    Vector3 tapPosition;
    Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        HideJS();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControlJS)
        {
            ControlJS();
        }
    }

    public void TouchedJSZone() //týklandýðýnda unity arayüzünde event trigger componentiyle cagriliyor ve calýsýyor
    {
        tapPosition = Input.mousePosition;
        //Debug.Log("Ekrana Dokunuldu");
        ShowJS();
        JSOutline.position = tapPosition;

    }
    
    public void UnTouchedJSZone() //ekran býrakýldýðýnda unity arayüzünde event trigger componentiyle cagriliyor ve calýsýyor
    {

        //Debug.Log("Ekrandan el çekild");
        HideJS();

    }

    private void ShowJS()
    {
        canControlJS = true;
        JSOutline.gameObject.SetActive(true);
    }

    private void HideJS()
    {
        canControlJS = false;
        JSOutline.gameObject.SetActive(false);
        move = Vector3.zero;

    }

    public Vector3 Move()
    {
        return move;
    }

    void ControlJS()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - tapPosition;

        //float moveMagnitude = direction.magnitude * moveFactor / Screen.width; //burasi muallak kaldi
        //moveMagnitude = Mathf.Min(moveMagnitude, JSOutline.rect.width / 2);

        float canvasYScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.y;
        float moveMagnitude = direction.magnitude * moveFactor * canvasYScale;
        //telefonlarýn boyutlarý deðiþtikçe harekette ona göre hýzlanýp yavaþlýyordu canvasYScale i hesaplayarak ve
        //bunu hareket büyükðünü hesapladýðýmýz yerde çarparak cihazdan cihaza olan farklýlýðýn önüne geçmiþ oluyoruz.

        float joystickOutlineWidth = JSOutline.rect.width / 2;
        float newWidth = joystickOutlineWidth * canvasYScale;

        moveMagnitude = Mathf.Min(moveMagnitude, newWidth);


        move = direction.normalized * moveMagnitude;

        Vector3 targetPos = tapPosition + move;


        JSButton.position = targetPos;

      
    }

 


    
}
