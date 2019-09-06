using UnityEngine;
using UnityEngine.UI;

public class SelectedIndicator : MonoBehaviour
{
    public Transform SelectedObject;
    public Image Indicator;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if (SelectedObject)
        {
            transform.position = new Vector3(SelectedObject.position.x, SelectedObject.position.y-.9f, SelectedObject.position.z);
        }
    }
}
