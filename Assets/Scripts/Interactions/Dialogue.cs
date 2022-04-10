using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
//Editor
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

namespace Interactions
{
    public class Dialogue: InteractionBase
    {
        #region TypeDefine
        enum State
        {
            No,
            Playing,
            Played,
            End
        }

        #endregion

        public string text;
        public string characterName;
        public AudioClip audioClip;
        public bool autoEnd = false; //这个，是新加的，还没被DataInit处理。

        //runtime
        State state;
        //cached
        DialogueTypeWriter writer;
        DialogueBox box;
        Voice voice;


#if UNITY_EDITOR

        public void DataInit(string text)
        {
            this.text = text;
            this.characterName = null;
            this.audioClip = null;
        }

        public void DataInit(string text, string characterName)
        {
            this.text = text;
            this.characterName = characterName;
            this.audioClip = null;
        }

        /// <summary>
        /// 在编辑中被调用。目前是InteractinList脚本。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="characterName"></param>
        public void DataInit (string text, string characterName = null, AudioClip audioClip = null)
        {
            this.text = text;
            this.characterName = characterName;
            this.audioClip = audioClip;
        }

        public void DataInit(string text, string characterName = null, string audioClipFileName = null)
        {
            this.text = text;
            this.characterName = characterName;

            {
                string voiceFolder = "Assets/Resources/1  Sound_Voice";
                string[] guids = AssetDatabase.FindAssets(audioClipFileName, new string[] { voiceFolder });
                string path = null;
                {
                    if (guids.Length == 0)
                    {
                        path = null;
                    }
                    else
                    {
                        foreach (var guid in guids)
                        {

                            string guidPath = AssetDatabase.GUIDToAssetPath(guid);
                            string guidName = System.IO.Path.GetFileNameWithoutExtension(guidPath);
                            if (guidName == audioClipFileName)
                            {
                                path = guidPath;
                                break;
                            }
                        }
                    }
                }
                if (path == null)
                {
                    Debug.Log($"根据audioClipFileName，找不到文件。Info:({GetInfo()})");
                }
                var voice = AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));
                if (voice is AudioClip)
                {
                    audioClip = voice as AudioClip;
                }
                else
                {
                    Debug.LogError($"根据audioClipFileName找到的文件，不是AudioClip。文件名：{path}");
                }
            }
            
        }
#endif

        public override void AStart()
        {
            writer = DialogueTypeWriter.singleton;
            box = DialogueBox.singleton;
            voice = Voice.singleton;

            base.AStart();

            box.Show(true);
            if (characterName != null)
            {
                box.SetName(characterName);
            }
            else
            {
                box.SetName("");
            }

            writer.OutputText(text);
            voice.StopVoice();
            if (audioClip != null)
            {
                voice.PlayVoice(audioClip);
            }
            state = State.Playing;
        }

        public override void AInteract()
        {
            var voice = Voice.singleton;

            if (state == State.Playing)
            {
                if (writer.state == DialogueTypeWriter.TypewriterState.Completed)
                {
                    state = State.Played;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                var writer = DialogueTypeWriter.singleton;

                if (state == State.Playing)
                {
                    writer.CompleteOutput();
                }
                else if (state == State.Played)
                {
                    voice.StopVoice();
                    state = State.End;
                    AEnd();
                }
            }
        }

        public override void AUpdate()
        {
            if (!autoEnd)
            {
                return;
            }

            if (writer.state == DialogueTypeWriter.TypewriterState.Completed)
            {
                if (Voice.singleton.GetState() == Voice.State.Stopped)
                {
                    state = State.End;
                    AEnd();
                }
            }

        }

        public string GetInfo()
        {
            return $"Dialogue:{gameObject.name}, Text:{text}";
        }
    }
}
