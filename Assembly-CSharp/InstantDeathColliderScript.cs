﻿// Decompiled with JetBrains decompiler
// Type: InstantDeathColliderScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEBC9029-E754-4F76-ACC2-E5BB554B97F0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class InstantDeathColliderScript : MonoBehaviour
{
  public int Frame;

  private void Update()
  {
    ++this.Frame;
    if (this.Frame <= 1)
      return;
    Object.Destroy((Object) this.gameObject);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer != 9)
      return;
    StudentScript component = other.gameObject.GetComponent<StudentScript>();
    if (!((Object) component != (Object) null) || component.StudentID <= 1)
      return;
    if (component.Rival)
    {
      component.StudentManager.RivalEliminated = true;
      component.StudentManager.Police.EndOfDay.RivalEliminationMethod = RivalEliminationType.Accident;
      Debug.Log((object) "Attempting to tell the game that the rival was murdered.");
    }
    component.DeathType = DeathType.Explosion;
    component.BecomeRagdoll();
    Rigidbody allRigidbody = component.Ragdoll.AllRigidbodies[0];
    allRigidbody.isKinematic = false;
    allRigidbody.AddForce((allRigidbody.transform.position - this.transform.position) * 5000f);
  }
}
