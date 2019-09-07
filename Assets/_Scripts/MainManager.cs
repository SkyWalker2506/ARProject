using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    GameObject ground;
    public GameObject FireWork;
    public GameObject ExplosionEffect;
    public float ExplosionRadius = 5;
    public float ExplosionForce = 1500;
    // Some animations don't work with some models so I added two different animation list for the models
    public List<AnimationClip> StandingUpAnims1;
    public List<AnimationClip> StandingUpAnims2;
    public Transform MainCamera;
    public SelectedIndicator SelectedIndicator;
    GameObject currentARObjectToLoad;
    public GameObject CurrentARObjectToLoad
    {
        get
        {
            return currentARObjectToLoad;
        }
        set
        {
            if (value)
                UIManager.Instance.ARPlacementIndicator.SetActive(true);
            else
                UIManager.Instance.ARPlacementIndicator.SetActive(false);
            currentARObjectToLoad = value;
        }
    }
    ARObject selectedArObject;
    public ARObject SelectedArObject
    {
        get
        {
            return selectedArObject;
        }
        set
        {
            selectedArObject = value;
            if(UIManager.Instance.AnimationHolderParent.GetChild(0))
            Destroy(UIManager.Instance.AnimationHolderParent.GetChild(0).gameObject);
            if(selectedArObject)
            {
                if(selectedArObject.Root)
                SelectedIndicator.SelectedObject = selectedArObject;
                Instantiate(selectedArObject.AnimationContentHolder, UIManager.Instance.AnimationHolderParent);
                SelectedIndicator.gameObject.SetActive(true);
                SelectedIndicator.Indicator.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), .5f);
                UIManager.Instance.ARPlacementIndicator.SetActive(true);
                value.gameObject.SetActive(true);
            }
            else
            {
                SelectedIndicator.SelectedObject = null;
                SelectedIndicator.gameObject.SetActive(false);
            }
           
        }
    }



    public static MainManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        #if UNITY_EDITOR
             ground.SetActive(true);
        #else
             ground.SetActive(false);
        #endif
    }
}