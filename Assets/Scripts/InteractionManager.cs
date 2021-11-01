using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Interactions;

public class InteractionManager : MonoBehaviour
{
    #region Field
    //Static
    public static InteractionManager singleton = null;

    //setting
    [SerializeField] TMP_Text nameTmpro = null;
    [SerializeField] SpriteRenderer backGround = null;
    [SerializeField] SpriteRenderer newBackGround = null;

    //runtime
    List<object> list;
    [NonSerialized]public object current;
    [NonSerialized] public int currentNum;
    bool backgroundFinish;
    bool newBackgroundFinish;
    #endregion


    #region Public Method

    public bool Next()
    {
        if (currentNum < list.Count - 1)
        {
            currentNum++;
            current = list[currentNum];
            ShowCurrent();
            return true;
        }
        return false;
    }

    #endregion

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        list = InteractionList.singleton.list;

        currentNum = 0;
        if (list.Count > currentNum)
        {
            current = list[currentNum];
        }
        ShowCurrent();
    }

    private void ShowCurrent()
    {
        var writer = DialogueTypeWriter.singleton;
        if (current is Idle)
        {
            var idle = current as Idle;
            idle.InteractionStart();
            DialogueBoxShowOrNot(false);
        }
        else if (current is Dialogue)
        {
            var dialogue = current as Dialogue;
            DialogueBoxShowOrNot(true);
            writer.OutputText(dialogue.text);
            if (dialogue.characterName != null)
            {
                nameTmpro.text = dialogue.characterName;
            }
            else
            {
                nameTmpro.text = "";
            }

        }
        else if (current is Character)
        {
            var character = current as Character;
            if (character.show == true)
            {
                character.character.SetActive(true);
                SpriteRenderer sr = character.character.GetComponent<SpriteRenderer>();
                sr.color = new Color(1, 1, 1, 0);
                sr.DOColor(new Color(1, 1, 1, 1), 0.5f);
            }
            else
            {
                character.character.SetActive(false);
            }
            Next();

        }
        else if (current is ChangeBackGround)
        {
            var cbg = current as ChangeBackGround;
            newBackGround.sprite = cbg.newBackGround;

            DialogueBoxShowOrNot(false);
            ;
            newBackGround.gameObject.SetActive(true);
            backgroundFinish = false;
            newBackgroundFinish = false;
            var color0 = new Color(1, 1, 1, 0);
            var color1 = new Color(1, 1, 1, 1);
            backGround.DOColor(color0, 0.8f).OnKill(() =>backgroundFinish = true);
            newBackGround.color = color1; //newBackGround.color = color0;
            newBackGround.DOColor(color1, 0.8f).OnKill(() => newBackgroundFinish = true);
            StartCoroutine(ChangeBackgroundFinish());
            ;
            IEnumerator ChangeBackgroundFinish()
            {
                while (!backgroundFinish || !newBackgroundFinish)
                {
                    yield return null;
                }
                backGround.sprite = newBackGround.sprite;
                backGround.color = new Color(1, 1, 1, 1);
                newBackGround.gameObject.SetActive(false);
                Next();
            }

        }
        else if (current is TimeChoice)
        {
            var tc = current as TimeChoice;
            DialogueBoxShowOrNot(true);
            tc.InteractionStart();
        }
        else if (current is Choice)
        {
            var choice = current as Choice;
            DialogueBoxShowOrNot(true);
            choice.InteractionStart();
        }
        else if (current is CustomInteraction)
        {
            var custom = current as CustomInteraction;
            custom.InteractionStart();
        }
        else if (current is GameEnd)
        {
            backGround.gameObject.SetActive(false);
            nameTmpro.text = "";
            DialogueBoxShowOrNot(true);
            writer.OutputText("游戏结束");
        }
    }

    public void DialogueBoxShowOrNot(bool show)
    {
        if (show)
        {
            if (DialogueBox.singleton.gameObject.activeSelf == false)
            {
                DialogueBox.singleton.gameObject.SetActive(true);
            }
        }
        else
        {
            if (DialogueBox.singleton.gameObject.activeSelf == true)
            {
                DialogueBox.singleton.gameObject.SetActive(false);
            }
        }
    }
}
