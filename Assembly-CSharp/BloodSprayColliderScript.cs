﻿// Decompiled with JetBrains decompiler
// Type: BloodSprayColliderScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEBC9029-E754-4F76-ACC2-E5BB554B97F0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class BloodSprayColliderScript : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer != 13)
      return;
    YandereScript component = other.gameObject.GetComponent<YandereScript>();
    if (!((Object) component != (Object) null))
      return;
    component.Bloodiness = 100f;
    Object.Destroy((Object) this.gameObject);
  }
}
