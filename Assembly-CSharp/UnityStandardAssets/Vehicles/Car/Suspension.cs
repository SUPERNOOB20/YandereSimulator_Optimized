﻿// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.Vehicles.Car.Suspension
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEBC9029-E754-4F76-ACC2-E5BB554B97F0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
  public class Suspension : MonoBehaviour
  {
    public GameObject wheel;
    private Vector3 m_TargetOriginalPosition;
    private Vector3 m_Origin;

    private void Start()
    {
      this.m_TargetOriginalPosition = this.wheel.transform.localPosition;
      this.m_Origin = this.transform.localPosition;
    }

    private void Update() => this.transform.localPosition = this.m_Origin + (this.wheel.transform.localPosition - this.m_TargetOriginalPosition);
  }
}
