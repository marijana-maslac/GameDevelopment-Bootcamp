using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator Anim;

    public void OnPointerEnter(PointerEventData pointer)
    {
        Debug.Log("Enter");
        Anim.SetBool("Zoom", true);
    }

    public void OnPointerExit(PointerEventData pointer)
    {
        Debug.Log("Exit");
        Anim.SetBool("Zoom", false);
    }
}

