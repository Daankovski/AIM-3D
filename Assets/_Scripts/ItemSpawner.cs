﻿using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class ItemSpawner : MonoBehaviour {

    UI ui;

    private int maxAmountOfItems = 5;
    private int currentAmountOfItems = 0;
    [SerializeField]
    private GameObject[] items;

    void Awake() {
        ui = GetComponent<UI>();
    }

    void Start () {
        //starts with producing an item.
        StartCoroutine(produceItem());
    }

    void CheckForPowerUps() {
        
    }

	IEnumerator produceItem()
    {
        //waits between 1 and 10 seconds.
        yield return new WaitForSeconds(Random.Range(1,10));

        if(currentAmountOfItems < maxAmountOfItems)
        {
            //declares a random position to produce an item and then instantiates it.
            Vector3 tempPos = new Vector3(Random.Range(-10, 10), 1f, Random.Range(-10, 10));
            Instantiate(items[Random.Range(0, items.Length)],tempPos,Quaternion.identity);
            currentAmountOfItems++;
        }

        //repeats the process.
        StartCoroutine(produceItem());
    }

    //getter and setter for the current amount of items. it is called for the item script itself.
    public int CurrentAmountOfItems
    {
        get { return currentAmountOfItems; }
        set { currentAmountOfItems = value; }
    }
}
