using System;
using System.Collections;
using Architecture.Services;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject winWindow;
        [SerializeField] private GameObject loseWindow;
        [SerializeField] private Button restartButton;
        
        private SceneLoader _sceneLoader;
        private PlayerController _player;

        private void OnEnable()
        {
            restartButton.onClick.AddListener(RestartScene);
            
            winWindow.SetActive(false);
            loseWindow.SetActive(false);
            restartButton.gameObject.SetActive(false);

            StartCoroutine(SubscribeOnPlayer());
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveAllListeners();
            if (_player != null)
            {
                _player.PlayerLose -= PlayerOnPlayerLose;
                _player.PlayerWin -= PlayerOnPlayerWin;
            }
        }

        public void Initialize(PlayerController player, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _player = player;
        }

        private IEnumerator SubscribeOnPlayer()
        {
            while (_player == null)
            {
                yield return null;
            }
            _player.PlayerLose += PlayerOnPlayerLose;
            _player.PlayerWin += PlayerOnPlayerWin;
        }
        
        private void PlayerOnPlayerWin()
        {
            winWindow.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        private void PlayerOnPlayerLose()
        {
            loseWindow.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        private void RestartScene()
        {
            _sceneLoader.RestartScene();
        }
    }
}
