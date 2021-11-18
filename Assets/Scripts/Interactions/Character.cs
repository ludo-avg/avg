using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

namespace Interactions
{
    public class Character: InteractionBase
    {
        public GameObject character { get { return _character; } private set { _character = value; } }
        [SerializeField] private GameObject _character;
        public bool show { get { return _show; } private set { _show = value; } }
        [SerializeField] private bool _show = true;

        public void DataInit(GameObject character, bool show)
        {
            this.character = character;
            _character = character;
            this.show = show;
        }

        public override void AStart()
        {
            base.AStart();

            if (show == true)
            {
                character.SetActive(true);
                SpriteRenderer sr = character.GetComponent<SpriteRenderer>();
                sr.color = new Color(1, 1, 1, 0);
                sr.DOColor(new Color(1, 1, 1, 1), 0.5f);
            }
            else
            {
                character.SetActive(false);
            }

            //是否等待Tween完成？
            //不等待，现在是会有bug的。先不管。
            AEnd();
        }
    }
}
