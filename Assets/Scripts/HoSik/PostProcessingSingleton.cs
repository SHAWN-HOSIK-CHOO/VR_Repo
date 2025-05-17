using System;
using UnityEngine;

namespace HoSik
{
    public class PostProcessingSingleton : MonoBehaviour
    {
        private static PostProcessingSingleton _instance = null;
        public static  PostProcessingSingleton Instance => _instance == null ? null : _instance;

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
    }
}
