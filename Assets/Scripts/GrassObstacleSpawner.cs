using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassObstacleSpawner : MonoBehaviour
{
    // maybe create a list of generated obstackes for the pool 

    // take the model x scale to know the size of it
    // substrak 15 from left & right

    // list of available positions
    // loop to randomlly place the obsacles

    #region Variables
    // list of available positions in the x axis 
    private List<int> availablePositions;
    // get the transform of the model to have access to the x scale -> for the list 
    [SerializeField] private Transform modelTransform;
    // array of obsacle prefabs to choose from 
    [SerializeField] private GameObject[] prefabs;
    private float scaleX;
    #endregion

    // onDisable reset the list ?

    private void OnEnable()
    {
        availablePositions = new List<int>();
    }

    private void Start()
    {
        scaleX = modelTransform.localScale.x;

        PopulateList();
        GenerateObstacles();
    }
    // populate the list with available positions 
    private void PopulateList()
    {
        // substract 15 from the edges of the model 
        int value = (int)(scaleX / 2);

        // e.g. loop from -20 to 20 
        for (int i = -value; i <= value; i++)
        {
            // do not add the last 3 positions to the list 
            if (Mathf.Abs(i) > value - 3)
                continue;
            availablePositions.Add(i);
        }
    }

    private void GenerateObstacles()
    {
        // for test put 15 obsacles at random positions 
        // must change 16 to a dynamic number 
        for(int i=0; i<16; i++)
        {
            GameObject obstacle = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);

            // choose a position randomlly from the list 
            int listIndex = Random.Range(0, availablePositions.Count);
            obstacle.transform.position = new Vector3(availablePositions[listIndex], transform.position.y, transform.position.z);

            // remove the chosen position from the list -> so there's no overlap
            availablePositions.RemoveAt(listIndex);

        }
    }
}
