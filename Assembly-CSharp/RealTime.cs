﻿// Decompiled with JetBrains decompiler
// Type: RealTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD17A22F-B301-43EA-811A-FA797D0BA442
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RealTime : MonoBehaviour
{
  public static float time => Time.unscaledTime;

  public static float deltaTime => Time.unscaledDeltaTime;
}
