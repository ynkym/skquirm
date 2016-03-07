using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

	public void loadScene(string sceneName){
    SceneManager.LoadScene(sceneName);
  }

  public void startGame(string sceneName){
    PlayerScore.Clear();
    ItemStateUI.Clear();
    LifeStateUI.Clear();
    SceneManager.LoadScene(sceneName);
  }
}
