using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float MoveSpeed = 5f;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        float mouseY = Input.mousePosition.y;
        if(mouseY > 1050)
        {
            //cam.transform.position = Translate(Vector3.up * MoveSpeed * Time.deltaTime);
            Vector3 curPos = cam.transform.position;
            Vector3 newPos = cam.transform.position + (Vector3.up * MoveSpeed * Time.deltaTime);
            cam.transform.position = newPos;
        }
        else if(mouseY < 30)
        {
            Vector3 curPos = cam.transform.position;
            Vector3 newPos = cam.transform.position + (Vector3.down * MoveSpeed * Time.deltaTime);

            float y = Mathf.Max(4, newPos.y);
            newPos.y = y;

            cam.transform.position = newPos;
        }


        //Debug.Log("mousePos  : " + Input.mousePosition);


        //Vector2 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("worldPos  : " + worldPos);


        //Ray2D ray = new Ray2D(worldPos, Vector2.zero);

        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        //if (hit.collider != null)
        //{
        //    if (hit.collider.name.Equals("CamUP"))
        //    {
        //        Debug.Log("camup");
        //        //cam.transform.position = Translate(Vector3.up * MoveSpeed * Time.deltaTime);
        //        Vector3 curPos = cam.transform.position;
        //        Vector3 newPos = cam.transform.position + (Vector3.up * MoveSpeed * Time.deltaTime);
        //        cam.transform.position = newPos;
        //    }
        //    else if (hit.collider.name.Equals("CamDown"))
        //    {
        //        Vector3 curPos = cam.transform.position;
        //        Vector3 newPos = cam.transform.position + (Vector3.up * MoveSpeed * Time.deltaTime);

        //        float y = Mathf.Min(4, newPos.y);
        //        newPos.y = y;

        //        cam.transform.position = newPos;
        //    }
        //}
    }
}
