﻿// Decompiled with JetBrains decompiler
// Type: MaidDereMinigame.ExitMaidMinigame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEBC9029-E754-4F76-ACC2-E5BB554B97F0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MaidDereMinigame
{
  public class ExitMaidMinigame : MonoBehaviour
  {
    private void OnMouseOver()
    {
      if (!Input.GetMouseButtonDown(0))
        return;
      GameController.GoToExitScene();
    }
  }
}
