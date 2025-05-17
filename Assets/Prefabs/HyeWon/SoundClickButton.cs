using System;
using System.Collections;
using HoSik;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Prefabs.HyeWon
{
    public class SoundClickButton : MonoBehaviour
    {
        private AudioSource crossWalkAlarm;
        private XRBaseInteractable interactable;

        public int relatedQuestID = -1;

        [Obsolete("Obsolete")]
        void Start()
        {
            // AudioSource 및 XRBaseInteractable 가져오기
            crossWalkAlarm = GetComponent<AudioSource>();
            interactable = GetComponent<XRBaseInteractable>();

            if (relatedQuestID != -1)
            {
                this.GetComponent<QuestData>().ManualQuestRegister();
            }

            // 클릭 이벤트 등록
            if (interactable != null)
            {
                interactable.onSelectEntered.AddListener(OnCubeClicked);
            }
        }

        private void OnCubeClicked(XRBaseInteractor interactor)
        {
            // 소리 재생
            if (crossWalkAlarm != null && !crossWalkAlarm.isPlaying)
            {
                crossWalkAlarm.Play();
            }

            if (relatedQuestID != -1)
            {
                StartCoroutine(CoSignalWithDelay(10.0f));
            }
        }

        [Obsolete("Obsolete")]
        void OnDestroy()
        {
            // 이벤트 등록 해제
            if (interactable != null)
            {
                interactable.onSelectEntered.RemoveListener(OnCubeClicked);
            }
        }

        IEnumerator CoSignalWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            InteractionManager.Instance.Object2Quest_SignalTunnel(relatedQuestID);
        }
    }
}

