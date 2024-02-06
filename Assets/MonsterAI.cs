using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AI;


public class MonsterAI : MonoBehaviour
{
    
    private Vector3 WalkTo;
    private bool PointerSet;

    public NavMeshAgent Nav;
    public float WalkingRange;
    public float SightRadius;
    public float MeleeRadius;
    public LayerMask floorlayer, playerlayer;
    public float Health;
    public float PlayerHealth;
    public float timer;
    public float state;

    // Start is called before the first frame update
    private void Awake()
    {
        Nav = GetComponent<NavMeshAgent>();
    }
    public void MovementMode() {
        if (PointerSet) {
            Nav.SetDestination(WalkTo);
        }
        if(!PointerSet) {
            SearchMode();
        }
        Vector3 WalkingDistance = transform.position - WalkTo;
        if(WalkingDistance.magnitude < 1f ) {
            PointerSet = false;
        }
    }
    private void SearchMode() {
        float AxisX = Random.Range(-WalkingRange, WalkingRange);
        float AxisZ = Random.Range(-WalkingRange, WalkingRange);
        WalkTo = new Vector3(transform.position.x + AxisX, transform.position.y, transform.position.z + AxisZ);
        if (Physics.Raycast(WalkTo, -transform.up, 2f, floorlayer)) {
            PointerSet = true;
        }
    }
    public void MeleeAttack(Collider Character) {
        
        if(Character.gameObject.CompareTag ("Player")){
            timer += Time.deltaTime;
            if(timer == 1.5) {
                PlayerHealth -= 1;
                timer = 0;
                //later have the timer check each second to see if an animation is being used if so attack
                if (PlayerHealth <= 0) {
                    Destroy(Character.gameObject);
                }
                // transform.position =
            }

        }
    }
    public void Chase() {

    }

    // Update is called once per frame
    private void Update()
    {
        //try doing a switch case instead
        bool playerInSight = Physics.CheckSphere(transform.position, SightRadius, playerlayer);
        bool playerInMeleeRadius = Physics.CheckSphere(transform.position, MeleeRadius, playerlayer);
        float camView = 60;
        Camera.main.fieldOfView = camView;
        
        if (!playerInSight && !playerInMeleeRadius) {
            MovementMode();
        }
        else if (playerInSight && !playerInMeleeRadius) {
            Chase();
        }
    }
}
