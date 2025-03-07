using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Crea un ray desde la camara hasta la posición del ratón
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Comprobar que el ray toca el suelo (NavMesh)
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            //Mueve el agent a la posicion clickada
            navAgent.SetDestination(hit.point);
        }
    }
}
