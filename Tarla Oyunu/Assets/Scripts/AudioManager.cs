using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AudioClipType { shopClip, grabClip}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shopSound, grabSound;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayAudio(AudioClipType clipType)
    {
        if(audioSource != null) //kontrol icin bu
        {
            AudioClip audioClip = null;

            if(clipType == AudioClipType.shopClip)
            {
                audioClip = shopSound;
            }
            else if(clipType == AudioClipType.grabClip)
            {
                audioClip = grabSound;
            }

            audioSource.PlayOneShot(audioClip, 0.7f); //burada editorden verdigimiz clipleri editorden atadigimiz audiosourcede calistiriyoruz
        }


    }

    public void StopBackgroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
