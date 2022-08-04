﻿// Decompiled with JetBrains decompiler
// Type: SpinWithMouse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF03FFAE-974C-4193-BB83-3E6945841C76
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
  public Transform target;
  public float speed = 1f;
  private Transform mTrans;

  private void Start() => this.mTrans = this.transform;

  private void OnDrag(Vector2 delta)
  {
    UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
    if ((Object) this.target != (Object) null)
      this.target.localRotation = Quaternion.Euler(0.0f, -0.5f * delta.x * this.speed, 0.0f) * this.target.localRotation;
    else
      this.mTrans.localRotation = Quaternion.Euler(0.0f, -0.5f * delta.x * this.speed, 0.0f) * this.mTrans.localRotation;
  }
}
