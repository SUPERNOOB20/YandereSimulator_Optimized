﻿// Decompiled with JetBrains decompiler
// Type: YanvaniaBlackHoleScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEBC9029-E754-4F76-ACC2-E5BB554B97F0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class YanvaniaBlackHoleScript : MonoBehaviour
{
  public GameObject BlackHoleAttack;
  public int Attacks;
  public float SpawnTimer;
  public float Timer;

  private void Update()
  {
    this.Timer += Time.deltaTime;
    if ((double) this.Timer <= 1.0)
      return;
    this.SpawnTimer -= Time.deltaTime;
    if ((double) this.SpawnTimer > 0.0 || this.Attacks >= 5)
      return;
    Object.Instantiate<GameObject>(this.BlackHoleAttack, this.transform.position, Quaternion.identity);
    this.SpawnTimer = 0.5f;
    ++this.Attacks;
  }
}
