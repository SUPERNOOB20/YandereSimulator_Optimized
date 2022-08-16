﻿// Decompiled with JetBrains decompiler
// Type: Timer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD17A22F-B301-43EA-811A-FA797D0BA442
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class Timer
{
  [SerializeField]
  private float currentSeconds;
  [SerializeField]
  private float targetSeconds;

  public Timer(float targetSeconds)
  {
    this.currentSeconds = 0.0f;
    this.targetSeconds = targetSeconds;
  }

  public float CurrentSeconds => this.currentSeconds;

  public float TargetSeconds => this.targetSeconds;

  public bool IsDone => (double) this.currentSeconds >= (double) this.targetSeconds;

  public float Progress => Mathf.Clamp01(this.currentSeconds / this.targetSeconds);

  public void Reset() => this.currentSeconds = 0.0f;

  public void SubtractTarget() => this.currentSeconds -= this.targetSeconds;

  public void Tick(float dt) => this.currentSeconds += dt;
}
