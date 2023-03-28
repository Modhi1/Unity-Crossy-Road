using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// required component animator 
public class PlayerMovement : MonoBehaviour
{
    #region Variables

    // smoothing for the player movement
    private float speed =5f;
    [SerializeField] Animator animator;

    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private bool onLog = false;

    // in another class
    private float logSpeed;
    private int indication;
    private Transform logTransform;

    #endregion


    void Start()
    {
        currentPosition = transform.position;
      
    }

    void Update()
    {
        CheckPlayerMovement();
        MovePlayer();

        if (onLog)
        {
            // put it in another class 
            MoveWithLog(logSpeed, indication, logTransform);
        }

    }

    // need to add if condition for the animator 
    private void CheckPlayerMovement()
    {
        // Up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // rotate -> (0,0,0)
            transform.eulerAngles = new Vector3(0, 0, 0);
            bool check = CheckCollision();
            if (check)
            {
                
                targetPosition.z++;
                animator.Play("Jump");
                CheckForLogs();
            }
            

        }

        // Down
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // rotate -> (0,180,0)
            transform.eulerAngles = new Vector3(0, 180, 0);
            bool check = CheckCollision();
            if (check)
            {
                targetPosition.z--;
                animator.Play("Jump");
                CheckForLogs();
            }
        }

        // Right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // rotate -> (0,90,0)
            transform.eulerAngles = new Vector3(0, 90, 0);
            bool check = CheckCollision();
            if (check)
            {
                targetPosition.x++;
                animator.Play("Jump");
                CheckForLogs();
            }
        }

        // Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // rotate -> (0,-90,0)
            transform.eulerAngles = new Vector3(0, -90, 0);
            bool check = CheckCollision();
            if (check)
            {
                targetPosition.x--;
                animator.Play("Jump");
                CheckForLogs();
            }
        }


    }

    private void MovePlayer()
    {

            targetPosition.y = transform.position.y;
            currentPosition = transform.position;
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            transform.position = newPosition;
    }


    private bool CheckCollision()
    {

        // Vector3.forward -> for the world
        // if the player rotates put transform.Forward -> for the player 
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 0.8f))
        {
            if (hitInfo.collider.tag == "Grass Obstacle")
            {
                // do not increment
                return false;
            }
        }

        return true;
    }


    private void CheckForLogs()
    {
        // remove distance 
        // generate a ray from the player target position downward with distance of 0.7f
        if (Physics.Raycast(targetPosition, -transform.up, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.tag == "Log")
            {
                print("on log");
                logSpeed = hitInfo.collider.gameObject.GetComponent<ObjectMovement>().ObjectSpeed;
                indication = hitInfo.collider.gameObject.GetComponent<ObjectMovement>().Indication;
                logTransform = hitInfo.collider.gameObject.transform;

                onLog = true;
                // let the player move with the log 
            }
            else if (hitInfo.collider.tag == "River")
            {
                //  gameObject.SetActive(false);
                print("player on River -> player loses");
                // player loses 
            }
            else
                onLog = false;
        }

    }

    // for the indication -> make a shared method to use 
    private void MoveWithLog(float logSpeed, int indication, Transform logTransform)
    {
        int direct;
        Vector3 vecDirect;

        if (indication == 1)
        {
            // go left
            vecDirect = -Vector3.right;
            direct = -1;
        }

        else
        {
            // go right
            vecDirect = Vector3.right;
            direct = 1;
        }

      //   transform.Translate(vecDirect * logSpeed * Time.deltaTime);
        
        targetPosition.x += (direct * logSpeed * Time.deltaTime);

    }

}
