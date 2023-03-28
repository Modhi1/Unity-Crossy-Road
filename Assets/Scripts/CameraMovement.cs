using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Variables
    // smoothing for the player movement
    [SerializeField] private float smoothMovement;
    [SerializeField] private Transform playerTransform;

    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private Vector3 offset;


    #endregion


    // Start is called before the first frame update
    private void Start()
    {
        currentPosition = transform.position;
        offset = currentPosition - playerTransform.position;

       // targetPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        checkCameraMovement();

    }

    private void checkCameraMovement()
    {
        currentPosition = transform.position;
        targetPosition = playerTransform.position + offset;

        // so the camera do not jump with the player 
        targetPosition.y = currentPosition.y;

        Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, smoothMovement);


        transform.position = newPosition;
    }
}
