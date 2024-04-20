using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KenoButton : MonoBehaviour
{
    [SerializeField]
    private Button This_Button;
    [SerializeField]
    private TMP_Text This_Text;
    [SerializeField]
    private Image This_Image;
    [SerializeField]
    private KenoBehaviour KenoManager;
    [SerializeField]
    private Color _normalColor;
    [SerializeField]
    private Color _selectedColor;
    [SerializeField]
    private Color _resultColor;
    [SerializeField]
    private GameObject _winTick;

    internal bool isActive = false;

    private void Start()
    {
        if (KenoManager == null) 
        {
            KenoManager = GameObject.FindWithTag("GameController").GetComponent<KenoBehaviour>();
        }
        if (This_Button) This_Button.onClick.RemoveAllListeners();
        if (This_Button) This_Button.onClick.AddListener(OnKenoSelect);
    }

    internal void OnKenoSelect()
    {
        isActive = !isActive;
        if(isActive)
        {
            if (KenoManager.selectionCounter < 10)
            {
                if (This_Image) This_Image.color = _selectedColor;
                KenoManager.selectionCounter++;
                KenoManager.AddKeno(int.Parse(This_Text.text));
            }
            else
            {
                isActive = false;
            }
        }
        else
        {
            if (This_Image) This_Image.color = _normalColor;
            KenoManager.selectionCounter--;
            KenoManager.RemoveKeno(int.Parse(This_Text.text));
        }
    }

    internal void ResultColor()
    {
        if (This_Image) This_Image.color = _resultColor;
        if(isActive)
        {
            enableWinTick();
            KenoManager.ResultCounter++;
            KenoManager.ActivateWinning();
        }
    }

    internal void ResetButton()
    {
        isActive = false;
        if (This_Image) This_Image.color = _normalColor;
        if (_winTick) _winTick.SetActive(false);
    }

    private void enableWinTick()
    {
        if (_winTick) _winTick.SetActive(true);
    }
}
