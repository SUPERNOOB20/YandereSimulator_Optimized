﻿// Decompiled with JetBrains decompiler
// Type: CameraDepthTextureMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC755693-C2BE-45B9-A389-81C492F832E2
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CameraDepthTextureMode : MonoBehaviour
{
  [SerializeField]
  private DepthTextureMode depthTextureMode;

  private void OnValidate() => this.SetCameraDepthTextureMode();

  private void Awake() => this.SetCameraDepthTextureMode();

  private void SetCameraDepthTextureMode() => this.GetComponent<Camera>().depthTextureMode = this.depthTextureMode;
}
