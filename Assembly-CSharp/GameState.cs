﻿// Decompiled with JetBrains decompiler
// Type: GameState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF03FFAE-974C-4193-BB83-3E6945841C76
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

[Serializable]
public class GameState
{
  public int Score;
  public float Health;
  public int LongestCombo;
  public int Combo;
  public Dictionary<DDRRating, int> Ratings;
  public DDRFinishStatus FinishStatus;

  public GameState()
  {
    this.Health = 100f;
    this.Ratings = new Dictionary<DDRRating, int>();
    this.Ratings.Add(DDRRating.Early, 0);
    this.Ratings.Add(DDRRating.Good, 0);
    this.Ratings.Add(DDRRating.Great, 0);
    this.Ratings.Add(DDRRating.Miss, 0);
    this.Ratings.Add(DDRRating.Ok, 0);
    this.Ratings.Add(DDRRating.Perfect, 0);
  }
}
