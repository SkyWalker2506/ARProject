using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{

    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        hits = new List<ARRaycastHit>();
    }

    void Update()
    {
        if (MainManager.Instance.CurrentARObjectToLoad ==null)
        {
    
            return;
        }

        //For simulating tap effect in Editor
        #if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
               MainManager.Instance.CurrentARObjectToLoad.transform.position = Vector3.zero;
               MainManager.Instance.CurrentARObjectToLoad.transform.rotation = Quaternion.identity;
               MainManager.Instance.SelectedArObject = MainManager.Instance.CurrentARObjectToLoad.GetComponent<ARObject>();
               MainManager.Instance.CurrentARObjectToLoad = null;
        }
        #endif
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        if (IsPointerOverUI(touch.position))
            return;

        if(raycastManager.Raycast(touch.position,hits))
        {
            Pose pose = hits[0].pose;
            MainManager.Instance.CurrentARObjectToLoad.transform.position = pose.position;
            MainManager.Instance.CurrentARObjectToLoad.transform.rotation = pose.rotation;
            MainManager.Instance.SelectedArObject = MainManager.Instance.CurrentARObjectToLoad.GetComponent<ARObject>();
            MainManager.Instance.CurrentARObjectToLoad = null;
        }

    }
    
    bool IsPointerOverUI(Vector2 pos)
    {
        if (EventSystem.current == null)
            return false;
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(pos.x, pos.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


}
