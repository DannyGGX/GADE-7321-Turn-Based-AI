using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DannyG
{

    public class MenuUI : MonoBehaviour
    {
        public bool settingsChosen = false; 
        public static int oponentValue = 0;
        public static int difficultyLevel = 0;
        public static int mapValue = 0;

       public void PlayGame()
        {
            SceneManager.LoadSceneAsync("Main Level");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

       public void ExitToMain()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }

        public void PvPSelected()
        {
            settingsChosen = true;
            oponentValue = 1;
        }

        public void AIOneSelected()
        {
            settingsChosen = true;
            oponentValue = 2;
            difficultyLevel = 1;
        }

        public void AITwoSelected()
        {
            settingsChosen = true;
            oponentValue = 2;
            difficultyLevel = 2;
        }

        public void Play1Selected()
        {
            if (settingsChosen == true) 
            {
                SceneManager.LoadSceneAsync("TestScene");
                mapValue = 1;
                settingsChosen = false;
            }
        }

        public void Play2Selected()
        {
            if (settingsChosen == true)
            {
                SceneManager.LoadSceneAsync("TestScene");
                mapValue = 2;
                settingsChosen = false;
            }
        }

        public void Play3Selected()
        {
            if (settingsChosen == true)
            {
                SceneManager.LoadSceneAsync("TestScene");
                mapValue = 3;
                settingsChosen = false;
            }
        }
    }
}
