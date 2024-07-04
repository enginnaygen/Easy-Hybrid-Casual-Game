using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField] Animator playerAnimator;

    public void ManageAnimations(Vector3 move)  //PlayerMovementte cagriliyor
    {
        if(move.magnitude > 0)
        {
            PlayerRunAnimation();

            playerAnimator.transform.forward = move.normalized; // bu sayede karakter gidilen yöne dönüyor.
        }
        else
        {
            PlayerIdleAnimation();
        }
    }

    void PlayerRunAnimation()
    {
        playerAnimator.Play("RUN");
    }

    void PlayerIdleAnimation()
    {
        playerAnimator.Play("IDLE");
    }
}
