using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100; //vida zombie
    private Animator animator;

    private NavMeshAgent navAgent;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }
    
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)//si atacas al zombie y aun no tiene la vida en 0
        {
            int randomValue = Random.Range(0, 2); //0 o 1

            if (randomValue==0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }
            isDead = true;

            //Dead sound
            SoundManager.THIS.zombieChannel2.PlayOneShot(SoundManager.THIS.zombieDeath);
        }
        else //realiza la animación de daño
        {
            animator.SetTrigger("DAMAGE");
            //Hurt sound
            SoundManager.THIS.zombieChannel2.PlayOneShot(SoundManager.THIS.zombieHurt);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); //Attacking//Stop Attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f); //Detection (start chasing)

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f); //Stop chasing
    }
}
