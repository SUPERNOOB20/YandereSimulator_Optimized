﻿using System;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class PhoneMinigameScript : MonoBehaviour
{
	// Token: 0x06001A9F RID: 6815 RVA: 0x0011E7D4 File Offset: 0x0011C9D4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.CameraEffects.UpdateDOF(0.23f);
			this.Prompt.Yandere.MainCamera.GetComponent<AudioListener>().enabled = true;
			this.Prompt.Yandere.Pickpocketing = true;
			this.Prompt.Yandere.CanMove = false;
			this.Prompt.Yandere.MainCamera.transform.eulerAngles = new Vector3(45f, 180f, 0f);
			this.Prompt.Yandere.MainCamera.transform.position = new Vector3(0.4f, 12.66666f, -29.2f);
			this.Prompt.Yandere.RPGCamera.enabled = false;
			this.SmartPhoneScreen = this.Event.Rival.SmartPhoneScreen;
			this.Smartphone = this.Event.Rival.SmartPhone.transform;
			this.PickpocketMinigame.StartingAlerts = this.Prompt.Yandere.Alerts;
			this.PickpocketMinigame.PickpocketSpot = null;
			this.PickpocketMinigame.Sabotage = true;
			this.PickpocketMinigame.Show = true;
			this.OriginalRotation = this.Smartphone.eulerAngles;
			this.OriginalPosition = this.Smartphone.position;
			this.Tampering = true;
		}
		if (this.Tampering)
		{
			this.Prompt.Yandere.MoveTowardsTarget(new Vector3(0f, 12f, -28.66666f));
			if (!this.PickpocketMinigame.Failure)
			{
				if (this.PickpocketMinigame.Progress == 1)
				{
					this.Smartphone.position = Vector3.Lerp(this.Smartphone.position, new Vector3(0.4f, this.Smartphone.position.y, this.Smartphone.position.z), Time.deltaTime * 10f);
					return;
				}
				if (this.PickpocketMinigame.Progress == 2)
				{
					this.Smartphone.eulerAngles = Vector3.Lerp(this.Smartphone.eulerAngles, new Vector3(0f, 180f, 0f), Time.deltaTime * 10f);
					return;
				}
				if (this.PickpocketMinigame.Progress == 3)
				{
					this.SmartPhoneScreen.material.mainTexture = this.AlarmOff;
					return;
				}
				if (this.PickpocketMinigame.Progress == 4)
				{
					this.Smartphone.eulerAngles = Vector3.Lerp(this.Smartphone.eulerAngles, new Vector3(this.OriginalRotation.x, this.OriginalRotation.y, this.OriginalRotation.z), Time.deltaTime * 10f);
					return;
				}
				if (!this.PickpocketMinigame.Show)
				{
					this.Smartphone.position = Vector3.Lerp(this.Smartphone.position, new Vector3(this.OriginalPosition.x, this.OriginalPosition.y, this.OriginalPosition.z), Time.deltaTime * 10f);
					this.Timer += Time.deltaTime;
					if ((double)this.Timer > 1.0)
					{
						this.Event.Sabotaged = true;
						this.End();
						return;
					}
				}
			}
			else
			{
				this.Prompt.Yandere.transform.position = new Vector3(0f, 12f, -28.5f);
				this.Prompt.Yandere.TheftTimer = 1f;
				this.Event.EndEvent();
				this.Event.Rival.transform.position = new Vector3(0f, 12f, -29.2f);
				this.Event.Rival.YandereVisible = true;
				this.Event.Rival.Distracted = false;
				this.Event.Rival.Alarm = 200f;
				this.End();
			}
		}
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x0011EC0C File Offset: 0x0011CE0C
	private void End()
	{
		this.Prompt.Yandere.CameraEffects.UpdateDOF(2f);
		this.Prompt.Yandere.MainCamera.GetComponent<AudioListener>().enabled = false;
		this.Prompt.Yandere.RPGCamera.enabled = true;
		this.Prompt.Yandere.gameObject.SetActive(true);
		this.Prompt.Yandere.Pickpocketing = false;
		this.Prompt.Yandere.CanMove = true;
		this.Prompt.Yandere.Caught = false;
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Tampering = false;
		base.gameObject.SetActive(false);
	}

	// Token: 0x04002C3A RID: 11322
	public PickpocketMinigameScript PickpocketMinigame;

	// Token: 0x04002C3B RID: 11323
	public OsanaThursdayAfterClassEventScript Event;

	// Token: 0x04002C3C RID: 11324
	public Renderer SmartPhoneScreen;

	// Token: 0x04002C3D RID: 11325
	public Transform Smartphone;

	// Token: 0x04002C3E RID: 11326
	public PromptScript Prompt;

	// Token: 0x04002C3F RID: 11327
	public Texture AlarmOff;

	// Token: 0x04002C40 RID: 11328
	public bool Tampering;

	// Token: 0x04002C41 RID: 11329
	public float Timer;

	// Token: 0x04002C42 RID: 11330
	public Vector3 OriginalPosition;

	// Token: 0x04002C43 RID: 11331
	public Vector3 OriginalRotation;
}
