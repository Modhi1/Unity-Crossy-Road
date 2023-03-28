using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    #region Variables

    // a reference of the path model (road/water) scale of x -> so that we can destroy the object based on it 
    private float scaleX;
    public float ScaleX
    {
        get { return scaleX; }
        set { scaleX = value; }
    }
    // must be set from the spwaner -> so it's different from path to path 
    private float objectSpeed;
    public float ObjectSpeed
    {
        get { return objectSpeed; }
        set { objectSpeed = value; }
    }

    // indication is set in the spawner to indicate which direction to choose 
    public int indication;
    public int Indication
    {
        get { return indication; }
        set { indication = value; }
    }
    // vector direction
    private Vector3 direction;

    [SerializeField] private GameObject model;
    #endregion

    private void Start()
    {
        SetDirectionAndRotation();

    }
    private void Update()
    {
       
        // move the object 
        transform.Translate(direction * objectSpeed * Time.deltaTime);

        // this can be change to -> if object is out side of the camera view then destroy the object
        // if the object exceeds the path Model in the x axis -> destroy the object 
        if ((scaleX / 2) < Mathf.Abs(transform.position.x))
        {
           Destroy(gameObject);
        }
        
    }


    // set direction &  rotate object
    private void SetDirectionAndRotation()
    {
     //  Vector3 objectEulerAngles = model.transform.eulerAngles;
        Vector3 objectEulerAnglesOrg = transform.eulerAngles;
        if (indication == 1)
        {
            // object move to left 
            // rotate object to left
            //  objectEulerAngles.y += -90;
            objectEulerAnglesOrg.y += -90;
    
          
        }

        else if (indication == 0)
        {
            // object move to right 
            // rotate object to right
           // objectEulerAngles.y += 90;
            objectEulerAnglesOrg.y += 90;
     


        }
        direction = transform.forward;
        transform.eulerAngles = objectEulerAnglesOrg;
    }
}
