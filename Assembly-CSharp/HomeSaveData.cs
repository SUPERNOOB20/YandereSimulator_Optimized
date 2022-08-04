﻿// Decompiled with JetBrains decompiler
// Type: HomeSaveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF03FFAE-974C-4193-BB83-3E6945841C76
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class HomeSaveData
{
  public bool lateForSchool;
  public bool night;
  public bool startInBasement;

  public static HomeSaveData ReadFromGlobals() => new HomeSaveData()
  {
    lateForSchool = HomeGlobals.LateForSchool,
    night = HomeGlobals.Night,
    startInBasement = HomeGlobals.StartInBasement
  };

  public static void WriteToGlobals(HomeSaveData data)
  {
    HomeGlobals.LateForSchool = data.lateForSchool;
    HomeGlobals.Night = data.night;
    HomeGlobals.StartInBasement = data.startInBasement;
  }
}
