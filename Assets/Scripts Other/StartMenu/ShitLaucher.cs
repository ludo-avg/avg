using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StartMenu
{
    public class ShitLaucher : MonoBehaviour
    {
        #region Singleton
        public static ShitLaucher singleton;
        private void Awake()
        {
            singleton = this;
        }
        #endregion

        #pragma warning disable 0649
        [SerializeField] private SpriteSet laoMuGenShits;
        [SerializeField] private SpriteSet aWenShits;

        [SerializeField] private Transform laoMuGenTransformParent;
        [SerializeField] private Transform aWenTransformParent;

        [SerializeField] private GameObject shitPrefab;
        #pragma warning restore 0649

        public void LaoMuGenLauch(float? speed = null, float? angle = null, float? rotateSpeed = null)
        {
            GameObject shit = Instantiate(shitPrefab);
            shit.GetComponent<Image>().sprite = laoMuGenShits.images[Random.Range(0, laoMuGenShits.images.Length)];
            shit.transform.SetParent(laoMuGenTransformParent, false);
            shit.transform.localPosition = new Vector3(0, 0, 0);

            shit.GetComponent<Shit>().PreStartSetting(speed, angle, rotateSpeed);
        }

        public void AWenLauch(float? speed = null, float? angle = null, float? rotateSpeed = null)
        {
            GameObject shit = Instantiate(shitPrefab);
            shit.GetComponent<Image>().sprite = aWenShits.images[Random.Range(0, aWenShits.images.Length)];
            shit.transform.SetParent(aWenTransformParent, false);
            shit.transform.localPosition = new Vector3(0, 0, 0);

            shit.GetComponent<Shit>().PreStartSetting(speed, angle, rotateSpeed);
        }

    }
}
