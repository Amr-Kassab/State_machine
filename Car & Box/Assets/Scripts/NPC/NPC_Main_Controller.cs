using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Main_Controller : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;
    public Transform[] Patrol_Locations;
    bool Arrived = true;
    int counter = 0;
    public float positionoffset;
    public float originalspeed;
    AudioSource audioo;
    public AudioClip Audio;
    bool HonkEnded = true;

    public LayerMask layer;
    public float raycheckDistance = 6f;


    public float oFfset = 2f;
    public float xoffset;
    public float zoffset;

    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        audioo = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        originalspeed = agent.speed;
    }
    public void Patrol()
    {
            if(Arrived)
            {
                Arrived = false;
                agent.SetDestination(Patrol_Locations[counter].position);
            }
            if (Vector3.Distance(transform.position , Patrol_Locations[counter].position) <= positionoffset)
            {
                Arrived = true;
                if (counter >= Patrol_Locations.Length - 1)
                {
                    counter = 0;
                }
                else
                {
                    counter += 1;
                }
            }
    }

    public void stop()
    {
        agent.speed = 0;
    }
    public void continueMoving()
    {
        agent.speed = originalspeed;
    }
    public void interrepted()
    {
        stop();
        StartCoroutine(CarHorn());
    }
    IEnumerator CarHorn()
    {
        if (HonkEnded)
        {
            HonkEnded = false;
            audioo.PlayOneShot(Audio);
            yield return new WaitForSeconds(Random.Range(6f , 7.5f));
            HonkEnded = true;
        }
    }
    public bool CheckIfPlayerIsInTheWay()
    {
        RaycastHit hit;
        Vector3 offset = new Vector3(xoffset, oFfset, zoffset);
        Ray ray = new Ray(transform.position + offset, transform.TransformDirection(Vector3.forward));

       if (Physics.Raycast(ray, out hit , raycheckDistance , playerLayer))
        {
            Debug.DrawRay(transform.position + offset , transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow , 2 , false);
            Debug.Log("HIT!");
            return true;
       }
        else
        {
            Debug.DrawRay(transform.position + offset , transform.TransformDirection(Vector3.forward) * raycheckDistance , Color.black , 2 , false);
            Debug.Log("Did not Hit");
            return false;
        }
    }
}
