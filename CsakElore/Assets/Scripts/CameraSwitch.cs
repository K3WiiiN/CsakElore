using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public bool fps;


    public Transform tpsController;
    public Transform fpsController;
    public Transform tpsCamera;
    public Transform fpsCamera;

    public static CameraSwitch instance;
    public static CameraSwitch GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (fps)
        {
            tpsController.gameObject.SetActive(false);
            tpsCamera.gameObject.SetActive(false);
            fpsController.transform.gameObject.SetActive(true);
            fpsCamera.gameObject.SetActive(true);
        }
        else
        {
            tpsController.gameObject.SetActive(true);
            tpsCamera.gameObject.SetActive(true);
            fpsController.gameObject.SetActive(false);
            fpsCamera.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (fps)
        {
            tpsController.transform.position = fpsController.transform.position;

        }
        else
        {
            fpsController.transform.position = tpsController.transform.position;
        }
    }

    public void SwitchToFps(Vector3 lookPosition)
    {
        fpsController.transform.position = tpsController.transform.position;
        fpsController.transform.rotation = tpsController.transform.rotation;
        

        fpsController.gameObject.SetActive(true);
        fpsCamera.transform.gameObject.SetActive(true);

        tpsController.gameObject.SetActive(false);
        tpsCamera.gameObject.SetActive(false);

        fps = true;
    }

    public void SwitchToTps(Vector3 lookPosition)
    {
        tpsController.transform.position = fpsController.transform.position;
        tpsCamera.transform.parent.position = tpsController.transform.position;

       
        tpsController.transform.rotation = fpsController.transform.rotation;
        tpsCamera.transform.rotation = tpsController.transform.rotation;

        tpsController.gameObject.SetActive(true);
        tpsCamera.gameObject.SetActive(true);
        fpsController.gameObject.SetActive(false);
        fpsCamera.transform.gameObject.SetActive(false);

        fps = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
