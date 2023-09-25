using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Utilitys.StateMachine;

public class Explore : MonoBehaviour
{
    [SerializeField] List<Button> buttons;
    TMP_Text choiceText;

    [SerializeField] int exploreCount;
    [SerializeField] int partyCount = 1;
    [SerializeField] int newAreaPercentage = 0;

    [SerializeField] BattleEncounter battleEncounter;
    [SerializeField] BattleStarter battleStarter;

    private void OnEnable()
    {
        ChoiceButotnSetUp();
    }

    public void ChoiceButotnSetUp() //�I�����̐ݒ�
    {
        foreach(Button choice in buttons)
        {
            choiceText = choice.GetComponentInChildren<TMP_Text>();

            if (Random.Range(1, 101) <= newAreaPercentage)
            {
                //special event
                choiceText.text = "New Area";
            }
            else if (Random.Range(1, 101) <= (partyCount * 0.25) * 100)
            {
                //battle
                choiceText.text = "Battle";
            }
            else if(Random.Range(1, 101) <= ((1 - partyCount * 0.25) * 0.3) * 100)
            {
                //Cave
                choiceText.text = "Cave";
            }
            else if (Random.Range(1, 101) <= ((1 - partyCount * 0.25) * 0.3) * 100)
            {
                //forest
                choiceText.text = "Forest";
            }
            else
            {
                //Other Event
                choiceText.text = "Other";
            }

        }
    }

    public void ChoiceMovement(int num)//�I�����̍s��
    {
        
        choiceText = buttons[num].GetComponentInChildren<TMP_Text>();
        if (choiceText.text == "Battle")//�퓬�J�n
        {
            //StartCoroutine(battleStarter.StartBattleCo());
            GameManager.instance.StateMachine.Push(BattleState.i);

        }
        else if (choiceText.text == "Cave")// ���A�V�[�����[�h
        {
            SceneManager.LoadSceneAsync("Big Cave Dungeon");
            GameManager.instance.StateMachine.ChangeState(FreeRoamState.i);
        }
    }

    public void ExploreCount()
    {
        exploreCount++;
        if(exploreCount >= 51 && exploreCount < 101)
        {
            newAreaPercentage += 2;
        }
        else if(exploreCount == 10)//10�}�X�i��ŃN���A(��)
        {
            SceneManager.LoadSceneAsync("ClearScene");
        }

    }
}
