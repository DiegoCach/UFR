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

        if (gameObject.transform.GetChild(5) != null)
        {
            if (destroyTimer >= 12f && (gameObject.transform.GetChild(5).gameObject.name == "Player1" || gameObject.transform.GetChild(5).gameObject.name == "Player2"))
            {
                activate(gameObject.transform.GetChild(5).gameObject);
                gameObject.transform.GetChild(5).gameObject.transform.parent = null;
            }
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
        if (int.Parse(other.GetComponent<NetworkIdentity>().netId.ToString()) == 10)
        {
            other.transform.parent = gameObject.transform;
            deactivate(other.gameObject);
        }
        else if (int.Parse(other.GetComponent<NetworkIdentity>().netId.ToString()) == 11)
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

    private void activate(GameObject player)
    {
        player.GetComponent<MovimientoPersonaje>().enabled = true;
        player.GetComponent<Attack>().enabled = true;
        player.GetComponent<Abilities>().enabled = true;
    }
}
