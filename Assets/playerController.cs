using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerController : MonoBehaviour
{
    public float speed = 5.0f;

    InputDevice rigtHand;
    InputDevice leftHand;
    bool reach;
    float rotacion;
    Vector3 movimiento;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        leftHand = leftHandDevices[0];

        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        rigtHand = rightHandDevices[0];


        leftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystickIzq);
        rigtHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystickDer);
        rotacion = (2.5f+Time.deltaTime) * joystickIzq.x;
        movimiento.z = -joystickDer.x;
        movimiento.x = joystickDer.y;
        transform.Translate(movimiento * speed * Time.deltaTime);


        

        Debug.Log(rotacion);

        transform.Rotate(rotacion * Vector3.down, Space.World);

        //transform.Rotate(0, rotacion, 0);   
        //if (joystickIzq.x > 0.1)
        //{
        //    Debug.Log("");
        //}

    }
}
