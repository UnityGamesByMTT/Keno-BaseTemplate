using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KenoBehaviour : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField]
    private List<TMP_Text> Keno_Texts;
    [SerializeField]
    private List<GameObject> Keno_Objects;
    [SerializeField]
    private List<KenoButton> KenoButtonScripts;
    [SerializeField]
    private List<int> SelectedList;
    [SerializeField]
    private List<int> ResultList;

    [Header("Integers")]
    [SerializeField]
    private int MaxSelection;
    [SerializeField]
    internal int selectionCounter = 0;
    [SerializeField]
    internal int ResultCounter = 0;

    [Header("Transforms")]
    [SerializeField]
    private Transform Keno_Container;

    [Header("GameObjects")]
    [SerializeField]
    private GameObject KenoCell_Prefab;

    [Header("Scripts")]
    [SerializeField]
    private UIManager uiManager;

    void Start()
    {
        Application.ExternalCall("window.parent.postMessage", "OnEnter", "*");
        for (int i = 0; i < 80; i++) 
        {
            GameObject Kc = Instantiate(KenoCell_Prefab, Keno_Container);
            Keno_Objects.Add(Kc);
            Keno_Texts.Add(Kc.GetComponentInChildren<TMP_Text>());
            KenoButtonScripts.Add(Kc.GetComponentInChildren<KenoButton>());
            Kc.GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();
        }
    }

    private List<int> templist = new List<int>();

    internal void PickRandoms(int count)
    {
        selectionCounter = 0;
        SelectedList.Clear();
        SelectedList.TrimExcess();
        foreach (KenoButton kc in KenoButtonScripts)
        {
            kc.ResetButton();
        }

        templist.Clear();
        templist.TrimExcess();
        templist = GenerateRandomNumbers(count, 0, 79);

        for (int i = 0; i < templist.Count; i++)
        {
            KenoButtonScripts[templist[i]].OnKenoSelect();
        }
    }

    private static List<int> GenerateRandomNumbers(int count, int minValue, int maxValue)
    {
        List<int> possibleNumbers = new List<int>();
        List<int> chosenNumbers = new List<int>();

        for (int index = minValue; index < maxValue; index++)
            possibleNumbers.Add(index);

        while (chosenNumbers.Count < count)
        {
            int position = Random.Range(0, possibleNumbers.Count);
            chosenNumbers.Add(possibleNumbers[position]);
            possibleNumbers.RemoveAt(position);
        }
        return chosenNumbers;
    }

    internal void AddKeno(int value)
    {
        if (!uiManager.isReset)
        {
            SelectedList.Add(value);
        }

        if(selectionCounter >= 2)
        {
            uiManager.CheckPlayButton(true);
        }
        else
        {
            uiManager.CheckPlayButton(false);
        }
        uiManager.UpdateSelectedText(selectionCounter);
    }

    internal void RemoveKeno(int value)
    {
        SelectedList.Remove(value);
        if (selectionCounter >= 2)
        {
            uiManager.CheckPlayButton(true);
        }
        else
        {
            uiManager.CheckPlayButton(false);
        }
        uiManager.UpdateSelectedText(selectionCounter);
    }

    internal void PlayDummyGame()
    {
        ResultList.Clear();
        ResultList.TrimExcess();
        ResultList = GenerateRandomNumbers(20, 0, 79);
        StartCoroutine(PlayGameRoutine());
    }

    private IEnumerator PlayGameRoutine()
    {
        for (int i = 0; i < ResultList.Count; i++)
        {
            KenoButtonScripts[ResultList[i]].ResultColor();
            yield return new WaitForSeconds(0.1f);
        }
        uiManager.EnableReset();
    }

    internal void ActivateWinning()
    {
        uiManager.CheckWinnings(ResultCounter);
    }

    internal void ResetButtons()
    {
        for (int i = 0; i < KenoButtonScripts.Count; i++)
        {
            KenoButtonScripts[i].ResetButton();
        }

        ResultCounter = 0;
        selectionCounter = 0;

        for (int i = 0; i < SelectedList.Count; i++)
        {
            KenoButtonScripts[SelectedList[i]].OnKenoSelect();
        }
    }

    internal void CleanPage()
    {
        for (int i = 0; i < KenoButtonScripts.Count; i++)
        {
            KenoButtonScripts[i].ResetButton();
        }

        SelectedList.Clear();
        SelectedList.TrimExcess();
        ResultCounter = 0;
        selectionCounter = 0;
    }
}
