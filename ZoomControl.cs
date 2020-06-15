using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomControl : MonoBehaviour
{

    public float minZoom = 1f;
    public float maxZoom = 10f;
    float sensitivity = 2f;
    Vector3 cameraPosition;
    Vector3 mousePositionOnScreen;
    Vector3 mousePositionOnScreen1;
    Vector3 camPos1;
    Vector3 mouseOnWorld;
   Vector3 camDragBegin;
    Vector3 camDragNext;

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = Camera.main.transform.position;
        mousePositionOnScreen = new Vector3();
        mousePositionOnScreen1 = new Vector3();
        camPos1 = new Vector3();
        mouseOnWorld = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            mousePositionOnScreen = mousePositionOnScreen1;
            mousePositionOnScreen1 = Input.mousePosition;
            if (Vector3.Distance(mousePositionOnScreen, mousePositionOnScreen1) == 0)
            {
                float fov = Camera.main.orthographicSize;
                fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
                fov = Mathf.Clamp(fov, minZoom, maxZoom);
                Camera.main.orthographicSize = fov;
                Vector3 mouseOnWorld1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 posDiff = mouseOnWorld - mouseOnWorld1;
                Vector3 camPos = Camera.main.transform.position;
                Camera.main.transform.position = new Vector3(camPos.x + posDiff.x, camPos.y + posDiff.y, camPos.z);
            }
            else
            {
                mouseOnWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

         
         //start Pan or move objects at mouse position
      
         if (Input.GetMouseButtonDown(2))
        {
            camDragBegin = Input.mousePosition;
            camPos1 = Camera.main.transform.position;
        }
      // Pan or move objects at mouse position
      
        if (Input.GetMouseButton(2))
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                
                    camDragNext = Input.mousePosition;
                Vector3 screenDelta = camDragBegin - camDragNext;
                Vector2 screenSize = ScaleScreenToWorldSize(Camera.main.aspect,Camera.main.orthographicSize, Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, screenDelta.x, screenDelta.y);
               
                Vector3 camPosMove = new Vector3(camPos1.x + screenSize.x, camPos1.y + screenSize.y, camPos1.z);
                Camera.main.transform.position = camPosMove;
            }
        }
    }

    // convert screen coordinate to world coordinate
    Vector2 ScaleScreenToWorldSize(float camAspect,float camSize,float camScreenPixelWidth,float camScreenPixelHeight,float screenW,float screenH)
    {
        float cameraWidth = camAspect * camSize * 2f;
        float cameraHeght = camSize * 2f;
        float screenWorldW = screenW * (cameraWidth / camScreenPixelWidth);
        float screenWorldH=screenH*(cameraHeght/camScreenPixelHeight);
        
        return new Vector2(screenWorldW,screenWorldH);
            
    }


}
