using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;

namespace Interactions
{
    public class GameEnd: InteractionBase
    {
        public void DataInit()
        {

        }

        public override void AStart()
        {
            base.AStart();

            BackgroundManager.singleton.currentBackground.SetActive(false);
            DialogueBox.singleton.Show(true);
            DialogueBox.singleton.SetName("");
            DialogueTypeWriter.singleton.OutputText("游戏结束");
        }

        //考虑加入dialogue交互
    }
}
