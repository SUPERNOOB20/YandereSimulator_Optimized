﻿// Decompiled with JetBrains decompiler
// Type: opencloseDoor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF03FFAE-974C-4193-BB83-3E6945841C76
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class opencloseDoor : MonoBehaviour
{
  public Animator openandclose;
  public bool open;
  public Transform Player;

  private void Start() => this.open = false;

  private void OnMouseOver()
  {
    if (!(bool) (Object) this.Player || (double) Vector3.Distance(this.Player.position, this.transform.position) >= 15.0)
      return;
    if (!this.open)
    {
      if (!Input.GetMouseButtonDown(0))
        return;
      this.StartCoroutine(this.opening());
    }
    else
    {
      if (!this.open || !Input.GetMouseButtonDown(0))
        return;
      this.StartCoroutine(this.closing());
    }
  }

  private IEnumerator opening()
  {
    MonoBehaviour.print((object) "you are opening the door");
    this.openandclose.Play("Opening");
    this.open = true;
    yield return (object) new WaitForSeconds(0.5f);
  }

  private IEnumerator closing()
  {
    MonoBehaviour.print((object) "you are closing the door");
    this.openandclose.Play("Closing");
    this.open = false;
    yield return (object) new WaitForSeconds(0.5f);
  }
}
