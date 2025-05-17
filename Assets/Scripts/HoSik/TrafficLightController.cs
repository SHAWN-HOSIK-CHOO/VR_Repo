using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System;

namespace HoSik
{
    public enum ETrafficLightState
    {
        Red,
        Green,
        Count
    }
    public class TrafficLightController : MonoBehaviour
    {
        public  float trafficChangeTime   = 15f;
        private float _currentTrafficTime = 0f;

        public  float carSpawnFrequency = 5f;
        private float _currentSpawnTime = 0f;
        
        public Transform[]  carSpawnPositions = new Transform[4];
        public GameObject[] pfCarObjects      = new GameObject[4];

        [Header("횡단보도 영역")]
        public float crossLineZoneMinZ = 0f;
        public                     float crossLineZoneMaxZ = 0f;
        [Header("전체 영역 z")] 
        public float areaBorderMinZ    = 0f;

        public float areaBorderMaxZ = 0f;

        private void Start()
        {
            StartCoroutine(CoTrafficChangeTimer());
            SpawnCars();
        }

        private void SpawnCars()
        {
            for (int i = 0; i < 4; i++)
            {
                if (pfCarObjects[i] == null)
                {
                    continue;
                }
                
                GameObject pfCar = Instantiate(pfCarObjects[i], carSpawnPositions[i].transform.position,
                                               carSpawnPositions[i].transform.rotation);

                pfCar.GetComponent<VehicleSimpleMover>()
                     .Init(areaBorderMinZ,areaBorderMaxZ,crossLineZoneMinZ, crossLineZoneMaxZ, i is>= 0 and<= 1);
            }
        }
        
        IEnumerator CoTrafficChangeTimer()
        {
            while (true)
            {
                _currentTrafficTime += Time.deltaTime;
                _currentSpawnTime   += Time.deltaTime;
               
                if (_currentTrafficTime >= trafficChangeTime)
                {
                    if (InteractionManager.Instance.currentTrafficLightState == ETrafficLightState.Green)
                    {
                        InteractionManager.Instance.currentTrafficLightState = ETrafficLightState.Red;
                        Debug.Log("Changed Red");
                    }
                    else if (InteractionManager.Instance.currentTrafficLightState == ETrafficLightState.Red)
                    {
                        InteractionManager.Instance.currentTrafficLightState = ETrafficLightState.Green;
                        Debug.Log("Changed Green");
                    }

                    _currentTrafficTime = 0f;
                }

                if (InteractionManager.Instance.currentTrafficLightState != ETrafficLightState.Red && _currentSpawnTime >= carSpawnFrequency)
                {
                    SpawnCars();
                    _currentSpawnTime = 0f;
                }
                
                yield return null;
            }
            
        }
    }
}
