using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace MJUtilities
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance { get; private set; }
        public List<SceneLoaderData> sceneLoaderDataList = new ();
        public SceneAsset pickedScene;
        
        private void Awake()
        {
            if (instance && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    
        private void Start()
        {
            
        }
    
        public void PickScene(SceneType sceneType)
        {
            foreach (var scene in sceneLoaderDataList)
            {
                if (scene.sceneType == sceneType)
                    pickedScene = scene.sceneAsset;
            }
        }
    
        [ContextMenu(nameof(RestartScene))]
        public void RestartScene()
        {
            StartCoroutine(RestartSceneCoroutine());
        }
    
        private IEnumerator RestartSceneCoroutine()
        {
            yield return new WaitForSeconds(1f);
                
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            SceneManager.LoadScene(currentSceneIndex);
    
            //OnSceneRestart?.Invoke();
        }
        
        [ContextMenu(nameof(LoadScene))]
        public void LoadScene()
        {
            StartCoroutine(LoadSceneCoroutine());
        }
    
        private IEnumerator LoadSceneCoroutine()
        {
            yield return new WaitForSeconds(1f);
            
            SceneManager.LoadScene(pickedScene.name);
        }
    }
    
    [Serializable]
    public enum SceneType
    {
        MainScene
    }
    [Serializable]
    public class SceneLoaderData
    {
        public SceneType sceneType;
        public SceneAsset sceneAsset;
    }
}