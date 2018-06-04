using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class TornadoAI : NetworkBehaviour {

    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    private float destroyTimer;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        destroyTimer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        if (destroyTimer >= 13f)
        {
            Destroy(gameObject);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (int.Parse(other.GetComponent<NetworkIdentity>().netId.ToString()) == 6)
        {
            other.transform.parent = gameObject.transform;
            deactivate(other.gameObject);
        }
        else if (int.Parse(other.GetComponent<NetworkIdentity>().netId.ToString()) == 7)
        {
            other.transform.parent = gameObject.transform;
            deactivate(other.gameObject);
        }
    }

    private void deactivate(GameObject player)
    {
        player.GetComponent<MovimientoPersonaje>().enabled = false;
        player.GetComponent<Attack>().enabled = false;
        player.GetComponent<Abilities>().enabled = false;
    }
}
