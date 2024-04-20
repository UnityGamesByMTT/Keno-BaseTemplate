using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button Random_Button;
    [SerializeField]
    private Button Plus_Button;
    [SerializeField]
    private Button Minus_Button;
    [SerializeField]
    private Button Play_Button;
    [SerializeField]
    private Button StakePlus_Button;
    [SerializeField]
    private Button StakeMinus_Button;
    [SerializeField]
    private Button Reset_Button;
    [SerializeField]
    private Button Delete_Button;
    [SerializeField]

    [Header("Texts")]
    private TMP_Text Count_Text;
    [SerializeField]
    private TMP_Text SelectedNO_Text;
    [SerializeField]
    private TMP_Text Stake_Text;
    [SerializeField]
    private TMP_Text Win_Text;

    [Header("Lists")]
    [SerializeField]
    private List<TMP_Text> Amount_Text;
    [SerializeField]
    private List<GameObject> Win_Objects;

    [Header("GameObjects")]
    [SerializeField]
    private GameObject Reset_Object;

    [Header("Scripts")]
    [SerializeField]
    private KenoBehaviour KenoManager;

    private int RandomCount = 2;
    private int stake = 5;
    private int winning = 0;
    internal bool isReset = false;

    void Start()
    {
        if (Minus_Button) Minus_Button.onClick.RemoveAllListeners();
        if (Minus_Button) Minus_Button.onClick.AddListener(delegate { ChangeCount(false); });

        if (Plus_Button) Plus_Button.onClick.RemoveAllListeners();
        if (Plus_Button) Plus_Button.onClick.AddListener(delegate { ChangeCount(true); });

        if (Random_Button) Random_Button.onClick.RemoveAllListeners();
        if (Random_Button) Random_Button.onClick.AddListener(PickRandomIndices);

        if (Play_Button) Play_Button.onClick.RemoveAllListeners();
        if (Play_Button) Play_Button.onClick.AddListener(DummyPlay);

        if (Play_Button) Play_Button.interactable = false;

        if (SelectedNO_Text) SelectedNO_Text.text = "0";

        if (StakePlus_Button) StakePlus_Button.onClick.RemoveAllListeners();
        if (StakePlus_Button) StakePlus_Button.onClick.AddListener(delegate { ChangeStake(true); });

        if (StakeMinus_Button) StakeMinus_Button.onClick.RemoveAllListeners();
        if (StakeMinus_Button) StakeMinus_Button.onClick.AddListener(delegate { ChangeStake(false); });

        if (Reset_Button) Reset_Button.onClick.RemoveAllListeners();
        if (Reset_Button) Reset_Button.onClick.AddListener(ResetGame);

        if (Delete_Button) Delete_Button.onClick.RemoveAllListeners();
        if (Delete_Button) Delete_Button.onClick.AddListener(CleanButtons);

        RandomCount = 2;
        stake = 5;
        winning = 0;
        if (Count_Text) Count_Text.text = RandomCount.ToString();
        if (Stake_Text) Stake_Text.text = stake.ToString();
        if (Win_Text) Win_Text.text = winning.ToString();
    }

    private void DummyPlay()
    {
        if(isReset)
        {
            ResetGame();
        }
        isReset = true;
        if (Random_Button) Random_Button.interactable = false;
        if (Delete_Button) Delete_Button.interactable = false;
        if (Play_Button) Play_Button.interactable = false;
        KenoManager.PlayDummyGame();
    }

    private void ChangeCount(bool type)
    {
        if (type)
        {
            if (RandomCount < 10)
            {
                RandomCount++;
                if (Count_Text) Count_Text.text = RandomCount.ToString();
            }
        }
        else
        {
            if (RandomCount > 2)
            {
                RandomCount--;
                if (Count_Text) Count_Text.text = RandomCount.ToString();
            }
        }
    }

    private void ChangeStake(bool type)
    {
        if (type)
        {
            if (stake < 20)
            {
                stake += 5;
                if (Stake_Text) Stake_Text.text = stake.ToString();
            }
        }
        else
        {
            if (stake > 5)
            {
                stake -= 5;
                if (Stake_Text) Stake_Text.text = stake.ToString();
            }
        }
    }

    private void PickRandomIndices()
    {
        if(isReset)
        {
            ResetGame();
        }
        KenoManager.PickRandoms(RandomCount);
    }

    internal void CheckPlayButton(bool isActive)
    {
        if (Play_Button) Play_Button.interactable = isActive;
    }

    internal void UpdateSelectedText(int count)
    {
        if (SelectedNO_Text) SelectedNO_Text.text = count.ToString();
        BetAmountUpdate(count);
    }

    internal void CheckWinnings(int count)
    {
        if(count >= 2)
        {
            if (Win_Objects[count - 2]) Win_Objects[count - 2].SetActive(true);
            winning += int.Parse(Amount_Text[count - 2].text);
        }
        else
        {
            for (int i = 0; i < Win_Objects.Count; i++)
            {
                if (Win_Objects[i]) Win_Objects[i].SetActive(false);
            }
            winning = 0;
        }
        if (Win_Text) Win_Text.text = winning.ToString();
    }

    internal void BetAmountUpdate(int count)
    {

        if (count >= 2)
        {
            for (int i = 2; i <= count; i++)
            {
                switch (i)
                {
                    case 2:
                        if (Amount_Text[0]) Amount_Text[0].text = (stake * i).ToString();
                        break;
                    case 3:
                        if (Amount_Text[1]) Amount_Text[1].text = (stake * i).ToString();
                        break;
                    case 4:
                        if (Amount_Text[2]) Amount_Text[2].text = (stake * i).ToString();
                        break;
                    case 5:
                        if (Amount_Text[3]) Amount_Text[3].text = (stake * i).ToString();
                        break;
                    case 6:
                        if (Amount_Text[4]) Amount_Text[4].text = (stake * i).ToString();
                        break;
                    case 7:
                        if (Amount_Text[5]) Amount_Text[5].text = (stake * i).ToString();
                        break;
                    case 8:
                        if (Amount_Text[6]) Amount_Text[6].text = (stake * i).ToString();
                        break;
                    case 9:
                        if (Amount_Text[7]) Amount_Text[7].text = (stake * i).ToString();
                        break;
                    case 10:
                        if (Amount_Text[8]) Amount_Text[8].text = (stake * i).ToString();
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < Amount_Text.Count; i++)
            {
                if (Amount_Text[i]) Amount_Text[i].text = string.Empty;
            }
        }
    }

    internal void EnableReset()
    {
        if (Reset_Object) Reset_Object.SetActive(true);
        if (Play_Button) Play_Button.interactable = true;
        if (Delete_Button) Delete_Button.interactable = true;
        if (Random_Button) Random_Button.interactable = true;
    }

    private void ResetGame()
    {
        KenoManager.ResetButtons();
        if (Reset_Object) Reset_Object.SetActive(false);
        isReset = false;
        CheckWinnings(0);
    }

    private void CleanButtons()
    {
        BetAmountUpdate(0);
        UpdateSelectedText(0);
        CheckWinnings(0);
        KenoManager.CleanPage();
    }

}
