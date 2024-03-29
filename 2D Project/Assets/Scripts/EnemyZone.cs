﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    private Transform enemyposition;
    // Start is called before the first frame update
    void Start()
    {
        enemyposition = GameObject.FindGameObjectWithTag("enemy").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemyposition.position;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("trigger"))
        {
            gameObject.SetActive(false);

        }
    }
}
