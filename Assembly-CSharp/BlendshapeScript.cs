﻿// Decompiled with JetBrains decompiler
// Type: BlendshapeScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12831466-57D6-4F5A-B867-CD140BE439C0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class BlendshapeScript : MonoBehaviour
{
  public SkinnedMeshRenderer MyMesh;
  public float Happiness;
  public float Blink;

  private void LateUpdate()
  {
    this.Happiness += Time.deltaTime * 10f;
    this.MyMesh.SetBlendShapeWeight(0, this.Happiness);
    this.Blink += Time.deltaTime * 10f;
    this.MyMesh.SetBlendShapeWeight(8, 100f);
  }
}
