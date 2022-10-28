﻿// Decompiled with JetBrains decompiler
// Type: YanvaniaJarShardScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC755693-C2BE-45B9-A389-81C492F832E2
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class YanvaniaJarShardScript : MonoBehaviour
{
  public float MyRotation;
  public float Rotation;

  private void Start()
  {
    this.Rotation = Random.Range(-360f, 360f);
    this.GetComponent<Rigidbody>().AddForce(Random.Range(-100f, 100f), Random.Range(0.0f, 100f), Random.Range(-100f, 100f));
  }

  private void Update()
  {
    this.MyRotation += this.Rotation;
    this.transform.eulerAngles = new Vector3(this.MyRotation, this.MyRotation, this.MyRotation);
    if ((double) this.transform.position.y >= 6.5)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
