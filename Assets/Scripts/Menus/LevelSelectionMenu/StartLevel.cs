using System.Collections.Generic;
using UnityEngine;

namespace Menus.LevelSelectionMenu
{
    public class StartLevel : MonoBehaviour
    {
        public List<GameObject> levelParents; // The list containing all level parents to disable before enabling a specific level

        // Maths
        public GameObject balloonBurst;
        public GameObject malletMath;
        public GameObject colorCount;
        public GameObject tickTockTurn;
        
        private void Start()
        {
            DisableAllLevels();
            switch (GameManager.Instance.gameLevel)
            {
                case GameLevel.BalloonBurst:
                    StartBalloonBurst();
                    break;
                case GameLevel.MalletMath:
                    StartMalletMath();
                    break;
                case GameLevel.ColorCount:
                    StartColorCount();
                    break;
                case GameLevel.TickTockTurn:
                    StartTickTockTurn();
                    break;
            }
        }

        public void DisableAllLevels()
        {
            foreach (GameObject levelParent in levelParents)
            {
               levelParent.SetActive(false); 
            }
        }

        // Maths
        public void StartBalloonBurst()
        {
            balloonBurst.SetActive(true);
        }

        public void StartMalletMath()
        {
            malletMath.SetActive(true);
        }

        public void StartColorCount()
        {
            colorCount.SetActive(true);
        }

        public void StartTickTockTurn()
        {
            tickTockTurn.SetActive(true);
        }
    }
}