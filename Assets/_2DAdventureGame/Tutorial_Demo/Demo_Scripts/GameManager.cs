using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Beginner2D
{
    /// <summary>
    /// This class is the "glue" between all the system in the game. It's a single entry point to be able to manipulate
    /// relationship between object. For example, it will find all the enemies in the scene and check if they are fix
    /// </summary>
    [DefaultExecutionOrder(10)]
    public class GameManager : MonoBehaviour
    {
        public PlayerController player;
        Enemy[] enemies;

        public UIHandler uiHandler;

        private bool gameEnded = false;
        int enemiesFixed = 0;

        private void Awake()
        {
            Physics2D.IgnoreLayerCollision(Helpers.ProjectileLayer, Helpers.ProjectileLayer, true);
            Physics2D.IgnoreLayerCollision(Helpers.PlayerLayer, Helpers.ProjectileLayer, true);
            
            for (int i = 0; i < 32; i++)
                Physics2D.IgnoreLayerCollision(Helpers.ConfinerLayer, i, true);
        }

        void Start()
        {
            enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

            foreach (var enemy in enemies)
            {
                enemy.OnFixed += HandleEnemyFixed;
            }
            uiHandler.SetCounter(0, enemies.Length);

            player.OnTalkedToNPC += HandlePlayerTalkedToNPC;
        }
        
        void Update()
        {
            if (gameEnded) return;

            // Lose condition
            if (player.health <= 0)
            {
                gameEnded = true;
                player.enabled = false;
                uiHandler.DisplayLoseScreen();
                Invoke(nameof(ReloadScene), 3f);
            }
        }

        void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void HandleEnemyFixed()
        {
            enemiesFixed++;
            uiHandler.SetCounter(enemiesFixed, enemies.Length);

            if (!gameEnded && player.health > 0 && enemiesFixed == enemies.Length)
            {
                gameEnded = true;
                player.enabled = false;
                uiHandler.DisplayWinScreen();
                Invoke(nameof(ReloadScene), 3f);
            }
        }

        void HandlePlayerTalkedToNPC()
        {
            if (!gameEnded) uiHandler.DisplayDialogue();
        }
    }
}
