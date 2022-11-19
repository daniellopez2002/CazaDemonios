using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerController : MonoBehaviour
{
    public float speedRotation = 5.0f;
    public float movementSpeed = 1.0f;
    [SerializeField] float stepHeigth = 0.3f;
    [SerializeField] float stepSmooth= 0.1f;
    float rotacion;

    InputDevice rigtHand;
    InputDevice leftHand;

    bool flashLight;
    public float batery = 100.0f;
    float maxBatery = 100.0f;
    float bateryDcrease = 5.0f;
    float bateryIncrease = 15.0f;

    public bool cross, protect;
    public float crossEnergy = 15.0f;


    public float stamina = 100.0f;
    float maxStamina = 100.0f;
    float staminaTimerRegen = 0.0f;
    const float staminaDecrease = 2.0f;
    const float staminaIncrease = 4.0f;
    const float staminaTimeToRegen = 3.0f;
    
    Vector3 movimiento;
    [SerializeField] Camera cam;
    [SerializeField] Light light_1;
    [SerializeField] GameObject light_2;
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    


    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeigth, stepRayUpper.transform.position.z);
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
        
        rotacion = (.5f+Time.deltaTime) * -joystickIzq.x;
        //transform.Translate(movimiento * speedRotation * Time.deltaTime);

        if (joystickDer.x != 0 || joystickDer.y != 0)
        {
            Vector3 direction = (cam.transform.forward * joystickDer.y + cam.transform.right* joystickDer.x).normalized;

            movimiento = direction;
        }
        
        movimiento.y += -9.57f * Time.deltaTime;

        cc.Move(movimiento * Time.deltaTime * movementSpeed);


        transform.Rotate(rotacion * Vector3.down, Space.World);


        leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool X);
        leftHand.TryGetFeatureValue(CommonUsages.secondaryButton, out bool Y);
        leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftTrigger);
        if (X)
        {
            stamina = Mathf.Clamp(stamina - (staminaDecrease * Time.deltaTime), 0.0f, maxStamina);
            if(stamina > 0.0)
            {
                movementSpeed = 2.0f;
                
            }
            else
            {
                movementSpeed =  1.0f;
                
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

        if (Y)
        {
            cross = !cross;
        }

        if (cross && crossEnergy > 0.0f)
        {
            protect = true;
            light_2.SetActive(true);
            crossEnergy = Mathf.Clamp(crossEnergy-(1f * Time.deltaTime), 0.0f, 15.0f);
        }
        else if(!cross || crossEnergy <= 0.0f)
        {
            cross = false;
            protect = false;
            light_2.SetActive(false);
            crossEnergy = Mathf.Clamp(crossEnergy + (3f * Time.deltaTime), 0.0f, 15.0f);
        }

        rigtHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool A);
        rigtHand.TryGetFeatureValue(CommonUsages.secondaryButton, out bool B); //no se usa
        rigtHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger);

        if (A)
        {
            flashLight = !flashLight; 
        }



        if (flashLight && batery > 0.0f && !cross)
        {
            light_1.enabled = true;
            batery = Mathf.Clamp(batery - (bateryDcrease * Time.deltaTime), 0.0f, maxBatery);
        }
        else
        {
            light_1.enabled = false;
        }


        if (trigger)
        {
            flashLight = false;
            batery = Mathf.Clamp(batery + (0.5f * Time.deltaTime), 0.0f, maxBatery);
        }
    }

    
}
