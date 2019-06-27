using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS2 : MonoBehaviour
{
    GameObject player;
    PlayerController pc;
    int livesHere;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        livesHere = pc.getLives();
    }

    // Update is called once per frame
    void Update()
    {
        livesHere = pc.getLives();
        if (livesHere < 3)
        {
            gameObject.SetActive(false);

        }

    }
}
