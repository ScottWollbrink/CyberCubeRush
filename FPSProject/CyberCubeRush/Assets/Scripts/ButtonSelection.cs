using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    private int selectedButton = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Decrement(); 
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Increment();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Decrement();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Increment();
        }

        if (selectedButton >= 0)
        {
            buttons[selectedButton].Select();           
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttons[selectedButton].onClick.Invoke();
        }
    }

    private void Increment()
    {
        if (selectedButton >= buttons.Length - 1)
            selectedButton = 0;
        else        
            selectedButton++;        
    }
    private void Decrement()
    {
        if (selectedButton <= 0)        
            selectedButton = buttons.Length - 1;        
        else 
            selectedButton--;
    }
}
