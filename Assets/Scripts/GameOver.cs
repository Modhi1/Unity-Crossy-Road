using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Transform gameOverText;


    private void Start()
    {
        gameOverText.position = PlayerContoller.instance.transform.position;

    }



}
