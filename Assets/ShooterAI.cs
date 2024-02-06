using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAI : MonoBehaviour
{
    private Vector3 WalkTo;
    private bool PointerSet;

    public NavMeshAgent Nav;
    public float WalkingRange;
    public float SightRadius;
    public float MeleeRadius;
    public float Stunned;
    public LayerMask floorlayer, playerlayer;
    public float Health;
    public float PlayerHealth;
    public float timer;
    public Rigidbody bullet;

    // Start is called before the first frame update
    private void Awake() {
        Nav = GetComponent<NavMeshAgent>();
    }
    public void MovementMode() {
        if (PointerSet) {
            Nav.SetDestination(WalkTo);
        }
        if (!PointerSet) {
            SearchMode();
        }
        Vector3 WalkingDistance = transform.position - WalkTo;
        if (WalkingDistance.magnitude < 1f) {
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
    public void Shoot() {
        timer += Time.deltaTime;
        if (timer == 1.5) {
            GameObject bullet = new GameObject();
            bullet.AddComponent<Rigidbody>();
            bullet.AddComponent<SphereCollider>();
            GameObject.FindGameObjectWithTag("bullet");
        }
    }
    public void Sight() {
        //float AxisX = Mathf.Lerp(transform.position.x);
        //float AxisY = Mathf.Lerp(transform.position.y);
        //float AxisZ = Mathf.Lerp(transform.position.z);

    }
    // Update is called once per frame
    private void Update() {
        bool playerInSight = Physics.CheckSphere(transform.position, SightRadius, playerlayer);

        if (!playerInSight) {
            MovementMode();
            Shoot();
        }
        if (playerInSight) {
            
        }

    }
}
