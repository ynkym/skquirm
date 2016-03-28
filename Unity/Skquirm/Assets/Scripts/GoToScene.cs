using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class GoToScene : MonoBehaviour {

  static Dictionary<string, int> sceneList = new Dictionary<string, int>(){
    {"Title", 0},
    {"Tank", 1},
    {"Toilet", 2},
    {"Sewer", 3},
    {"GameOver", 4}
  };

  static public int currentGame;
  static public int maxGame = 3;

  // called from result / game over screen
  public static void goNext(){
    if (currentGame > 0 && currentGame < maxGame){
      sceneTransitionByIndex(currentGame + 1);
    }else{
      windTransition(sceneList["Title"]);
    }
  }

  // called from title screen
  public static void startGame(string sceneName){
    PlayerScore.Clear();
    ItemStateUI.Clear();
    LifeStateUI.Clear();
    sceneTransition(sceneName);
  }

  // called from gametimer
  public static void goToResult(int stageNum){
    currentGame = stageNum;
    sceneTransition("GameOver");
  }

  public static void sceneTransition(string sceneName){
    int sceneIndex = sceneList[sceneName];
    sceneTransitionByIndex(sceneIndex);
  }

  public static void sceneTransitionByIndex(int sceneIndex){
    var fader = new FadeTransition()
    {
      nextScene = sceneIndex,
      fadedDelay = 0.1f,
      fadeToColor = Color.black
    };
    TransitionKit.instance.transitionWithDelegate( fader );
  }

  public static void windTransition(int sceneIndex){
      //int sceneIndex = sceneList[sceneName];
      var wind = new WindTransition()
      {
        nextScene = sceneIndex,
        duration = 1.0f,
        size = 0.3f
      };
      TransitionKit.instance.transitionWithDelegate( wind );
  }
}
