﻿// Decompiled with JetBrains decompiler
// Type: TitleExtrasScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12831466-57D6-4F5A-B867-CD140BE439C0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TitleExtrasScript : MonoBehaviour
{
  public bool Show;

  private void Start() => this.transform.localPosition = new Vector3(1050f, this.transform.localPosition.y, this.transform.localPosition.z);

  private void Update()
  {
    if (!this.Show)
      this.transform.localPosition = new Vector3(Mathf.Lerp(this.transform.localPosition.x, 1050f, Time.deltaTime * 10f), this.transform.localPosition.y, this.transform.localPosition.z);
    else
      this.transform.localPosition = new Vector3(Mathf.Lerp(this.transform.localPosition.x, 0.0f, Time.deltaTime * 10f), this.transform.localPosition.y, this.transform.localPosition.z);
  }
}
