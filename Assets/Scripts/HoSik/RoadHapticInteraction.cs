using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.UI;

namespace HoSik
{
    public enum ERoadBlockType
    {
        PointBlock,
        LineBlock,
        Count
    }
    
    public class RoadHapticInteraction : MonoBehaviour
    {
        [Header("Block Type")] 
        public ERoadBlockType thisBlockType;

        [Header("Haptic impulse interval")] 
        public float interval;

        private bool      _isCoroutineRunning = false;
        private Coroutine _coroutine;
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!_isCoroutineRunning)
                {
                    //Debug.Log("Started Coroutine");
                    _coroutine = StartCoroutine(CoCallFunctionWithTime());
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_isCoroutineRunning)
                {
                    //Debug.Log("Ended Coroutine");
                    StopCoroutine(_coroutine);
                    UIManager.Instance.SetWalkingAnimation(false);
                    _isCoroutineRunning = false;
                }
            }
        }

        public static void SendHaptics(ERoadBlockType blockType)
        {
            if (blockType == ERoadBlockType.PointBlock)
            {
                InteractionManager.Instance.rightController.SendHapticImpulse(0.8f, 0.5f);
                InteractionManager.Instance.leftController.SendHapticImpulse(0.8f, 0.5f);
                UIManager.Instance.SetWalkingAnimationColor(Color.green);
                Debug.Log("Point Block Haptic!");
            }
            else if (blockType == ERoadBlockType.LineBlock)
            {
                InteractionManager.Instance.rightController.SendHapticImpulse(0.5f, 1f);
                InteractionManager.Instance.leftController.SendHapticImpulse(0.5f, 1f);
                UIManager.Instance.SetWalkingAnimationColor(Color.white);
                Debug.Log("Line Block Haptic!");
            }
        }

        IEnumerator CoCallFunctionWithTime()
        {
            _isCoroutineRunning = true;
            UIManager.Instance.SetWalkingAnimation(true);
            while (true)
            {
                RoadHapticInteraction.SendHaptics(thisBlockType);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
