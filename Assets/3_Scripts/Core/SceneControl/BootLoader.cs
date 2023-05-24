using UnityEngine;

namespace Funlary
{
    public class BootLoader : AbstractSingleton<BootLoader>
    {
        private GameObject mainMenu;
        private GameObject game;

        private void Start()
        {
            mainMenu = transform.GetChild(0).gameObject;
        }
        
        public void CreateGameLevel(GameObject referenceObject)
        {
            //GameObject gameObj = Object.Instantiate(Resources.Load<GameObject>($"Sections/Game"));
            var gameObj = Instantiate(referenceObject);
            gameObj.name = "--->" + gameObj.name;
            DeactivateMenu(gameObj);
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
    }
}
