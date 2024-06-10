using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    /* Private Variables */
    [SerializeField]
    private float maxMoveSpeed = 1f;
    private float moveSpeed = 0.5f;
    private float accelerometerMultiplier = 15f;
    private Rigidbody rb;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 initialAccel = Vector3.zero;
    private LineRenderer line;
    [SerializeField]
    private Vector3 lineRendererOffset = new Vector3(0, 0.75f, 0);
    private bool runningOnHandheldDevice = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            runningOnHandheldDevice = true;
            Debug.Log("handheld");
        }
            
        else
            runningOnHandheldDevice = false;

        initialAccel = Input.acceleration;

        EventManager.onMoveSpeedChanged.AddListener(OnMoveSpeedChanged);
        StartCoroutine(ShowInitMoveSpeedInOptions());
    }

    private IEnumerator ShowInitMoveSpeedInOptions()
    {
        yield return new WaitForSeconds(1f);

        EventManager.initialMoveSpeedVal.Invoke(remapSliderValue(moveSpeed, 0f, maxMoveSpeed, 0f, 100f));
    }

    // Update is called once per frame
    void Update()
    {
        if (runningOnHandheldDevice)
        {
            Vector3 currentAccel = Input.acceleration;
            moveDir = currentAccel - initialAccel;
            moveDir = new Vector3(moveDir.x, 0f, -moveDir.z);

            moveDir.Normalize();

            rb.AddForce(moveDir * moveSpeed * accelerometerMultiplier);

            // Debug Line (enable LineRenderer in Start function first)
            // line.SetPosition(0, transform.position + lineRendererOffset);
            // line.SetPosition(1, transform.position + lineRendererOffset + moveDir * 2);
        }
        else
        {
            moveDir.x = moveSpeed * Input.GetAxis("Horizontal");
            moveDir.z = moveSpeed * Input.GetAxis("Vertical");

            
            rb.AddForce(moveDir);

            // Debug Line (enable LineRenderer in Start function first)
            line.SetPosition(0, transform.position + lineRendererOffset);
            line.SetPosition(1, transform.position + lineRendererOffset + moveDir * 2);
        }
    }

    void OnMoveSpeedChanged(float val)
    {
        float remappedValue = remapSliderValue(val, 0f, 100f, 0f, maxMoveSpeed);
        moveSpeed = remappedValue;
    }

    float remapSliderValue(float val, float from1, float to1, float from2, float to2)
    {
        return (val - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
