using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObjectSelector : MonoBehaviour
{
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (MainManager.Instance.CurrentARObjectToLoad)
                return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                ARObject hitARObject = hit.transform.root.GetComponent<ARObject>();
                if (hitARObject)
                {
                    MainManager.Instance.SelectedArObject = hitARObject;
                }
            }
        }

    }
}
