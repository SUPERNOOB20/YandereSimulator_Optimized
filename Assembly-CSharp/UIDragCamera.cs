﻿// Decompiled with JetBrains decompiler
// Type: UIDragCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEBC9029-E754-4F76-ACC2-E5BB554B97F0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera : MonoBehaviour
{
  public UIDraggableCamera draggableCamera;

  private void Awake()
  {
    if (!((Object) this.draggableCamera == (Object) null))
      return;
    this.draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(this.gameObject);
  }

  private void OnPress(bool isPressed)
  {
    if (!this.enabled || !NGUITools.GetActive(this.gameObject) || !((Object) this.draggableCamera != (Object) null) || !this.draggableCamera.enabled)
      return;
    this.draggableCamera.Press(isPressed);
  }

  private void OnDrag(Vector2 delta)
  {
    if (!this.enabled || !NGUITools.GetActive(this.gameObject) || !((Object) this.draggableCamera != (Object) null) || !this.draggableCamera.enabled)
      return;
    this.draggableCamera.Drag(delta);
  }

  private void OnScroll(float delta)
  {
    if (!this.enabled || !NGUITools.GetActive(this.gameObject) || !((Object) this.draggableCamera != (Object) null) || !this.draggableCamera.enabled)
      return;
    this.draggableCamera.Scroll(delta);
  }
}
