﻿// Decompiled with JetBrains decompiler
// Type: GhostScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DC2A12D-6390-4505-844F-2E3192236485
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GhostScript : MonoBehaviour
{
  public Transform SmartphoneCamera;
  public Transform Neck;
  public Transform GhostEyeLocation;
  public Transform GhostEye;
  public int Frame;
  public bool Move;

  private void Update()
  {
    if ((double) Time.timeScale <= 9.9999997473787516E-05)
      return;
    if (this.Frame > 0)
    {
      this.GetComponent<Animation>().enabled = false;
      this.gameObject.SetActive(false);
      this.Frame = 0;
    }
    ++this.Frame;
  }

  public void Look() => this.Neck.LookAt(this.SmartphoneCamera.position);
}
