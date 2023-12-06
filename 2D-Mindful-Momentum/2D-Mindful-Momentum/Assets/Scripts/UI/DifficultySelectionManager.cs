// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UIElements;
// using Button = UnityEngine.UI.Button;
//
// public class DifficultySelectionManager : MonoBehaviour
// {
//     public static DifficultySelectionManager Instance; // Singleton instance
//
//     private List<MenuButton> _buttons = new List<MenuButton>();
//     
//     private void Awake()
//     {
//         // Singleton pattern to ensure only one instance exists
//         if (Instance == null)
//         {
//             Instance = this;
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }
//
//     public void RegisterButton(MenuButton button)
//     {
//         _buttons.Add(button);
//     }
//
//     public void HandleButtonClick(MenuButton clickedButton)
//     {
//         foreach (MenuButton button in _buttons)
//         {
//             if (button != clickedButton)
//             {
//                 button.SetSelected(false);
//                 button.DecreaseFontSize();
//                 button.ChangeColor(button.normalColor);
//             }
//             else
//             {
//                 button.SetSelected(true);
//                 button.IncreaseFontSize();
//                 button.ChangeColor(Color.white);
//             }
//         }
//     }
// }
