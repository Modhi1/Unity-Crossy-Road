using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    #region

    public static PlayerContoller instance;
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem particle;
 //   [SerializeField] private GameObject gameOver;

    #endregion

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            return;
        }

        Destroy(gameObject);
    }



    private void OnTriggerEnter(Collider collision)
    {
        // play particale system
        if (collision.gameObject.tag == "Grass Obstacle" || collision.gameObject.tag == "Piece" || collision.gameObject.tag == "Log")
        {
           //player didn't lose
            return;
        }

        print("GAME OVER");
        particle.Play();
        particle.transform.position = transform.position;
        // deactivate script objectMovement

            //gameOver.SetActive(true);
        

        
        player.SetActive(false);

    }




}
