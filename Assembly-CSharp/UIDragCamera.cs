﻿// Decompiled with JetBrains decompiler
// Type: UIDragCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD17A22F-B301-43EA-811A-FA797D0BA442
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
