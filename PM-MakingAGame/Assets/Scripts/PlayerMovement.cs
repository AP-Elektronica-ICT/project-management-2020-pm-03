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

    public Transform attackPoint;


    void Update()
    {
        // Input + linken van animations met de bewegingen
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        // Normaliseren van vector zodat de snelheid constant blijft
        movement.Normalize();

        Animation.SetFloat("Vertical", movement.y);
        Animation.SetFloat("Horizontal", movement.x);
        Animation.SetFloat("Speed", movement.sqrMagnitude);

        // Juiste idle richting
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            Animation.SetFloat("LastMove", 1);
            attackPoint.localPosition = new Vector3(1, 0);
        }
            
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            Animation.SetFloat("LastMove", -1);
            attackPoint.localPosition = new Vector3(-1,0);
        }
        runsound();
            
    }

    private void FixedUpdate()
    {
        RigidBody.MovePosition(RigidBody.position + movement * MovementSpeed * Time.fixedDeltaTime);
    }
    private void runsound()
    {
        
        if (movement.x != 0 || movement.y != 0)
        {
            FindObjectOfType<AudioManager>().Unmute("HeroRun");
            

        }
        else
        {
            FindObjectOfType<AudioManager>().Mute("HeroRun");
        }
    }
}
