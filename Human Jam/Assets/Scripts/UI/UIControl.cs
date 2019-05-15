using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum UIControlType
{
    LEFT,
    RIGHT,
    TOTAL
}

public class UIControl : MonoBehaviour, IPointerDownHandler
{
    public UIControlType controlType;

    [HideInInspector]
    public PlaceHolder_Player player_cs;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player_cs)
        {
            if(controlType == UIControlType.LEFT)
                player_cs.MoveLeft();
            else if (controlType == UIControlType.RIGHT)
                player_cs.MoveRight();
        }
    }
}
