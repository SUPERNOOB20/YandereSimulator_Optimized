﻿using System;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
public class SafeScript : MonoBehaviour
{
	// Token: 0x06001C0D RID: 7181 RVA: 0x00148499 File Offset: 0x00146699
	private void Start()
	{
		this.ContentsPrompt.MyCollider.enabled = false;
		this.SafePrompt.enabled = false;
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x001484B8 File Offset: 0x001466B8
	private void Update()
	{
		if (this.Key.activeInHierarchy && this.KeyPrompt.Circle[0].fillAmount == 0f)
		{
			this.KeyPrompt.Yandere.Inventory.SafeKey = true;
			this.SafePrompt.HideButton[0] = false;
			this.SafePrompt.enabled = true;
			this.Key.SetActive(false);
		}
		if (this.SafePrompt.Circle[0].fillAmount == 0f)
		{
			this.KeyPrompt.Yandere.Inventory.SafeKey = false;
			this.ContentsPrompt.MyCollider.enabled = true;
			this.Open = true;
			this.SafePrompt.Hide();
			this.SafePrompt.enabled = false;
		}
		if (this.ContentsPrompt.Circle[0].fillAmount == 0f)
		{
			this.MissionMode.DocumentsStolen = true;
			base.enabled = false;
			this.ContentsPrompt.Hide();
			this.ContentsPrompt.enabled = false;
			this.ContentsPrompt.gameObject.SetActive(false);
		}
		if (this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
			this.Door.localEulerAngles = new Vector3(this.Door.localEulerAngles.x, this.Rotation, this.Door.localEulerAngles.z);
			if (this.Rotation < 1f)
			{
				this.Open = false;
				return;
			}
		}
		else if (this.SafePrompt.Yandere.Inventory.LockPick)
		{
			this.SafePrompt.HideButton[2] = false;
			this.SafePrompt.enabled = true;
			if (this.SafePrompt.Circle[2].fillAmount == 0f)
			{
				this.KeyPrompt.Hide();
				this.KeyPrompt.enabled = false;
				this.SafePrompt.Yandere.Inventory.LockPick = false;
				this.SafePrompt.HideButton[2] = true;
				this.ContentsPrompt.MyCollider.enabled = true;
				this.Open = true;
				return;
			}
		}
		else if (!this.SafePrompt.HideButton[2])
		{
			this.SafePrompt.HideButton[2] = true;
		}
	}

	// Token: 0x04003167 RID: 12647
	public MissionModeScript MissionMode;

	// Token: 0x04003168 RID: 12648
	public PromptScript ContentsPrompt;

	// Token: 0x04003169 RID: 12649
	public PromptScript SafePrompt;

	// Token: 0x0400316A RID: 12650
	public PromptScript KeyPrompt;

	// Token: 0x0400316B RID: 12651
	public Transform Door;

	// Token: 0x0400316C RID: 12652
	public GameObject Key;

	// Token: 0x0400316D RID: 12653
	public float Rotation;

	// Token: 0x0400316E RID: 12654
	public bool Open;
}
