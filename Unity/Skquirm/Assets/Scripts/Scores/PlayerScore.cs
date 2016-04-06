using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScore : Object, System.IComparable<PlayerScore> {

  public static int maxScore = 0;

  static Dictionary<int, PlayerScore> Instances = new Dictionary<int, PlayerScore>();
  // for debugging
  // static Dictionary<int, PlayerScore> Instances = new Dictionary<int, PlayerScore>(){
  //   {0, new PlayerScore(0, 0)},
  //   {1, new PlayerScore(1, 2)},
  //   {2, new PlayerScore(2, 4)},
  //   {3, new PlayerScore(3, 1)}
  // };

  public static void AddScoreToPlayer(int playerNum, int increment=1){
    if (Instances.ContainsKey(playerNum)){
      Instances[playerNum].AddScore(increment);

      // update maxScore
      maxScore = 0;
      for (int i = 0; i < 4; i++){
        maxScore = Instances[i].score > maxScore ? Instances[i].score : maxScore;
      }

      // update rank
      for (int i = 0; i < 4; i++){
        Instances[i].CheckWinner();
      }
    }
  }

  public static int GetScore(int playerNum){
    if (Instances.ContainsKey(playerNum)){
      return Instances[playerNum].GetScore();
    }else{
      return 0;
    }
  }

  public static void CheckWinnerForPlayer(int playerNum){
    if (Instances.ContainsKey(playerNum)){
      Instances[playerNum].CheckWinner();
    }
  }

  public static PlayerScore Create(int playerNum){
    if (Instances.ContainsKey(playerNum)){
      return Instances[playerNum];
    }else{
      return new PlayerScore(playerNum);
    }
  }

  public static List<PlayerScore> GetSortedScores(){
    List<PlayerScore> scores = new List<PlayerScore>();
    foreach (PlayerScore score in Instances.Values){
        scores.Add(score);
    }
    scores.Sort();
    return scores;
  }

  public static void Clear(){ Instances.Clear(); maxScore = 0; }

  public int playerNum;
  public int score;

  public PlayerScore(int playerNum){
    Instances.Add(playerNum, this);
    this.playerNum = playerNum;
    score = 0;
  }

  public PlayerScore(int playerNum, int score){
    //Instances.Add(playerNum, this);
    this.playerNum = playerNum;
    this.score = score;
  }

  public int CompareTo(PlayerScore other){
    return other.score - this.score;
  }

  public void CheckWinner(){
    if (score >= maxScore && score != 0){
      WinnerIndicatorBehavior.ShowForPlayer(playerNum);
    }else{
      WinnerIndicatorBehavior.HideForPlayer(playerNum);
    }
  }

  void AddScore(int increment){
    score = score + increment;
  }

  int GetScore(){
    return score;
  }
}
