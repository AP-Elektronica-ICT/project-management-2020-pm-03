using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 5f;

    // zorgt ervoor dat de hero niet door de grond gaat, kan bewegen,...
    public Rigidbody2D RigidBody;

    // Vector aanmaken waar we x-co en y-co in opslagen
    Vector2 movement;

    public Animator Animation;


    void Update()
    {
        // Input + linken van animations met de bewegingen
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        Animation.SetFloat("Vertical", movement.y);
        Animation.SetFloat("Horizontal", movement.x);
        Animation.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        RigidBody.MovePosition(RigidBody.position + movement * MovementSpeed * Time.fixedDeltaTime);
    }
}
