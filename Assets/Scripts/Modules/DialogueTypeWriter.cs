using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Modules
{
    /*
        代码出自
        https://blog.csdn.net/qq_21397217/article/details/119155513
    */


    /// <summary>
    /// 用于TextMeshPro的打字机效果组件。
    /// 已知问题：
    ///   1. FadeRange大于0时，会强制将可见字符的透明度设为完全不透明。
    ///      要修复此问题，需要在开始输出字符前记录所有字符的原始透明度，并在执行字符淡化时代入记录的原始透明度进行计算。
    ///   2. 带有删除线、下划线、背景色等效果的文本不能正常显示。
    ///   3. 输出字符的过程中改变TextMeshPro组件的RectTransform参数，会导致文本显示异常。
    /// </summary>

    public class DialogueTypeWriter : MonoBehaviour
    {
        #region Type Define
        public enum TypewriterState
        {
            Completed,
            Outputting,
            Interrupted
        }

        public byte OutputSpeed
        {
            get { return _outputSpeed; }
            set
            {
                _outputSpeed = value;
                CompleteOutput();
            }
        }

        public byte FadeRange
        {
            get { return _fadeRange; }
            set
            {
                _fadeRange = value;
                CompleteOutput();
            }
        }
        #endregion

        #region Static
        public static DialogueTypeWriter singleton = null;
        #endregion

        #region Field
        //setting
        [Tooltip("字符输出速度（字数/秒）")]
        [Range(1, 255)]
        [SerializeField]
        private byte _outputSpeed = 20;

        [SerializeField]
        private TMP_Text _textComponent = null;

        private byte _fadeRange = 0;


        //runtime
        public TypewriterState state { get; private set; } = TypewriterState.Completed;

        private Coroutine _outputCoroutine;

        private Action<TypewriterState> _outputEndCallback;
        #endregion

        #region Function
        /*
            -------------------
                ⬤ General
            -------------------
        */
        private void Awake()
        {
            singleton = this;
        }

        private void OnDisable()
        {
            if (state == TypewriterState.Outputting)
            {
                //中断输出
                state = TypewriterState.Interrupted;
                StopCoroutine(_outputCoroutine);
                OnOutputEnd(true);
            }
        }

        private void OnValidate()
        {
            if (state == TypewriterState.Outputting)
            {
                OutputText(_textComponent.text);
            }
        }
        /*
            -------------------
                ⬤ Public
            -------------------
        */

        /// <summary>
        /// 输出文字。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="outputEndCallback"></param>
        public void OutputText(string text, Action<TypewriterState> outputEndCallback = null)
        {
            // 如果当前正在执行字符输出，将其中断
            if (state == TypewriterState.Outputting)
            {
                StopCoroutine(_outputCoroutine);

                state = TypewriterState.Interrupted;
                OnOutputEnd(false);
            }

            _textComponent.text = text;
            _outputEndCallback = outputEndCallback;

            // 如果对象未激活，直接完成输出
            if (!isActiveAndEnabled)
            {
                state = TypewriterState.Completed;
                OnOutputEnd(true);
                return;
            }


            // 开始新的字符输出协程
            if (FadeRange > 0)
            {
                _outputCoroutine = StartCoroutine(OutputCharactersFading());
            }
            else
            {
                _outputCoroutine = StartCoroutine(OutputCharactersNoFading());
            }
        }

        /// <summary>
        /// 完成正在进行的打字机效果，将所有文字显示出来。
        /// </summary>
        public void CompleteOutput()
        {
            if (state == TypewriterState.Outputting)
            {
                state = TypewriterState.Completed;
                StopCoroutine(_outputCoroutine);
                OnOutputEnd(true);
            }
        }

        /*
            -------------------
                ⬤ Private
            -------------------
        */

        /// <summary>
        /// 以不带淡入效果输出字符的协程。
        /// </summary>
        /// <param name="skipFirstCharacter"></param>
        /// <returns></returns>
        private IEnumerator OutputCharactersNoFading(bool skipFirstCharacter = false)
        {
            state = TypewriterState.Outputting;

            // 先隐藏所有字符
            _textComponent.maxVisibleCharacters = skipFirstCharacter ? 1 : 0;
            _textComponent.ForceMeshUpdate();

            // 按时间逐个显示字符
            var timer = 0f;
            var interval = 1.0f / OutputSpeed;
            var textInfo = _textComponent.textInfo;
            while (_textComponent.maxVisibleCharacters < textInfo.characterCount)
            {
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    timer = 0;
                    _textComponent.maxVisibleCharacters++;
                }

                yield return null;
            }

            // 输出过程结束
            state = TypewriterState.Completed;
            OnOutputEnd(false);
        }

        /// <summary>
        /// 以带有淡入效果输出字符的协程。
        /// </summary>
        /// <returns></returns>
        private IEnumerator OutputCharactersFading()
        {
            state = TypewriterState.Outputting;

            // 确保字符处于可见状态
            var textInfo = _textComponent.textInfo;
            _textComponent.maxVisibleCharacters = textInfo.characterCount;
            _textComponent.ForceMeshUpdate();

            // 没有字符时，直接结束输出
            if (textInfo.characterCount == 0)
            {
                state = TypewriterState.Completed;
                OnOutputEnd(false);

                yield break;
            }

            // 先将所有字符设置到透明状态
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                SetCharacterAlpha(i, 0);
            }

            // 按时间逐渐显示字符
            var timer = 0f;
            var interval = 1.0f / OutputSpeed;
            var headCharacterIndex = 0;
            while (state == TypewriterState.Outputting)
            {
                timer += Time.deltaTime;

                // 计算字符顶点颜色透明度
                var isFadeCompleted = true;
                var tailIndex = headCharacterIndex - FadeRange + 1;
                for (int i = headCharacterIndex; i > -1 && i >= tailIndex; i--)
                {
                    // 不处理不可见字符，否则可能导致某些位置的字符闪烁
                    if (!textInfo.characterInfo[i].isVisible)
                    {
                        continue;
                    }

                    var step = headCharacterIndex - i;
                    var alpha = (byte)Mathf.Clamp((timer / interval + step) / FadeRange * 255, 0, 255);

                    isFadeCompleted &= alpha == 255;
                    SetCharacterAlpha(i, alpha);
                }

                _textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                // 检查是否完成字符输出
                if (timer >= interval)
                {
                    if (headCharacterIndex < textInfo.characterCount - 1)
                    {
                        timer = 0;
                        headCharacterIndex++;
                    }
                    else if (isFadeCompleted)
                    {
                        state = TypewriterState.Completed;
                        OnOutputEnd(false);

                        yield break;
                    }
                }

                yield return null;
            }
        }

        /// <summary>
        /// 设置字符的顶点颜色Alpha值。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="alpha"></param>
        private void SetCharacterAlpha(int index, byte alpha)
        {
            var materialIndex = _textComponent.textInfo.characterInfo[index].materialReferenceIndex;
            var vertexColors = _textComponent.textInfo.meshInfo[materialIndex].colors32;
            var vertexIndex = _textComponent.textInfo.characterInfo[index].vertexIndex;

            vertexColors[vertexIndex + 0].a = alpha;
            vertexColors[vertexIndex + 1].a = alpha;
            vertexColors[vertexIndex + 2].a = alpha;
            vertexColors[vertexIndex + 3].a = alpha;
        }

        /// <summary>
        /// 处理输出结束逻辑。
        /// </summary>
        /// <param name="isShowAllCharacters"></param>
        private void OnOutputEnd(bool isShowAllCharacters)
        {
            // 清理协程
            _outputCoroutine = null;

            // 将所有字符显示出来
            if (isShowAllCharacters)
            {
                var textInfo = _textComponent.textInfo;
                for (int i = 0; i < textInfo.characterCount; i++)
                {
                    SetCharacterAlpha(i, 255);
                }

                _textComponent.maxVisibleCharacters = textInfo.characterCount;
                _textComponent.ForceMeshUpdate();
            }

            // 触发输出完成回调
            if (_outputEndCallback != null)
            {
                var temp = _outputEndCallback;
                _outputEndCallback = null;
                temp.Invoke(state);
            }
        }
        #endregion
    }
}