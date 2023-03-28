using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{

    #region Variables

    // make number of Grass pieces in the array high -> to increase the propability of choosing a grass
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform playerPosition;
    private List<GameObject> generatedPieces;
    private Vector3 lastPosition;

    #endregion

    private void Start()
    {
        generatedPieces = new List<GameObject>();
        Spawner();
    }

    private void Update()
    {
        bool generate = CheckPlayerPosition();

        if (generate)
        {
            GenerateNew();
            
        }
    }

    private void Spawner()
    {
        // change it & make it dynamic 
        Vector3 currentPosition = new Vector3(0, -0.5f, 2);
        // at the beginning spawn 25 piece

        for(int i=0; i<26; i++)
        {
            currentPosition.z++;
            GameObject piece = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
            piece.transform.position = currentPosition;
            generatedPieces.Add(piece);

            if (i == 25)
            {
                lastPosition = currentPosition;
            }
        }


    }
    // if the distance between the player and the last piece is less than 15 in Z axis -> generate a piece
    private bool CheckPlayerPosition()
    {
        if(lastPosition.z - playerPosition.position.z < 15)
        {
            // generate another piece
            return true;
        }

        return false;
    }

    private void GenerateNew()
    {

        // Generate a new piece at the end
        lastPosition.z++;
        GameObject piece = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
        piece.transform.position = lastPosition;
        generatedPieces.Add(piece);

        // destroy the first piece
        Destroy(generatedPieces[0]);
        generatedPieces.RemoveAt(0);

    }
}
