using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public Transform Camera;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Position at Start: " + transform.position);
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobar si estamos en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //comprobar si estamos tocando el groundMask
        //Resetear la velocidad base
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Crear el vector de movimiento
        Vector3 move = Camera.transform.right * x + Camera.transform.forward * z;

        //Movimiento
        controller.Move(move * speed * Time.deltaTime);

        //Comprobar si el jugador puede saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Saltando
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!isGrounded)
        {
            //Caida
            velocity.y += gravity * Time.deltaTime;

        }

        //Generar salto
        controller.Move(velocity * Time.deltaTime);

        if (lastPosition!=gameObject.transform.position && isGrounded==true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;
    }
}
