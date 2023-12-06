using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonBehaviourScript : 
    MonoBehaviour, 
    IPointerEnterHandler, 
    IPointerExitHandler
{
    public float increasedSize = 1.2f; // Set the increased font size multiplier
    
    private TMP_Text _buttonText;
    private float _normalSize;
    
    // Start is called before the first frame update
    void Start()
    {
        _buttonText = GetComponentInChildren<TMP_Text>();
        _normalSize = _buttonText.fontSize;
    }
    
    void IncreaseFontSize()
    {
        _buttonText.fontSize *= increasedSize;
    }
    
    void DecreaseFontSize()
    {
        _buttonText.fontSize = _normalSize;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_buttonText != null)
        {
            IncreaseFontSize();
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_buttonText != null )
        {
            DecreaseFontSize();
        }
    }
}
