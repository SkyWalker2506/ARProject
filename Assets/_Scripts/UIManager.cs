using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Joystick Joystick;
    public Transform AnimationHolderParent;
    public Image DownloadIndicator;
    public GameObject ARPlacementIndicator;

    public static UIManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public IEnumerator UpdateDownloadIndicator(ARObjectData arObject)
    {
        while(arObject.TotalProgress<1)
        {
            DownloadIndicator.fillAmount=arObject.TotalProgress;
             yield return null;
        }
        DownloadIndicator.fillAmount = 0;
    }


    public void CreateFireWork()
    {
        Vector3 ps = MainManager.Instance. MainCamera.position + MainManager.Instance.MainCamera.forward * 5;
        GameObject fw = Instantiate(MainManager.Instance.FireWork);
        fw.transform.position = new Vector3(ps.x, 0, ps.z);
        Destroy(fw.gameObject, 9);
    }

    public void Explode()
    {
        ARObject selectedArObject = MainManager.Instance.SelectedArObject;
        if (selectedArObject == null)
            return;
        Vector3 pos = selectedArObject.Root.position + selectedArObject.Root.forward;
        Destroy(Instantiate(MainManager.Instance.ExplosionEffect, pos, Quaternion.identity), 3);
        Collider[] affectedObjects = Physics.OverlapSphere(pos, MainManager.Instance.ExplosionRadius);
        foreach (Collider obj in affectedObjects)
        {

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                ARObject ARObj = obj.transform.root.GetComponent<ARObject>();
                if (!ARObj.isRagdollOn)
                {
                    ARObj.ToggleRagdoll(true);
                    ARObj.isStandingUp = false;
                }
                if (!ARObj.isStandingUp)
                    StartCoroutine(ARObj.StandUp());
                rb.AddExplosionForce(MainManager.Instance.ExplosionForce, pos, MainManager.Instance.ExplosionRadius);
            }
        }
    }
}
