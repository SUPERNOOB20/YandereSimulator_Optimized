﻿// Decompiled with JetBrains decompiler
// Type: MaidDereMinigame.SoundEmitter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12831466-57D6-4F5A-B867-CD140BE439C0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using MaidDereMinigame.Malee;
using System;
using UnityEngine;

namespace MaidDereMinigame
{
  [Serializable]
  public class SoundEmitter
  {
    public SFXController.Sounds sound;
    public bool interupt;
    [Reorderable]
    public AudioSources sources;
    [Reorderable]
    public AudioClips clips;

    public AudioSource GetSource()
    {
      for (int index = 0; index < this.sources.Count; ++index)
      {
        if (!this.sources[index].isPlaying)
          return this.sources[index];
      }
      return this.sources[0];
    }
  }
}
