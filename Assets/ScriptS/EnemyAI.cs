using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    Transform player;
    public Transform[] wayPoints;
    public Transform rayOrigin;
    NavMeshAgent agent;
    int currentWayPointIndex = 0;
    Animator fsm; 
    Vector3[] wayPointsPos = new Vector3[3];
    
    void Start() {

        for (int i = 0; i < wayPoints.Length; i++)

            wayPointsPos[i] = wayPoints[i].position;
       
        fsm = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wayPointsPos[currentWayPointIndex]);
        StartCoroutine("CheckPlayer");
    }

    IEnumerator CheckPlayer()
    {
        CheckDistance();
        CheckVisibility();
        CheckDistanceFromCurrentWaypoint();

        yield return new WaitForSeconds(0.1f);
        yield return CheckPlayer();
    }

    private void CheckDistanceFromCurrentWaypoint() {

        float distance = Vector3.Distance(wayPointsPos[currentWayPointIndex], rayOrigin.position);
        //float distance = (player.position - transform.position).magnitude;
        fsm.SetFloat("distanceFromCurrentWaypoint", distance);
    }

    private void CheckDistance() {

        float distance = Vector3.Distance(player.position, rayOrigin.position);
        //float distance = (player.position - transform.position).magnitude;
        fsm.SetFloat("distance", distance);
    }

    private void CheckVisibility()
    {
        float maxDistance = 20;
        Vector3 direction = (player.position - rayOrigin.position).normalized;
        Debug.DrawRay(rayOrigin.position, direction * maxDistance, Color.red);
        //Vector3 direction2 = (player.position - transform.position) / (player.position - transform.position).magnitude;

        if (Physics.Raycast(rayOrigin.position, direction, out RaycastHit info, maxDistance))  {

            if (info.transform.tag == "Player")

                fsm.SetBool("isVisible", true);

            else

                fsm.SetBool("isVisible", false); 
        }
        else {

            fsm.SetBool("isVisible", false);
        }
    }

    public void SetLookRotation() {

        Vector3 dir = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
    }

    public void Shoot() {

        float shootFreq = 5;
        GetComponent<ShootBehaviour>().Shoot(shootFreq);
    }

    public void Patrol(){
    }

    public void Chase(){

        agent.SetDestination(player.position);
    }

    public void SetNewWayPoint() {

        int ix;
        switch (currentWayPointIndex){

            case 0:
                ix = Random.Range(0, 3);
                while (ix == 0){

                    ix = Random.Range(0, 3);
                }
                currentWayPointIndex = ix; 
                break;
            case 1:
                ix = Random.Range(0, 3);
                while (ix == 1) {

                    ix = Random.Range(0, 3);
                }
                currentWayPointIndex = ix;
                break;
            case 2:
                ix = Random.Range(0, 3);
                while (ix == 2) {

                    ix = Random.Range(0, 3);
                }
                currentWayPointIndex = ix;
                break;
        }
        agent.SetDestination(wayPointsPos[currentWayPointIndex]);
    }
}
