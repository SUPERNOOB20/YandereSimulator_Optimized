﻿// Decompiled with JetBrains decompiler
// Type: UnityEngine.PostProcessing.MinAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD17A22F-B301-43EA-811A-FA797D0BA442
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

namespace UnityEngine.PostProcessing
{
  public sealed class MinAttribute : PropertyAttribute
  {
    public readonly float min;

    public MinAttribute(float min) => this.min = min;
  }
}
