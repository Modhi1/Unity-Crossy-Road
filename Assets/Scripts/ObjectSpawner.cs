using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{

    #region Variables

    // list of cars prefabs -> so the cars are different from road to road 
    [SerializeField] private GameObject[] prefabs;
    // get the transform of the spawner object -> to change the position of the instantiated object
    [SerializeField] private Transform spawnerTransform;
    // get the transform of the model to have access to the x scale -> to set it in the object movement 
    [SerializeField] private Transform modelTransform;
    // spawn rate is dynamic -> so it's different from road to road
    private float spawnRate;
    // index for randomlly choosing a car prefab from the list
    private int index;
    // var to set the object  speed when moving
    private float objectSpeed;
    //
    private int indication;
    //
    private float scaleX;

    #endregion


    private void OnEnable()
    {
        SetValues();

        UpdateSpawnerPosition();

    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawner), 0, spawnRate);
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(Spawner));
    }

    private void SetValues()
    {
        spawnRate = Random.Range(2, 5);
        objectSpeed = Random.Range(3, 7);
        index = Random.Range(0, prefabs.Length);
        scaleX = modelTransform.localScale.x;
        indication = Random.Range(0, 2);

        
    }

    private void Spawner()
    {
        // instantiate an object
        // this way is to Instantiate under the parent instead of the other way were you Instantiate under the root
        //   prefab   |      position   
        GameObject obj = Instantiate(prefabs[index], transform);

        obj.transform.position = spawnerTransform.position;
        SetObjectValues(obj);


    }

    private void SetObjectValues(GameObject obj)
    {
        obj.GetComponent<ObjectMovement>().ObjectSpeed = objectSpeed;
        obj.GetComponent<ObjectMovement>().indication = indication;
        obj.GetComponent<ObjectMovement>().ScaleX = scaleX;
    }

    private void UpdateSpawnerPosition()
    {
        float xDirection;
        // indication is 0 or 1
        // indication = 0 -> spawner is at the left of the path model && vector moves right
        // indication = 1 -> spawner is at the right of the path model && vector moves left
 
        if (indication == 0)
            xDirection = -(scaleX / 2);
        else
            xDirection = scaleX / 2;
        spawnerTransform.position = new Vector3(xDirection, spawnerTransform.position.y, spawnerTransform.position.z);


    }


}
