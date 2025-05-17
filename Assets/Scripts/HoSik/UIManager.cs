using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace HoSik
{
    public class UIManager : MonoBehaviour
    {
        public GameObject canvasObject;

        private static UIManager _instance = null;
        public static  UIManager Instance => _instance == null ? null : _instance;

        public  RectTransform guideUpdateRect;
        public  TMP_Text      guideText;
        //private bool          _isMessageOn = false;
        private Coroutine     _messageCoroutine;

        public  GameObject walkingAnimation;
        private Image      _walkingAnimationImage;

        public GameObject soundAnimation;

        public GameObject timer;
        public TMP_Text   timerText;

        public RectTransform questUpdateRect;
        public TMP_Text      questUpdateRectText;

        public TMP_Text questInfoText;
        
        public GameObject[] onUIsForDoorOpen  = new GameObject[4];
        public GameObject[] offUIsForDoorOpen = new GameObject[4];
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void SetGuideText(string txt)
        {
            guideText.text = txt;
        }

        public void SetWalkingAnimation(bool flag)
        {
            walkingAnimation.SetActive(flag);
        }
        
        public void SetTimerText(int time)
        {
            timerText.text = time / 60 + "m " + time % 60 + "s";            
        }

        public void SetQuestUpdateRect(string txt)
        {
            questUpdateRectText.text = txt;
            SetQuestInfoText(txt);
            //StartCoroutine(CoQuestUpdateAnimation(new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.6f, 1.6f, 1.0f), 2.5f));
        }

        public void SetWalkingAnimationColor(Color c)
        {
            _walkingAnimationImage = walkingAnimation.GetComponent<Image>();
            _walkingAnimationImage.color = c;
        }
        
        public void SetGuideUpdateRect(string txt)
        {
            if (_messageCoroutine != null)
            {
                StopCoroutine(_messageCoroutine);
                guideUpdateRect.gameObject.SetActive(false);
            }
            
            SetGuideText(txt);
            _messageCoroutine = StartCoroutine(CoGuideUpdateAnimation( 2.5f));
        }

        private void SetQuestInfoText(string txt)
        {
            questInfoText.text = txt;
        }

        public void InitializeUIForEachScene(ESceneType sceneType)
        {
            questUpdateRect.gameObject.SetActive(false);
            guideUpdateRect.gameObject.SetActive(false);
            timer.SetActive(false);
            
            switch (sceneType)
            {
                case ESceneType.Intro:
                    break;
                case ESceneType.Home:
                    break;
                case ESceneType.Road:
                    soundAnimation.SetActive(false);
                    walkingAnimation.SetActive(false);
                    break;
                case ESceneType.Bus:
                    soundAnimation.SetActive(true);
                    walkingAnimation.SetActive(false);
                    break;
                default:
                    Debug.Log("Invalid Scene Name");
                    break;
            }
        }

        IEnumerator CoQuestUpdateAnimation(Vector3 startScale, Vector3 endScale,float duration = 2.0f)
        {
            questUpdateRect.gameObject.SetActive(true);
            float halfDuration = duration / 2f; 
            float elapsedTime  = 0f;
            
            while (elapsedTime < halfDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / halfDuration); 
                questUpdateRect.localScale = Vector3.Lerp(startScale, endScale, t);
                yield return null;
            }
            
            elapsedTime = 0f; 
            while (elapsedTime < halfDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / halfDuration); 
                questUpdateRect.localScale = Vector3.Lerp(endScale, startScale, t);
                yield return null;
            }
            
            questUpdateRect.localScale = startScale;
            yield return new WaitForSeconds(0.3f);
            questUpdateRect.gameObject.SetActive(false);
        }
        
        IEnumerator CoGuideUpdateAnimation(float duration = 2.0f)
        {
            //_isMessageOn = true;
            guideUpdateRect.gameObject.SetActive(true);
            float elapsedTime  = 0f;
            
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            //_isMessageOn = false;
            guideUpdateRect.gameObject.SetActive(false);
        }
        
        public void DoorOpenedUIChanges()
        {
            foreach (var uiObject in offUIsForDoorOpen)
            {
                uiObject.SetActive(false);
            }
            
            foreach (var uiObject in onUIsForDoorOpen)
            {
                uiObject.SetActive(true);
            }
        }
    }
}
