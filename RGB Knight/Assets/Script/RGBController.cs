using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum 컬러를 세팅해두면 자동으로 레이어, 렌더러 컬러를 수정한다.
/// </summary>
public class RGBController : MonoBehaviour
{
    public EColor _EColor;

    void Awake()
    {
        SetColor(_EColor);
    }

    public void SetColor(EColor eColor)
    {
        _EColor = eColor;

        gameObject.layer = LayerMask.NameToLayer(_EColor.ToString());
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.color = _EColor == EColor.Red ? Color.red
                    : _EColor == EColor.Green ? Color.green
                    : Color.blue;
    }
}
