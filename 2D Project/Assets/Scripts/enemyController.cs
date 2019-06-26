using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    Animator anim;
    public float speed;
    private Transform target;
    private PlayerController player;
    bool playerInZone = false;
    bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInZone = player.getEnemyMove();

        if (playerInZone == true)
        {
            anim.SetBool("Sensed", true);
            transform.position = Vector2.MoveTowards(transform.position,
            target.position, speed * Time.deltaTime);

        }

    }
 
}
