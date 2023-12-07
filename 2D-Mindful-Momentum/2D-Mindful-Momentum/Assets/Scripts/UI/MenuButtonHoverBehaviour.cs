using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButton :  MonoBehaviour, 
    IPointerEnterHandler, 
    IPointerExitHandler
{
     public float increasedSize = 1.2f; // Set the increased font size multiplier
        
        private TMP_Text _buttonText;
        private float _normalSize;
        private string _normalColor;
        private bool _isSelected = false;
        // Start is called before the first frame update
        void Start()
        {
            _buttonText = GetComponentInChildren<TMP_Text>();
            _normalSize = _buttonText.fontSize;
            _normalColor = _buttonText.color.ToString();
        }
        
        public void IncreaseFontSize()
        {
            _buttonText.fontSize *= increasedSize;
        }
        
        public void DecreaseFontSize()
        {
            _buttonText.fontSize = _normalSize;
        }
        
        public void ChangeColor()
        {
            _buttonText.color = new Color(1f, 1f, 1f, 1);
        }
        
        public void ResetColor()
        {
            _buttonText.color = new Color32(0xA6, 0xA6, 0xA6, 0xFF);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_buttonText != null)
            {
                if (!_isSelected)
                {
                    IncreaseFontSize();
                    ChangeColor();
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_buttonText != null)
            {
                if (!_isSelected)
                {
                    DecreaseFontSize();
                    ResetColor();
                }
            }
        }
}
