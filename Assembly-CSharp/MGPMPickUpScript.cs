﻿// Decompiled with JetBrains decompiler
// Type: MGPMPickUpScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF03FFAE-974C-4193-BB83-3E6945841C76
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MGPMPickUpScript : MonoBehaviour
{
  public float Speed;

  private void Update()
  {
    this.transform.Translate(Vector3.up * Time.deltaTime * this.Speed * -1f);
    if ((double) this.transform.localPosition.y >= -300.0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
