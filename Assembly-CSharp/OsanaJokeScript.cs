﻿using System;
using UnityEngine;

// Token: 0x02000384 RID: 900
public class OsanaJokeScript : MonoBehaviour
{
	// Token: 0x06001A20 RID: 6688 RVA: 0x001141B8 File Offset: 0x001123B8
	private void Update()
	{
		if (this.Advance)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 14f)
			{
				Application.Quit();
				return;
			}
			if (this.Timer > 3f)
			{
				this.Label.text = "Congratulations! You have beaten Yandere Simulator!";
				if (!this.Jukebox.isPlaying)
				{
					this.Jukebox.clip = this.VictoryMusic;
					this.Jukebox.Play();
					return;
				}
			}
		}
		else if (Input.GetKeyDown("f"))
		{
			this.Rotation[0].enabled = false;
			this.Rotation[1].enabled = false;
			this.Rotation[2].enabled = false;
			this.Rotation[3].enabled = false;
			this.Rotation[4].enabled = false;
			this.Rotation[5].enabled = false;
			this.Rotation[6].enabled = false;
			this.Rotation[7].enabled = false;
			UnityEngine.Object.Instantiate<GameObject>(this.BloodSplatterEffect, this.Head.position, Quaternion.identity);
			this.Head.localScale = new Vector3(0f, 0f, 0f);
			this.Jukebox.clip = this.BloodSplatterSFX;
			this.Jukebox.Play();
			this.Label.text = "";
			this.Advance = true;
		}
	}

	// Token: 0x04002A9D RID: 10909
	public ConstantRandomRotation[] Rotation;

	// Token: 0x04002A9E RID: 10910
	public GameObject BloodSplatterEffect;

	// Token: 0x04002A9F RID: 10911
	public AudioClip BloodSplatterSFX;

	// Token: 0x04002AA0 RID: 10912
	public AudioClip VictoryMusic;

	// Token: 0x04002AA1 RID: 10913
	public AudioSource Jukebox;

	// Token: 0x04002AA2 RID: 10914
	public Transform Head;

	// Token: 0x04002AA3 RID: 10915
	public UILabel Label;

	// Token: 0x04002AA4 RID: 10916
	public bool Advance;

	// Token: 0x04002AA5 RID: 10917
	public float Timer;

	// Token: 0x04002AA6 RID: 10918
	public int ID;
}
