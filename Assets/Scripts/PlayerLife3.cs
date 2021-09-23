using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife3 : MonoBehaviour
{
    public GameObject heart1,heart2,heart3,gameOver;
    public static int health3;
   
    void Start()
    {
        health3=DBManager.health3;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

       switch(health3){
           case 3:
           heart1.gameObject.SetActive(true);
           heart2.gameObject.SetActive(true);
           heart3.gameObject.SetActive(true);
           break;
           case 2:
           heart1.gameObject.SetActive(true);
           heart2.gameObject.SetActive(true);
           heart3.gameObject.SetActive(false);
           break;
           case 1:
           heart1.gameObject.SetActive(true);
           heart2.gameObject.SetActive(false);
           heart3.gameObject.SetActive(false);
           break;
           case 0:
           heart1.gameObject.SetActive(false);
           heart2.gameObject.SetActive(false);
           heart3.gameObject.SetActive(false);
           gameOver.gameObject.SetActive(true);
           break;
       } 
    }
}