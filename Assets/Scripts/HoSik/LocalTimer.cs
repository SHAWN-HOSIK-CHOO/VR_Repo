using System;
using System.Collections;
using UnityEngine;

namespace HoSik
{
    public class LocalTimer : MonoBehaviour
    {
        public  float targetTime   = 180f;
        private float _currentTime = 0f;

        public bool isTimeOver = false;

        private Coroutine _currentCoroutine;

        public void InitTimer()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            UIManager.Instance.timer.SetActive(false);
            _currentTime = 0f;
            isTimeOver   = false;
        }
        
        public void StartTimer()
        {
            UIManager.Instance.timer.SetActive(true);
            _currentCoroutine = StartCoroutine(CoStartTimer());
        }

        public void StopTimer()
        {
            UIManager.Instance.timer.SetActive(false);
            StopCoroutine(_currentCoroutine);
        }
        
        IEnumerator CoStartTimer()
        {
            _currentTime = targetTime;
            UIManager.Instance.SetTimerText(Mathf.CeilToInt(_currentTime)); 

            while (_currentTime > 0f)
            {
                float prevTime = Mathf.CeilToInt(_currentTime); 
                _currentTime -= Time.deltaTime;
                
                if (!Mathf.Approximately(Mathf.CeilToInt(_currentTime), prevTime))
                {
                    UIManager.Instance.SetTimerText(Mathf.CeilToInt(_currentTime));
                }

                yield return null;
            }

            _currentTime = 0f;
            isTimeOver   = true;
            UIManager.Instance.SetTimerText(0); 
        }
    }
}
