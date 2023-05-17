using UnityEngine;

namespace Funlary
{
    public enum Case { Grid, Runner }
    public class BootLoader : AbstractSingleton<BootLoader>
    {
        private GameObject mainMenu;
        private Case Case = Case.Grid;
        private GameObject game;

        private void Start()
        {
            mainMenu = transform.GetChild(0).gameObject;
        }
        private void DeactivateMenu(GameObject gameObj)
        {
            game = gameObj;
            mainMenu.SetActive(false);
        }

        public void ActivateMainMenu()
        {
            mainMenu.SetActive(true);
            Destroy(game);
        }

        public void _CreateGame()
        {
            GameObject gameObj = Object.Instantiate(Resources.Load<GameObject>($"Sections/Game"));
            gameObj.name = "--->GAME";
            DeactivateMenu(gameObj);
        }

        public void _CreateMyRoom()
        {
            GameObject gameObj = Object.Instantiate(Resources.Load<GameObject>($"Sections/MyRoom"));
            gameObj.name = "--->MY ROOM";
            DeactivateMenu(gameObj);
        }
    }
}
