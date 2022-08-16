﻿// Decompiled with JetBrains decompiler
// Type: TextMessageScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD17A22F-B301-43EA-811A-FA797D0BA442
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TextMessageScript : MonoBehaviour
{
  public UILabel Label;
  public GameObject Image;
  public bool Attachment;
  public bool Right;

  private void Start()
  {
    if (!this.Attachment && (Object) this.Image != (Object) null)
      this.Image.SetActive(false);
    if (!this.Right || !EventGlobals.OsanaConversation)
      return;
    this.gameObject.GetComponent<UISprite>().color = new Color(1f, 0.5f, 0.0f);
    this.Label.color = new Color(1f, 1f, 1f);
  }

  private void Update() => this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
}
