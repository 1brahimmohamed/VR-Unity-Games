using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DiffcultyButtonsManager : MonoBehaviour
{
    public static DiffcultyButtonsManager Instance; // Singleton instance
    public List<Button> buttons = new List<Button>();

    public Button defaultButton;
    private Button _selectedButton;
    
    public float increasedSize = 1.5f;
    private float _normalSize;
    
    
    void Start()
    {
        // Get the default button's font size
        _normalSize = defaultButton.GetComponentInChildren<TMP_Text>().fontSize;
        _selectedButton = defaultButton;
        UpdateTheButtons();
    }
    
    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void IncreaseFontSize(Button button)
    {
        button.GetComponentInChildren<TMP_Text>().fontSize *= increasedSize;
    }
        
    public void DecreaseFontSize(Button button)
    {
        button.GetComponentInChildren<TMP_Text>().fontSize = _normalSize;
    }
    
    public void ChangeColor(Button button,Color color)
    {
        button.GetComponentInChildren<TMP_Text>().color = color;
    }
    
    public void SetSelectButton(Button button)
    {
        // If the button is not the selected button then set it as the selected button and update the buttons
        if (button != _selectedButton)
        {
            _selectedButton = button;
            UIManager.Instance.SetDifficulty(button.name);
            UpdateTheButtons();
        }
    }

    private void UpdateTheButtons()
    {
        // Loop through all the buttons and change the font size and color of the selected button
        foreach (var button in buttons)
        {
            if (button == _selectedButton)
            { 
                Debug.Log("Selected button found"); 
                IncreaseFontSize(button); 
                ChangeColor(button, Color.white);
            }
            else
            {
                DecreaseFontSize(button);
                ChangeColor(button, new Color32(0xA6, 0xA6, 0xA6, 0xFF));
            }
        }
    }
}
