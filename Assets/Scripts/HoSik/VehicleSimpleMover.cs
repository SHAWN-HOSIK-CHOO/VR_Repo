using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

namespace HoSik
{
    public class VehicleSimpleMover : SimpleMover
    {
        private float _crossLineZoneMinZ = 0f;
        private float _crossLineZoneMaxZ = 0f;
        private bool  _isUpDirection     = false;

        public  float lifeSpan     = 10f;

        private float _areaMin = 0f;
        private float _areaMax = 0f;

        private AudioSource _audio;
        
        private void Update()
        {
            if ((!_isUpDirection && this.transform.position.z >= _areaMax) || (_isUpDirection && this.transform.position.z <= _areaMin))
            {
                Destroy(this.gameObject);
            }
            else
            {
                MoveAttribute();
            }
        }

        public void Init(float areaMin, float areaMax, float minZ, float maxZ, bool isUp)
        {
            _areaMin           = areaMin;
            _areaMax           = areaMax;
            _crossLineZoneMinZ = minZ;
            _crossLineZoneMaxZ = maxZ;
            _isUpDirection     = isUp;

            _audio = GetComponent<AudioSource>();
        }
        
        public override void MoveAttribute()
        {
            if (InteractionManager.Instance.currentTrafficLightState == ETrafficLightState.Red)
            {
                //_audio.Pause();
                if (_isUpDirection && this.transform.position.z <= _crossLineZoneMaxZ)
                {
                    
                }
                else if(!_isUpDirection && this.transform.position.z >= _crossLineZoneMinZ)
                {
                    
                }
                else
                {
                    _audio.Pause();
                    return;
                }
            }

            if (!_audio.isPlaying)
            {
                _audio.UnPause();
            }
            
            _elapsedTime += Time.deltaTime;
            
            if (_elapsedTime >= duration)
            {
                _elapsedTime   = 0f; 
            }
            
            transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
        }
    }
}
