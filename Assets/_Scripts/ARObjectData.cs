using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ARObjectData 
{
    public GameObject GO;
    public ModelObject ModelObject;
    public ModelTexture Textures;
    public ModelAnimation Animations;
    public ulong TotalSize
    {
        get
        {
            return ModelObject.Size + Textures.Size+ Animations.Size;
        }
    }
    public float TotalProgress
    {
        get
        {
            float progress= (ModelObject.DownloadedSize + Textures.DownloadedSize + Animations.DownloadedSize) /
            (float)TotalSize;
            Debug.Log(progress);
            return progress;
        }
    }


}

[Serializable]
public class ModelObject
{
    public GameObject Model;
    public string ModelLink;
    public ulong Size;
    public ulong DownloadedSize;
    AssetBundle assetBundle;

    public IEnumerator DownloadModel()
    {
        WWW request = WWW.LoadFromCacheOrDownload(ModelLink, 0);
               
        while(!request.isDone)
        {
            DownloadedSize =(ulong)( Size * request.progress);
            Debug.Log(request.progress);
            yield return null;
        }

        if(request.error==null)
        {
            DownloadedSize = Size;
            assetBundle = request.assetBundle;
          
        }
    }

    public void LoadModel()
    {
        Model = assetBundle.LoadAllAssets<GameObject>()[0];
    }

}

[Serializable]
    public class ModelTexture
    {
        public string TextureLink;
         public ulong Size;
          public ulong DownloadedSize;

    public IEnumerator DownloadTexture()
    {
        WWW request = WWW.LoadFromCacheOrDownload(TextureLink, 0);

        while (!request.isDone)
        {
            DownloadedSize = (ulong)(Size * request.progress);

            Debug.Log(request.progress);
            yield return null;
        }

        if (request.error == null)
        {
            DownloadedSize = Size;
            request.assetBundle.LoadAllAssets();
        }
    }
}

[Serializable]
public class ModelAnimation
{
        public GameObject AnimationButtonsHolder;
        public string AnimationLink;
        public ulong Size;
        public ulong DownloadedSize;

    public IEnumerator DownloadAnimation()
        {
            WWW request = WWW.LoadFromCacheOrDownload(AnimationLink, 0);

            while (!request.isDone)
            {
                DownloadedSize = (ulong)(Size * request.progress);
                Debug.Log(request.progress);
                yield return null;
            }
       
            if (request.error == null)
            {
                DownloadedSize = Size;
                AnimationButtonsHolder = request.assetBundle.LoadAllAssets<GameObject>()[0];
            }
        }

}

