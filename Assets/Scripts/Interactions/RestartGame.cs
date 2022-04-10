using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactions
{
    public class RestartGame : InteractionBase
    {
        float time = 0.75f;
        public override void AStart()
        {
            IEnumerator Restart()
            {
                yield return new WaitForSeconds(time);

                AEnd();
                gameObject.SetActive(false);
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }

            base.AStart();
            gameObject.SetActive(true);
            Modules.Bgm.singleton.VolumeDown(time);
            StartCoroutine(Restart());
        }
    }
}
