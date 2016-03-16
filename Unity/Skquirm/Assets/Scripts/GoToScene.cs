using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class GoToScene : MonoBehaviour {

  static Dictionary<string, int> sceneList = new Dictionary<string, int>(){
    {"Title", 0},
    {"Scene 1", 1},
    {"GameOver", 2}
  };

	public void loadScene(string sceneName){
    windTransition(sceneName);
    //SceneManager.LoadScene(sceneName);
  }

  public void startGame(string sceneName){
    PlayerScore.Clear();
    ItemStateUI.Clear();
    LifeStateUI.Clear();
    sceneTransition(sceneName);
    //SceneManager.LoadScene(sceneName);
  }

  public static void sceneTransition(string sceneName){
  int sceneIndex = sceneList[sceneName];
      var fader = new FadeTransition()
      {
        nextScene = sceneIndex,
        fadedDelay = 0.1f,
        fadeToColor = Color.black
      };
      TransitionKit.instance.transitionWithDelegate( fader );
  }

  public static void windTransition(string sceneName){
      int sceneIndex = sceneList[sceneName];
      var wind = new WindTransition()
      {
        nextScene = sceneIndex,
        duration = 1.0f,
        size = 0.3f
      };
      TransitionKit.instance.transitionWithDelegate( wind );
  }
}
