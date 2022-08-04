﻿// Decompiled with JetBrains decompiler
// Type: StudentCrusherScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF03FFAE-974C-4193-BB83-3E6945841C76
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class StudentCrusherScript : MonoBehaviour
{
  public MechaScript Mecha;

  private void OnTriggerEnter(Collider other)
  {
    if (other.transform.root.gameObject.layer != 9)
      return;
    StudentScript component = other.transform.root.gameObject.GetComponent<StudentScript>();
    if (!((Object) component != (Object) null) || component.StudentID <= 1)
      return;
    if ((double) this.Mecha.Speed > 0.89999997615814209)
    {
      Object.Instantiate<GameObject>(component.BloodyScream, this.transform.position + Vector3.up, Quaternion.identity);
      component.BecomeRagdoll();
    }
    if ((double) this.Mecha.Speed <= 5.0)
      return;
    component.Ragdoll.Dismember();
  }
}
