using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] JoystickController joystickController;
    CharacterController characterController;
    Vector3 moveVector;

    [SerializeField] float gravityMultiplier = 3f;
    [SerializeField] float gravityVelocity;
    float gravity = -9.81f;

    [SerializeField] PlayerAnimator playerAnimator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
 
    void Update()
    {
        MovePlayer();
    }


    void MovePlayer()
    {
        moveVector = joystickController.Move() * moveSpeed * Time.deltaTime / Screen.width;

        moveVector.z = moveVector.y; //ileri gitmesini istedi�im i�in
        moveVector.y = 0; //karakter yukar�ya y�kselmiyor ��nk�

        playerAnimator.ManageAnimations(moveVector);
        ApplyGravity(); // yapay bir yer�ekimi bir �eylerin �st�ne ��karsa insin diye �ok da gerek yok  

        characterController.Move(moveVector);



    }

    void ApplyGravity()
    {
        if(characterController.isGrounded && characterController.velocity.y <= 0)
        {
            gravityVelocity = -1f;
        }

        else
        {
            gravityVelocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        moveVector.y = gravityVelocity;
    }

}
