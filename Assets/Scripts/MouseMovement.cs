using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{

    public float mouseSesitivity = 500f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    // Start is called before the first frame update
    void Start()
    {
        //Para bloquear el cursor en el centro de la pantalla y hacerlo invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Hacer los inputs del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSesitivity * Time.deltaTime; //mover de arriba a abajo
        float mouseY = Input.GetAxis("Mouse Y") * mouseSesitivity * Time.deltaTime; //mover de izquierda a derecha

        //Rotación en el eje x (mirar hacia arriba y abajo)
        xRotation -= mouseY;

        //Bloquear rotacion si se sube muchoel cursor
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Rotación en el eje y (mirar hacia la izquierda y derecha)
        yRotation += mouseX;

        //Aplicar la rotacion al transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}
