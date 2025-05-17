using System;
using UnityEngine;

namespace HoSik
{
    public class PlayerManager : MonoBehaviour
    {
        private static PlayerManager _instance = null;
        public static  PlayerManager Instance => _instance == null ? null : _instance;

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
