using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

namespace HoSik
{
    public class PlayerWorldCollisionController : MonoBehaviour
    {
        public TMP_Text debugText;

        [Header("Script when player collided with each")]
        [Header("Randomly selected when many")]
        public string[] npcHitScripts = new string[4];

        public string   bicycleHitScript;
        public string[] obstacleHitScripts = new string[4];
        public string   vehicleHitScript;

        private bool _canDisplayMessage = true;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!_canDisplayMessage)
            {
                return;
            }
            
            GameObject goOther = hit.gameObject;
            
            if (goOther.CompareTag("Npc"))
            {
                goOther.GetComponent<NpcSimpleMover>().PlayAudio();
                StartCoroutine(CoShowText());
            }
            else if (goOther.CompareTag("Bicycle"))
            {
                UIManager.Instance.SetGuideUpdateRect(bicycleHitScript);
            }
            else if (goOther.CompareTag("Obstacle"))
            {
                int randomIdx = Random.Range(0, 4);
                UIManager.Instance.SetGuideUpdateRect(obstacleHitScripts[0]);
            }
            else if (goOther.CompareTag("Vehicle"))
            {
                UIManager.Instance.SetGuideUpdateRect(vehicleHitScript);
            }
        }


        IEnumerator CoShowText()
        {
            _canDisplayMessage = false;
            int randomIdx = Random.Range(0, 4);
            UIManager.Instance.SetGuideUpdateRect(npcHitScripts[randomIdx]);
            yield return new WaitForSeconds(0.5f);
            _canDisplayMessage = true;
        }
        
    }
}
