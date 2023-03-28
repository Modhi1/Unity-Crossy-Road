using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemStop : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;

    public void OnParticleSystemStopped()
    {
        gameOver.SetActive(true);
    }


}
