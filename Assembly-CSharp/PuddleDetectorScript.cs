﻿// Decompiled with JetBrains decompiler
// Type: PuddleDetectorScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DC2A12D-6390-4505-844F-2E3192236485
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PuddleDetectorScript : MonoBehaviour
{
  public PowerSwitchScript PowerSwitch;
  public int Frame;

  private void Update()
  {
    if (this.Frame > 0)
      Object.Destroy((Object) this.gameObject);
    ++this.Frame;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!(other.gameObject.tag == "Puddle") || !other.gameObject.GetComponent<BloodPoolScript>().Water)
      return;
    this.PowerSwitch.NewPuddle = other.gameObject;
    this.PowerSwitch.Electricity.transform.position = this.PowerSwitch.NewPuddle.transform.position;
    this.PowerSwitch.Electricity.SetActive(true);
  }
}
