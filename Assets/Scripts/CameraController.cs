using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    public float horizontolRotateSpeed = 1.0f;
    public float verticalRotateSpeed = 80.0f;
    public float cameraSmoothTime = 0.1f;

    private IUserInput pi;
    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;

    private GameObject mainCamera;
    private Vector3 cameraRotateVelocity;

    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20f;
        ActorController actorController =  playerHandle.GetComponent<ActorController>();
        model = actorController.model;
        pi = actorController.pi;
        mainCamera = Camera.main.gameObject;

        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void FixedUpdate () 
    {
        Vector3 tempModelEuler = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontolRotateSpeed * Time.fixedDeltaTime);
        tempEulerX -= pi.Jup * verticalRotateSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
        cameraHandle.transform.localEulerAngles = new Vector3(
            tempEulerX, 0, 0);

        model.transform.eulerAngles = tempModelEuler;

        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position, ref cameraRotateVelocity, cameraSmoothTime);
        //mainCamera.transform.rotation = transform.rotation;
        mainCamera.transform.LookAt(cameraHandle.transform.position);
	}
}
