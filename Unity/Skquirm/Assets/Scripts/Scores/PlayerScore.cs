using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScore : Object {

  static Dictionary<int, PlayerScore> Instances = new Dictionary<int, PlayerScore>();

  public static void AddScoreToPlayer(int playerNum, int increment=1){
    if (Instances.ContainsKey(playerNum)){
        Instances[playerNum].AddScore(increment);
    }
  }

  public static int GetScore(int playerNum){
    if (Instances.ContainsKey(playerNum)){
      return Instances[playerNum].GetScore();
    }else{
      return -1;
    }
  }

  public static PlayerScore Create(int playerNum){
    return new PlayerScore(playerNum);
  }

  public static void Clear(){ Instances.Clear(); }

  private int score;

  public PlayerScore(int playerNum){
    if (Instances.ContainsKey(playerNum)){
      Instances[playerNum] = this;
    }else{
      Instances.Add(playerNum, this);
    }
    score = 0;
  }

  void AddScore(int increment){
    score = score + increment;
  }

  int GetScore(){
    return score;
  }
}
