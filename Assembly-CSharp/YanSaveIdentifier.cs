﻿// Decompiled with JetBrains decompiler
// Type: YanSaveIdentifier
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC755693-C2BE-45B9-A389-81C492F832E2
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class YanSaveIdentifier : MonoBehaviour
{
  public string ObjectID;
  [HideInInspector]
  public bool AutoGenerated;
  [HideInInspector]
  public List<Component> EnabledComponents = new List<Component>();
  [HideInInspector]
  public List<DisabledYanSaveMember> DisabledProperties = new List<DisabledYanSaveMember>();
  [HideInInspector]
  public List<DisabledYanSaveMember> DisabledFields = new List<DisabledYanSaveMember>();
  [HideInInspector]
  public bool InitializedInspector;
  private static List<YanSaveIdentifier> Identifiers = new List<YanSaveIdentifier>();

  public static GameObject GetObject(string id)
  {
    foreach (YanSaveIdentifier identifier in YanSaveIdentifier.Identifiers)
    {
      if (identifier.ObjectID == id)
        return identifier.gameObject;
    }
    return (GameObject) null;
  }

  public static GameObject GetObject(SerializedGameObject serializedGameObject)
  {
    foreach (YanSaveIdentifier identifier in YanSaveIdentifier.Identifiers)
    {
      if (identifier.ObjectID == serializedGameObject.ObjectID)
        return identifier.gameObject;
    }
    return (GameObject) null;
  }

  public void OnEnable()
  {
    if (!YanSaveIdentifier.Identifiers.Contains(this))
      YanSaveIdentifier.Identifiers.Add(this);
    if (!string.IsNullOrEmpty(this.ObjectID))
      return;
    this.ObjectID = this.gameObject.name;
  }

  public void OnDestroy() => YanSaveIdentifier.Identifiers.Remove(this);
}
