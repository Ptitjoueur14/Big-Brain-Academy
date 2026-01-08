using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Modes
{
    public class BrainMass : MonoBehaviour
    {
        [Header("Brain Cloud Prefabs")]
        public BrainCloud smallCloudPrefab; // Cloud to spawn on
        public BrainCloud mediumCloudPrefab;
        public BrainCloud bigCloudPrefab;
        public Vector2 brainCloudSpawnPosition;

        [Header("Brain Mass")]
        public int brainMassNumber;
        public bool isBrainMassDecreasing;
        
        [Header("Brain Mass Settings")]
        public int maxBrainMass; // The maxiumum mass obtainable
        public float maxBrainMassTime; // The time in seconds to get the max brain mass
        public float minBrainMassTime; // The time in seconds to get 1 g brain mass

        [Header("Brain Cloud Settings")] 
        public int bigCloudMinMass;
        public int mediumCloudMinMass;
        
        private void Start()
        {
            brainMassNumber = maxBrainMass;
            StartCoroutine(StartBrainMassDecrease());
        }

        public IEnumerator StartBrainMassDecrease()
        {
            yield return new WaitForSeconds(maxBrainMassTime);
            isBrainMassDecreasing = true;
            float deltaTime = (minBrainMassTime - maxBrainMassTime) / maxBrainMass;
            Debug.Log($"Decreasing Brain Mass from {maxBrainMass} g to 0 g by 1 g every {deltaTime} seconds");
            for (int i = 0; i < maxBrainMass; i++)
            {
                brainMassNumber--;
                yield return new WaitForSeconds(deltaTime);
            }
            isBrainMassDecreasing = false;
        }
        

        public void SpawnBrainCloud()
        {
            BrainCloud brainCloud;
            
            if (brainMassNumber >= bigCloudMinMass)
            {
                brainCloud = Instantiate(bigCloudPrefab, brainCloudSpawnPosition, Quaternion.identity);
            }
            else if (brainMassNumber >= mediumCloudMinMass)
            {
                brainCloud = Instantiate(mediumCloudPrefab, brainCloudSpawnPosition, Quaternion.identity);
            }
            else
            {
                brainCloud = Instantiate(smallCloudPrefab, brainCloudSpawnPosition, Quaternion.identity);
            }
            
            brainCloud.brainMassNumberText.text = brainMassNumber + " g";
            RestartBrainMassDecrease();
        }

        public void RestartBrainMassDecrease()
        {
            StopAllCoroutines();
            brainMassNumber = maxBrainMass;
            StartCoroutine(StartBrainMassDecrease());
        }
    }
}