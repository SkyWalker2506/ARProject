using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed;
    public Joystick joystick;

    public void FixedUpdate()
    {
        if (MainManager.Instance.SelectedArObject)
        {
            Vector3 direction = MainManager.Instance.MainCamera.forward * joystick.Vertical + MainManager.Instance.MainCamera.right * joystick.Horizontal;
            direction.y = 0;
            MainManager.Instance.SelectedArObject.transform.position += direction * speed * Time.fixedDeltaTime;
        }
    }
}