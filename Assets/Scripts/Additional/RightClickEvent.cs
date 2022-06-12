using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickEvent : MonoBehaviour
{
    public void Click(BaseEventData bed)
    {
        PointerEventData ped = (PointerEventData)bed;
        Debug.Log("Button: " + ped.pointerId);
    }
}
