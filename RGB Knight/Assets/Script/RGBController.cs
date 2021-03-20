using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum 컬러를 세팅해두면 자동으로 레이어, 렌더러 컬러를 수정한다.
/// </summary>
public class RGBController : MonoBehaviour
{
    public bool IsRandom = true;
    public EColor _EColor;

    void Awake()
    {
        if (IsRandom)
        {
            _EColor = (EColor)Random.Range(0, 3);
        }

        SetColor(_EColor);
    }

    public void SetColor(EColor eColor)
    {
        _EColor = eColor;

        gameObject.layer = LayerMask.NameToLayer(_EColor.ToString());
        SpriteRenderer render = GetComponentInChildren<SpriteRenderer>();
        render.color = _EColor == EColor.Red ? Color.red
                    : _EColor == EColor.Green ? Color.green
                    : Color.blue;
    }
}
