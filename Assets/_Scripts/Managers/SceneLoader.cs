using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] SceneLoadEventChannelSO sceneLoadChannel;

    private SceneName currentScene;

    private void OnEnable() {
        sceneLoadChannel.OnEventRaised += HandleSceneLoadRequest;
    }

    private void OnDisable() {
        sceneLoadChannel.OnEventRaised -= HandleSceneLoadRequest;
    }
    
    private void Start() {
        SceneName activeScene;
        if(CheckActiveScene(out activeScene)) {
            currentScene = activeScene;

            if(currentScene == SceneName.Game) {
                HandleSceneLoadRequest(SceneName.StartScreen);
            }

        } else {
            LoadScene(SceneName.StartScreen);
        }
    }

    private void HandleSceneLoadRequest(SceneName sceneToLoad) {
        if(sceneToLoad == currentScene) return;

        UnloadCurrentScene();
        LoadScene(sceneToLoad);
    }

    private void LoadScene(SceneName sceneToLoad) {
        SceneManager.LoadScene(SceneNameToString(sceneToLoad), LoadSceneMode.Additive);
        currentScene = sceneToLoad;
    }

    private void UnloadCurrentScene() {
        SceneManager.UnloadSceneAsync(SceneNameToString(currentScene));
    }

    bool CheckActiveScene(out SceneName activeScene) {
        activeScene = (
            SceneManager.GetSceneByName("Game").IsValid() ?
            SceneName.Game :
            SceneName.StartScreen
        );
        return SceneManager.sceneCount > 1;
    }

    private string SceneNameToString(SceneName sceneName) {
        return sceneName == SceneName.Game ? "Game" : "MainMenu";
    }
}

public enum SceneName {StartScreen, Game}
