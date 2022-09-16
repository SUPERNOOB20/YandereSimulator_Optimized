﻿// Decompiled with JetBrains decompiler
// Type: ModelSwapScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEBC9029-E754-4F76-ACC2-E5BB554B97F0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

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
