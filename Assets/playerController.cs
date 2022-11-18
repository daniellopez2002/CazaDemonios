using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerController : MonoBehaviour
{
    public float speedRotation = 5.0f;
    public float movementSpeed = 1.0f;
    float rotacion;

    InputDevice rigtHand;
    InputDevice leftHand;

    bool flashLight;
    public float batery = 100.0f;
    float maxBatery = 100.0f;
    float bateryDcrease = 5.0f;
    float bateryIncrease = 15.0f;




    public float stamina = 100.0f;
    float maxStamina = 100.0f;
    float staminaTimerRegen = 0.0f;
    const float staminaDecrease = 2.0f;
    const float staminaIncrease = 4.0f;
    const float staminaTimeToRegen = 3.0f;
    
    Vector3 movimiento;
    [SerializeField] Camera cam;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        movimiento = Vector3.zero;

        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        leftHand = leftHandDevices[0];

        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        rigtHand = rightHandDevices[0];

        leftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystickIzq);
        rigtHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystickDer);
        
        rotacion = (.5f+Time.deltaTime) * joystickIzq.x;
        //transform.Translate(movimiento * speedRotation * Time.deltaTime);

        if (joystickDer.x != 0 || joystickDer.y != 0)
        {
            Vector3 direction = (cam.transform.forward * joystickDer.y + cam.transform.right* joystickDer.x).normalized;

            movimiento = direction * movementSpeed;
        }
        
        movimiento.y = rb.velocity.y;
        rb.velocity = movimiento;


        transform.Rotate(rotacion * Vector3.down, Space.World);


        leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool Y);
        leftHand.TryGetFeatureValue(CommonUsages.secondaryButton, out bool X);
        if (Y || X)
        {
            stamina = Mathf.Clamp(stamina - (staminaDecrease * Time.deltaTime), 0.0f, maxStamina);
            if(stamina > 0.0)
            {
                movementSpeed = 2.0f;
                Debug.Log("runing");
            }
            else
            {
                movementSpeed =  1.0f;
                Debug.Log("tired");
            }
        }else if (stamina < maxStamina)
        {
            movementSpeed = 1.0f;
            if (staminaTimerRegen >= staminaTimeToRegen)
            {
                stamina = Mathf.Clamp(stamina + (staminaIncrease * Time.deltaTime), 0.0f, maxStamina);
            }
            else
            {
                staminaTimerRegen += Time.deltaTime;
            }
        }

        rigtHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool A);
        rigtHand.TryGetFeatureValue(CommonUsages.secondaryButton, out bool B);

        if(A || B)
        {
            if (!flashLight && batery > 0.0f)
            {
                flashLight = true;
            }
            else
            {
                flashLight = false;
            }
        }

        if (flashLight)
        {
            batery = Mathf.Clamp(batery - (bateryDcrease* Time.deltaTime), 0.0f, maxBatery);
            flashLight = true;
        }
        else
        {
            flashLight = false;
        }

    }
}
