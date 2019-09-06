using UnityEngine;
using UnityEngine.UI;

public class SelectedIndicator : MonoBehaviour
{
    public ARObject SelectedObject;
    public Image Indicator;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if (SelectedObject)
        {
            transform.position = new Vector3(SelectedObject.Root.position.x, SelectedObject.transform.position.y, SelectedObject.Root.position.z);
        }
    }
}
