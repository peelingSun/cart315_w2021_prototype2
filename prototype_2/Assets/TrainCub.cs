using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCub : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Something entered the training pen.");
        if(other.gameObject.CompareTag("isTrainingCollider"))
        {
            Debug.Log("Cub is training.");
            other.gameObject.GetComponent<Cub>().isInTrainingProgram = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("isTrainingCollider"))
        {
            Debug.Log("Cub is leaving training.");
            other.gameObject.GetComponent<Cub>().isInTrainingProgram = false;
        }
    }
}
