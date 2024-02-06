using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    public float speed;
    public float timer;
    public void OnTriggerEnter(Collider PickUp) {
        //if the collided object is called health potion add 5 to health then destroy object
        if(PickUp.gameObject.CompareTag("HealthPotion")) {
            health += 5;
            Destroy(PickUp.gameObject);
        }
        //else if its called speed potion set a timer and while its less than 10 set speed to 20
        //if timer equals 10 reset timer 
        else if (PickUp.gameObject.CompareTag("SpeedPotion")) {
            timer += Time.deltaTime;
            Destroy(PickUp.gameObject);
            while(timer < 10) {
                speed = 20;
            }
            if (timer == 10) {
                timer = 0;
                speed = 10;
            }
        }
    }
}
