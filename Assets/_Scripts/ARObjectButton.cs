using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARObjectButton: MonoBehaviour
{

    public ARObjectData ARObjectData;
    bool isDownloading;
    public void DownloadAndCreateObject()
    {
        StartCoroutine(CreateObject());
    }

    IEnumerator CreateObject()
    {
        if (ARObjectData!=null && ARObjectData.GO == null)
            yield return DownloadARObject(ARObjectData);
        if(ARObjectData.GO)
        {
            GameObject GO = Instantiate(ARObjectData.GO);
            ARObject ARO = GO.AddComponent<ARObject>();
            ARO.Animation = GO.AddComponent<Animation>();
            ARO.Animation.playAutomatically = true;
            ARO.AnimationContentHolder = Instantiate(ARObjectData.Animations.AnimationButtonsHolder);
            if (MainManager.Instance.CurrentARObjectToLoad)
            {
                ARObject current = MainManager.Instance.CurrentARObjectToLoad.GetComponent<ARObject>();
                Destroy(current.AnimationContentHolder);
                Destroy(current.gameObject);
            }
            MainManager.Instance.CurrentARObjectToLoad = GO;
        }
    }

    IEnumerator DownloadARObject(ARObjectData arObjectData)
    {
        if (!isDownloading)
        {
            isDownloading = true;
        Coroutine indicator= StartCoroutine(UIManager.Instance.UpdateDownloadIndicator(arObjectData));
        yield return arObjectData.ModelObject.DownloadModel();
        yield return arObjectData.Textures.DownloadTexture();
        yield return arObjectData.Animations.DownloadAnimation();
        arObjectData.ModelObject.LoadModel();
        arObjectData.GO = Instantiate(arObjectData.ModelObject.Model);
        arObjectData.GO.SetActive(false);
        UIManager.Instance.DownloadIndicator.fillAmount = 0;
        StopCoroutine(indicator);
            isDownloading = false;
        }
    }
}

