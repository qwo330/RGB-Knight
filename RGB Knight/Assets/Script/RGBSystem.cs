using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBSystem : MonoBehaviour
{
    Vector2 mouseStart, mouseEnd;
    Actor actor;
    SpriteRenderer render;
    PlatformEffector2D plat;

    void Awake()
    {
        actor = GetComponent<Actor>();
        render = actor.GetComponent<SpriteRenderer>();
        plat = actor.GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mouseStart = Input.mousePosition;

            // todo : 컬러 선택 UI 표시
        }
        else if (Input.GetMouseButton(1))
        {
            ChangeTimeScale(0.2f);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            mouseEnd = Input.mousePosition;
            ChangeTimeScale(1f);

            // 마우스 일정 이상 움직여야 컬러 선택한 것으로 판정
            if (Vector2.Distance(mouseStart, mouseEnd) > 100f)
            {
                Vector2 mouseDir = (mouseEnd - mouseStart).normalized;
                ChangePlayerColor(mouseDir);
            }
        }
    }

    // 마우스 방향 벡터를 통해 각도로 컬러 판정
    public void ChangePlayerColor(Vector2 mouseDir)
    {
        float angle = Vector2.SignedAngle(Vector2.up, mouseDir);
        int layerIndex = 0;

        if (-120 <= angle && angle < 0)
        {
            // red
            layerIndex = LayerMask.NameToLayer("Red");
            render.color = Color.red;
        }
        else if (0 <= angle && angle < 120)
        {
            // blue
            layerIndex = LayerMask.NameToLayer("Blue");
            render.color = Color.blue;
        }
        else
        {
            // green
            layerIndex = LayerMask.NameToLayer("Green");
            render.color = Color.green;
        }


        actor.gameObject.layer = layerIndex;
        //plat.colliderMask = 1 << layerIndex; 
    }

    public void ChangeTimeScale(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // FixedUpdate()를 초당 50회(1/50 = 0.02f) 호출
    }

    public IEnumerator WaitRealTime(float seconds)
    {
        float eTime = Time.realtimeSinceStartup + seconds;
        while (Time.realtimeSinceStartup < eTime)
        {
            yield return null;
        }
    }
}
