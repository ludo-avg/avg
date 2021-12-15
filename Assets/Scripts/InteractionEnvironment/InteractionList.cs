//C#
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//Unity
using UnityEngine;
using UnityEngine.Events;
//Plugin
using NaughtyAttributes;
//Project
using Interactions;
using Tools.Extensions;
//Editor
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

public partial class InteractionList : MonoBehaviour
{
    #region Singleton
    public static InteractionList singleton = null;
    void Awake()
    {
        singleton = this;
    }
    #endregion

    //setting
    [SerializeField] UserData userData = null;
    [SerializeField] GameObject backGroundSchoolGate = null;
    [SerializeField] GameObject backGroundPeopleMarket = null;
    [SerializeField] GameObject backGroundRentFatherCorner = null;
    [SerializeField] GameObject backGroundRentFatherCornerWtihoutYoungMan = null;
    [SerializeField] GameObject customInteractionSenarioHome = null;
    [SerializeField] GameObject characterTeacher = null;
    [SerializeField] GameObject characterLaoMuGen = null;
    [SerializeField] GameObject characterYoungMan = null;
    [SerializeField] GameObject characterDirector = null;
    [SerializeField] GameObject characterAWenFather = null;
    [SerializeField] TimeChoice timeChoice = null;
    [SerializeField] Choice choice = null;
    [SerializeField] Custom custom = null;

#if UNITY_EDITOR
    [Button("DeleteAllChildren")]
    private void DeleteAllChildren()
    {
        List<Transform> list = new List<Transform>();
        foreach (Transform child in transform)
        {
            list.Add(child);
        }
        foreach (Transform child in list)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    [Button("CreateInteractions")]
    private void CreateInteractions()
    {
        CreateList();
        DialogueMatchVoice();
        ChoiceMatchVoice();
    }

    private void DialogueMatchVoice()
    {
        #region Setting
        //Function Define
        string DialogueToVoice(string d)
        {
            string v = "";
            if (d.Length <= 10) v = d;
            else v = d.Substring(0, 10);

            return v;
        }

        string GetDialogeInfo(Dialogue d)
        {
            return $"Dialogue:{d.gameObject.name}, Text:{d.text}";
        }

        //Setting
        string voiceFolder = "Assets/Resources/1  Sound_Voice";
        #endregion

        //
        List<Dialogue> list = new List<Dialogue>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Dialogue>() != null)
            {
                var d = child.GetComponent<Dialogue>();
                if (d.audioClip == null) list.Add(child.GetComponent<Dialogue>());
            }
        }
        list = list.OrderBy(o => o.transform.GetSiblingIndex()).ToList();

        Dictionary<string, int> nameCount = new Dictionary<string, int>();
        foreach(Dialogue d in list)
        {
            string fileName = DialogueToVoice(d.text);
            if (!nameCount.ContainsKey(fileName))
            {
                nameCount[fileName] = 1;
            }
            else
            {
                nameCount[fileName] ++;
            }
            string exactFileName = fileName;
            if (nameCount[fileName] > 1)
            {
                exactFileName = fileName + nameCount[fileName];
            }
            string[] guids = AssetDatabase.FindAssets(fileName, new string[] {voiceFolder});
            /*
                需要采用精确匹配。
                guids是模糊匹配的结果。
                所以，这里再作一系列处理。
            */
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
                        if (guidName == exactFileName)
                        {
                            path = guidPath;
                            break;
                        }
                    }
                }
            }
            if (path == null)
            {
                Debug.Log($"找不到voice文件。Info:({GetDialogeInfo(d)})");
                continue;
            }
            var voice = AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));
            if (voice is AudioClip)
            {
                d.audioClip = voice as AudioClip;
            }
            else
            {
                Debug.LogError($"voice文件，不是AudioClip。文件名：{path}");
            }
        }
    }

    private void ChoiceMatchVoice()
    {
        //只是复制了 DialogueMatchVoice 的代码，并修改之。以后有时间再好好重构。

        #region Setting
        //Function Define
        string ChoiceToVoice(string d)
        {
            string v = "";
            if (d.Length <= 10) v = d;
            else v = d.Substring(0, 10);

            return v;
        }

        string GetChoiceInfo(Choice d)
        {
            return $"Dialogue:{d.gameObject.name}, Text:{d.text}";
        }

        //Setting
        string voiceFolder = "Assets/Resources/1  Sound_Voice";
        #endregion

        //
        List<Choice> list = new List<Choice>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Choice>() != null)
            {
                var d = child.GetComponent<Choice>();
                if (d.audioClip == null) list.Add(child.GetComponent<Choice>());
            }
        }
        list = list.OrderBy(o => o.transform.GetSiblingIndex()).ToList();

        Dictionary<string, int> nameCount = new Dictionary<string, int>();
        foreach (Choice d in list)
        {
            string fileName = ChoiceToVoice(d.text);
            if (!nameCount.ContainsKey(fileName))
            {
                nameCount[fileName] = 1;
            }
            else
            {
                nameCount[fileName]++;
            }
            string exactFileName = fileName;
            if (nameCount[fileName] > 1)
            {
                exactFileName = fileName + nameCount[fileName];
            }
            string[] guids = AssetDatabase.FindAssets(fileName, new string[] { voiceFolder });
            /*
                需要采用精确匹配。
                guids是模糊匹配的结果。
                所以，这里再作一系列处理。
            */
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
                        if (guidName == exactFileName)
                        {
                            path = guidPath;
                            break;
                        }
                    }
                }
            }
            if (path == null)
            {
                Debug.Log($"找不到voice文件。Info:({GetChoiceInfo(d)})");
                continue;
            }
            var voice = AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));
            if (voice is AudioClip)
            {
                d.audioClip = voice as AudioClip;
            }
            else
            {
                Debug.LogError($"voice文件，不是AudioClip。文件名：{path}");
            }
        }
    }
#endif
}
