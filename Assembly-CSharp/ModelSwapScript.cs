﻿// Decompiled with JetBrains decompiler
// Type: ModelSwapScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC755693-C2BE-45B9-A389-81C492F832E2
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class ModelSwapScript : MonoBehaviour
{
  public Transform PelvisRoot;
  public GameObject Attachment;

  public void Update() => Input.GetKeyDown("z");

  public void Attach(GameObject Attachment, bool Inactives) => this.StartCoroutine(this.Attach_Threat(this.PelvisRoot, Attachment, Inactives));

  public IEnumerator Attach_Threat(
    Transform PelvisRoot,
    GameObject Attachment,
    bool Inactives)
  {
    Attachment.transform.SetParent(PelvisRoot);
    PelvisRoot.localEulerAngles = Vector3.zero;
    PelvisRoot.localPosition = Vector3.zero;
    Transform[] componentsInChildren = PelvisRoot.GetComponentsInChildren<Transform>(Inactives);
    foreach (Transform componentsInChild in Attachment.GetComponentsInChildren<Transform>(Inactives))
    {
      foreach (Transform p in componentsInChildren)
      {
        if (componentsInChild.name == p.name)
        {
          componentsInChild.SetParent(p);
          componentsInChild.localEulerAngles = Vector3.zero;
          componentsInChild.localPosition = Vector3.zero;
        }
      }
    }
    yield return (object) null;
  }
}
