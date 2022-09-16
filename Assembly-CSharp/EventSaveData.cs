﻿// Decompiled with JetBrains decompiler
// Type: EventSaveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEBC9029-E754-4F76-ACC2-E5BB554B97F0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class EventSaveData
{
  public bool befriendConversation;
  public bool event1;
  public bool event2;
  public bool kidnapConversation;
  public bool livingRoom;

  public static EventSaveData ReadFromGlobals() => new EventSaveData()
  {
    befriendConversation = EventGlobals.BefriendConversation,
    event1 = EventGlobals.Event1,
    event2 = EventGlobals.Event2,
    kidnapConversation = EventGlobals.KidnapConversation,
    livingRoom = EventGlobals.LivingRoom
  };

  public static void WriteToGlobals(EventSaveData data)
  {
    EventGlobals.BefriendConversation = data.befriendConversation;
    EventGlobals.Event1 = data.event1;
    EventGlobals.Event2 = data.event2;
    EventGlobals.KidnapConversation = data.kidnapConversation;
    EventGlobals.LivingRoom = data.livingRoom;
  }
}
