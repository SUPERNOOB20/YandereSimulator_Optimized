﻿// Decompiled with JetBrains decompiler
// Type: TaskPickupScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12831466-57D6-4F5A-B867-CD140BE439C0
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
