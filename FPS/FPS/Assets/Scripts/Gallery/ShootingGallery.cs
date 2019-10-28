using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingGallery : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreCount;
    public int score
    {
        get { return GameManager.Instance.score;  }
        set
        {
            GameManager.Instance.score = value;
            scoreCount.text = value.ToString() + "/20";
        }
    }
    private int objectiveIndex;

    [SerializeField] private GalleryTimer[] galleryObjectives;

    public static ShootingGallery instance;
    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Two shooting galleries have been created", gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        CloseGallery();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Entering Gallery");
            
        scoreCount.gameObject.SetActive(true);
        score = 0;
        objectiveIndex = 0;
        
        try {
            Invoke(nameof(NextObjective), galleryObjectives[objectiveIndex].timeBeforEevent);
        } catch (Exception) { }
        
        //InvokeRepeating(nameof(DeactivateObjective), 5f, 5f);
    }

    /*private void DeactivateObjective()
    {
        foreach (GalleryTimer objective in galleryObjectives)
            try {
                if (objective.eventToTrigger.gameObject.activeSelf)
                {
                    objective.eventToTrigger.gameObject.SetActive(false);
                    return;
                }
            } catch (Exception) { }
    }*/

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Exiting Gallery");
        
        CloseGallery();
    }

    private void CloseGallery()
    {
        scoreCount.gameObject.SetActive(false);

        foreach (var objective in galleryObjectives)
            try {
                objective.eventToTrigger.gameObject.SetActive(false);
            } catch (Exception) { }
            
        
        CancelInvoke();
    }


    private void NextObjective()
    {
        try {
            galleryObjectives[objectiveIndex].eventToTrigger.DoEvent();
            
        } catch (Exception) { }

        int indxPast = objectiveIndex <= 0 ? galleryObjectives.Length - 1 : objectiveIndex - 1;
        galleryObjectives[indxPast].eventToTrigger.gameObject.SetActive(false);

        objectiveIndex++;

        if (objectiveIndex >= galleryObjectives.Length)
            objectiveIndex = 0;
        
        try {
            Invoke(nameof(NextObjective), galleryObjectives[objectiveIndex].timeBeforEevent);
        } catch (Exception) { }
        

    }
}
