﻿// Decompiled with JetBrains decompiler
// Type: CigsScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF03FFAE-974C-4193-BB83-3E6945841C76
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CigsScript : MonoBehaviour
{
  public PromptScript Prompt;

  private void Update()
  {
    if ((double) this.Prompt.Circle[0].fillAmount != 0.0)
      return;
    SchemeGlobals.SetSchemeStage(3, 3);
    this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
    this.Prompt.Yandere.Inventory.Cigs = true;
    this.Prompt.Yandere.TheftTimer = 0.1f;
    Object.Destroy((Object) this.gameObject);
    this.Prompt.Yandere.StudentManager.TaskManager.CheckTaskPickups();
    this.Prompt.Yandere.StolenObjectID = 1;
    Debug.Log((object) "Yandere-chan just grabbed a box of cigarettes.");
  }
}
