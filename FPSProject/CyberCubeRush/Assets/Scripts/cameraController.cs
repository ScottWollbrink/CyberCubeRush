using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] int sensitivityFactor;
    [SerializeField] int lockVertMin, lockVertMax;
    [SerializeField] bool invertY = false;

    private float settingsSensitivity = .5f;

    float rotX;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //get input

        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivityFactor * settingsSensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityFactor * settingsSensitivity;

        //invert Y-axis
        if (invertY)
            rotX += mouseY;
        else
            rotX -= mouseY;

        // clamp x axis rotation
        rotX = Mathf.Clamp(rotX, lockVertMin, lockVertMax);

        //rotate the cam on the x-axis
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);

        //rotate the player on the y-axis
        transform.parent.Rotate(Vector3.up * mouseX);
    }

    public void SetSettingsSense(float sensitivity)
    {
        settingsSensitivity = sensitivity * 100f;
    }
    public void ToggleInvert()
    {
        invertY = !invertY;
    }
    public void SetInvert(bool val)
    {
        invertY = val;
    }
}
