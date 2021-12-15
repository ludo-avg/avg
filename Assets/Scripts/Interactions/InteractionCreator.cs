using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
namespace Interactions
{
    [ExecuteInEditMode]
    public class InteractionCreator : MonoBehaviour
    {
        [SerializeField] private GameObject idle = null;
        [SerializeField] private GameObject gameEnd = null;
        [SerializeField] private GameObject dialogue = null;
        [SerializeField] private GameObject character = null;
        [SerializeField] private GameObject changeBackground = null;
        [SerializeField] private GameObject choice = null;
        [SerializeField] private GameObject timeChoice = null;
        [SerializeField] private GameObject customInteraction = null;
        [SerializeField] private GameObject function = null;
        public void Awake()
        {
            idle_s = idle;
            gameEnd_s = gameEnd;
            dialogue_s = dialogue;
            character_s = character;
            changeBackground_s = changeBackground;
            choice_s = choice;
            timeChoice_s = timeChoice;
            customInteraction_s = customInteraction;
            function_s = function;
        }
        public void Update()
        {
            idle_s = idle;
            gameEnd_s = gameEnd;
            dialogue_s = dialogue;
            character_s = character;
            changeBackground_s = changeBackground;
            choice_s = choice;
            timeChoice_s = timeChoice;
            customInteraction_s = customInteraction;
            function_s = function;
        }



        static GameObject idle_s;
        static GameObject gameEnd_s;
        static GameObject dialogue_s;
        static GameObject character_s;
        static GameObject changeBackground_s;
        static GameObject choice_s;
        static GameObject timeChoice_s;
        static GameObject customInteraction_s;
        static GameObject function_s;

        public class Idle
        {
            public static GameObject Create(float time)
            {
                GameObject obj = Instantiate(idle_s);
                Interactions.Idle result = obj.GetComponent<Interactions.Idle>();
                result.DataInit(time);

                obj.SetActive(false);
                return obj;
            }
        }

        public class GameEnd
        {
            public static GameObject Create()
            {
                GameObject obj = Instantiate(gameEnd_s);
                Interactions.GameEnd result = obj.GetComponent<Interactions.GameEnd>();
                result.DataInit();

                obj.SetActive(false);
                return obj;
            }
        }


        public class Dialogue
        {
            public static GameObject Create(string text)
            {
                GameObject obj = Instantiate(dialogue_s);
                Interactions.Dialogue result = obj.GetComponent<Interactions.Dialogue>();
                result.DataInit(text);

                obj.SetActive(false);
                return obj;
            }

            public static GameObject Create(string text, string name)
            {
                GameObject obj = Instantiate(dialogue_s);
                Interactions.Dialogue result = obj.GetComponent<Interactions.Dialogue>();
                result.DataInit(text, name);

                obj.SetActive(false);
                return obj;
            }

            public static GameObject Create(string text, string name = null, AudioClip audioClip = null)
            {
                GameObject obj = Instantiate(dialogue_s);
                Interactions.Dialogue result = obj.GetComponent<Interactions.Dialogue>();
                result.DataInit(text, name, audioClip);

                obj.SetActive(false);
                return obj;
            }

            public static GameObject Create(string text, string name = null, string audioClipFileName = null)
            {
                GameObject obj = Instantiate(dialogue_s);
                Interactions.Dialogue result = obj.GetComponent<Interactions.Dialogue>();
                result.DataInit(text, name, audioClipFileName);

                obj.SetActive(false);
                return obj;
            }
        }

        public class Character
        {
            public static GameObject Create(GameObject character, bool show)
            {
                GameObject obj = Instantiate(character_s);
                Interactions.Character result = obj.GetComponent<Interactions.Character>();
                result.DataInit(character, show);

                obj.SetActive(false);
                return obj;
            }
        }

        public class ChangeBackground
        {
            public static GameObject Create(GameObject background, Interactions.ChangeBackground.InType inType = Interactions.ChangeBackground.InType.AutoDecide, bool instantChange = false)
            {
                GameObject obj = Instantiate(changeBackground_s);
                Interactions.ChangeBackground result = obj.GetComponent<Interactions.ChangeBackground>();
                result.DataInit(background, inType, instantChange);

                obj.SetActive(false);
                return obj;
            }
        }

        public class Choice
        {
            public static GameObject Create(string text)
            {
                GameObject obj = Instantiate(choice_s);
                Interactions.Choice result = obj.GetComponent<Interactions.Choice>();
                result.DataInit(text);

                obj.SetActive(false);
                return obj;
            }
            public static GameObject Copy(GameObject choice)
            {
                GameObject obj = Instantiate(choice);

                obj.SetActive(false);
                return obj;
            }

        }

        public class TimeChoice
        {
            public static GameObject Create(string text, float time)
            {
                GameObject obj = Instantiate(timeChoice_s);
                Interactions.TimeChoice result = obj.GetComponent<Interactions.TimeChoice>();
                result.DataInit(text, time);

                obj.SetActive(false);
                return obj;
            }

            public static GameObject Copy(GameObject timeChoice)
            {
                GameObject obj = Instantiate(timeChoice);

                obj.SetActive(false);
                return obj;
            }
        }

        public class CustomInteraction
        {
            public static GameObject Create()
            {
                GameObject obj = Instantiate(customInteraction_s);
                Interactions.Custom result = obj.GetComponent<Interactions.Custom>();
                result.DataInit();

                obj.SetActive(false);
                return obj;
            }

            public static GameObject Copy(GameObject custom)
            {
                GameObject obj = Instantiate(custom);

                obj.SetActive(false);
                return obj;
            }
        }

        public class Function
        {
            public static GameObject Create()
            {
                GameObject obj = Instantiate(function_s);
                Interactions.Function result = obj.GetComponent<Interactions.Function>();
                result.DataInit();

                obj.SetActive(false);
                return obj;
            }
        }

    }
}
#endif