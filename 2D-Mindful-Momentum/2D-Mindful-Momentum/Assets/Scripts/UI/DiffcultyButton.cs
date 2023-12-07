using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiffcultyButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Set the difficulty
        DiffcultyButtonsManager.Instance.SetSelectButton(gameObject.GetComponent<Button>());
    }
}

