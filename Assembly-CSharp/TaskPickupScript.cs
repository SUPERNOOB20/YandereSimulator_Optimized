﻿// Decompiled with JetBrains decompiler
// Type: TaskPickupScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEBC9029-E754-4F76-ACC2-E5BB554B97F0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TaskPickupScript : MonoBehaviour
{
  public PromptScript Prompt;
  public int TaskObjectID;
  public int ButtonID = 3;

  private void Update()
  {
    if ((double) this.Prompt.Circle[this.ButtonID].fillAmount != 0.0)
      return;
    this.Prompt.Yandere.StudentManager.TaskManager.CheckTaskPickups();
    if (this.TaskObjectID != 25)
      return;
    this.Prompt.Yandere.Inventory.Bra = true;
  }
}
