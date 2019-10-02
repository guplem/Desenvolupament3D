using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingGallery : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreCount;
    public int score;
    private int objectiveIndex;

    [SerializeField] private ObjectiveGallery[] galleryObjectives;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entering Gallery");
            
            scoreCount.gameObject.SetActive(true);
            score = 0;
            objectiveIndex = 0;
            NextObjective();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exiting Gallery");
            scoreCount.gameObject.SetActive(false);

            foreach (ObjectiveGallery objective in galleryObjectives)
                objective.gameObject.SetActive(false);

        }
    }


    public void NextObjective()
    {
        galleryObjectives[objectiveIndex].StartMatch(this);
        objectiveIndex++;
    }
}
