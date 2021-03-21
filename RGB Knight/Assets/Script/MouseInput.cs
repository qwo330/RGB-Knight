using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [HideInInspector]
    public GameObject Selected = null;
    Rigidbody2D targetRB = null;
    Collider2D targetColl = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(worldPos, Vector2.zero);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.CompareTag("Respawn"))
                    return;

                GameObject origin = hit.collider.gameObject;
                Selected = CopyObject(origin);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (Selected == null)
                return;

            Vector2 wolrdPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Selected.transform.position = wolrdPos;

            if (Input.GetMouseButtonDown(1))
            {
                if (Selected != null)
                {
                    Selected.transform.Rotate(new Vector3(0, 0, -90f));
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Selected == null)
                return;

            targetRB.gravityScale = 1f;
            targetRB = null;

            targetColl.isTrigger = false;
            targetColl = null;

            GameManager.Instance.CopyedList.Add(Selected);
            Selected = null;
        }
    }

    public GameObject CopyObject(GameObject target)
    {
        GameObject copyed = Instantiate(target);
        copyed.tag = "Respawn";

        targetColl = copyed.GetComponent<Collider2D>();

        targetRB = copyed.AddComponent<Rigidbody2D>();
        targetRB.gravityScale = 0;
        //targetRB = copyed.GetComponent<Rigidbody2D>();

        return copyed;
    }
}
