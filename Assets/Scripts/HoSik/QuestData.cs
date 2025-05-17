using System;
using System.Collections;
using UnityEngine;
using TMPro;

namespace HoSik
{
    public class QuestData : MonoBehaviour
    {
        public ESceneType sceneType;
        public int        questID;
        public string     questScript;
        public string     questGuideScript;

        public bool isQuestCompleted = false;

        public LocalTimer timer;

        private Coroutine _questCoroutine;

        public void ManualQuestRegister()
        {
            InteractionManager.Instance.quests[questID] = this;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InteractionManager.Instance.quests[questID] = this;
                StartQuest(questID);
                UIManager.Instance.SetQuestUpdateRect(questScript);
                UIManager.Instance.SetGuideUpdateRect(questGuideScript);
            }
        }

        public void StartQuest(int id)
        {
            if (id == 0)
            {
                _questCoroutine = StartCoroutine(CoQuest0Timer());
            }
            else if (id == 1)
            {
                isQuestCompleted = true;
            }
            else if (id == 2)
            {
                isQuestCompleted = true;
            }
            else if (id == 3)
            {
                InteractionManager.Instance.hasReachedBusStation = true;
                timer.StopTimer();
            }
            else if (id == 4)
            {
                isQuestCompleted = true;
            }
            else if (id == 5)
            {
                if (isQuestCompleted)
                {
                    UIManager.Instance.SetGuideUpdateRect("약속에 늦지는 않겠어");
                }
            }
            else if (id == 6)
            {
                isQuestCompleted = true;
            }
            else if (id == 7)
            {
                isQuestCompleted = true;
            }
        }

        IEnumerator CoQuest0Timer()
        {
            timer.StartTimer();
            while (!timer.isTimeOver)
            {

                yield return null;
            }

            if (!InteractionManager.Instance.hasReachedBusStation)
            {
                UIManager.Instance.SetGuideUpdateRect("나 : 버스를 놓져버렸다");
                UIManager.Instance.SetQuestUpdateRect("목표 실패");
                
                timer.InitTimer();
                InteractionManager.Instance.TeleportCharacter();
                isQuestCompleted = false;
            }
            else if (InteractionManager.Instance.hasReachedBusStation)
            {
                isQuestCompleted = true;
            }
        }
    }
}
