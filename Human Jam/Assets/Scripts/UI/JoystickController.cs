using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class JoystickController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public Image Inner_Joystick;
    public Vector3 jsVec;   

    // For Dynamic resize
    private Vector2 default_size = new Vector2(515, 386);

    // Twerkable Value
    private float _maxClampBoundary_OuterCircle = 50; // Clamp Boundary->OuterCircle

    // Setup
    private Vector3 _startPos; // Default start position of the image

    void Start()
    {
        _startPos = Inner_Joystick.transform.position;

        // Dynamic resize max boundary clamp
        _maxClampBoundary_OuterCircle = _maxClampBoundary_OuterCircle * (Screen.width/default_size.x);
    }


    public void OnDrag(PointerEventData data)
    {
        // Calculate [Change in dist]-> Value during dragging
        Vector3 newPos = Vector3.zero;
        newPos.x = data.position.x - _startPos.x;
        newPos.y = data.position.y - _startPos.y;

        // Apply [change in dist]-> Value to image->JSBtn
        //-Translate
        //-Clamp
        Inner_Joystick.transform.position = Vector3.ClampMagnitude(newPos, _maxClampBoundary_OuterCircle) + _startPos;
        Inner_Joystick.transform.position = new Vector3(Inner_Joystick.transform.position.x, Inner_Joystick.transform.position.y, 0);

        // Event: Convert it to 0.0f-1.0f
        UpdateVirtualAxes(Inner_Joystick.transform.position);
    }

    public void OnPointerDown(PointerEventData data)
    {
        // Calculate [Change in dist]-> Value during dragging
        Vector3 newPos = Vector3.zero;
        newPos.x = data.position.x - _startPos.x;
        newPos.y = data.position.y - _startPos.y;

        // Apply [change in dist]-> Value to image->JSBtn
        //-Translate
        //-Clamp
        Inner_Joystick.transform.position = Vector3.ClampMagnitude(newPos, _maxClampBoundary_OuterCircle) + _startPos;
        Inner_Joystick.transform.position = new Vector3(Inner_Joystick.transform.position.x, Inner_Joystick.transform.position.y, 0);

        // Event: Convert it to 0.0f-1.0f
        UpdateVirtualAxes(Inner_Joystick.transform.position);
    }

    public void OnPointerUp(PointerEventData data)
    {
        // Resets the js btn image back to center
        Inner_Joystick.transform.position = _startPos;

        // Stop All Movement
        UpdateVirtualAxes(_startPos);
        jsVec = Vector3.zero;
    }

    // Convert to 0.0f - 1.0f
    void UpdateVirtualAxes(Vector3 value)
    {
        // Dist moved-> Value
        Vector3 delta = _startPos - value;

        // Convert it to 0.0f - 1.0f
        delta /= _maxClampBoundary_OuterCircle;

        // Store value to public variable
        jsVec = new Vector3(-delta.x, -delta.y, 0);

        /* Example
         * _startPos = 0%
         * _maxClampBoundary_OuterCircle = 100%
         * value = somewhere between 0%-100%, for this example we treat value = 70%
         * delta = 0% - 70%
         * delta /= 100% (-70%/100% = -0.7f) <- Answer
         */
    }
}
