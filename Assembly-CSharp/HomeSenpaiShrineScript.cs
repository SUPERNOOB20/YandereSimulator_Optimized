﻿using System;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class HomeSenpaiShrineScript : MonoBehaviour
{
	// Token: 0x060018B6 RID: 6326 RVA: 0x000F3354 File Offset: 0x000F1554
	public void Start()
	{
		this.UpdateText(this.GetCurrentIndex());
		for (int i = 1; i < 11; i++)
		{
			if (PlayerGlobals.GetShrineCollectible(i))
			{
				this.Collectibles[i].SetActive(true);
			}
		}
	}

	// Token: 0x060018B7 RID: 6327 RVA: 0x000F3390 File Offset: 0x000F1590
	private bool InUpperHalf()
	{
		return this.Y < 2;
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x000F339B File Offset: 0x000F159B
	private int GetCurrentIndex()
	{
		if (this.InUpperHalf())
		{
			return this.Y;
		}
		return 2 + (this.X + (this.Y - 2) * this.Columns);
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x000F33C4 File Offset: 0x000F15C4
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.PauseScreen.Show)
		{
			if (this.HomeCamera.ID == 6)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 135f, Time.deltaTime * 10f);
				this.RightDoor.localEulerAngles = new Vector3(this.RightDoor.localEulerAngles.x, this.Rotation, this.RightDoor.localEulerAngles.z);
				this.LeftDoor.localEulerAngles = new Vector3(this.LeftDoor.localEulerAngles.x, -this.Rotation, this.LeftDoor.localEulerAngles.z);
				if (this.InputManager.TappedUp)
				{
					this.Y = ((this.Y > 0) ? (this.Y - 1) : (this.Rows - 1));
				}
				if (this.InputManager.TappedDown)
				{
					this.Y = ((this.Y < this.Rows - 1) ? (this.Y + 1) : 0);
				}
				if (this.InputManager.TappedRight && !this.InUpperHalf())
				{
					this.X = ((this.X < this.Columns - 1) ? (this.X + 1) : 0);
				}
				if (this.InputManager.TappedLeft && !this.InUpperHalf())
				{
					this.X = ((this.X > 0) ? (this.X - 1) : (this.Columns - 1));
				}
				if (this.InUpperHalf())
				{
					this.X = 1;
				}
				int currentIndex = this.GetCurrentIndex();
				this.HomeCamera.Destination = this.Destinations[currentIndex];
				this.HomeCamera.Target = this.Targets[currentIndex];
				if (this.InputManager.TappedUp || this.InputManager.TappedDown || this.InputManager.TappedRight || this.InputManager.TappedLeft)
				{
					this.UpdateText(currentIndex - 1);
				}
				if (Input.GetButtonDown("B"))
				{
					this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
					this.HomeCamera.Target = this.HomeCamera.Targets[0];
					this.HomeYandere.CanMove = true;
					this.HomeYandere.gameObject.SetActive(true);
					this.HomeWindow.Show = false;
				}
			}
		}
		else if (!this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
			this.RightDoor.localEulerAngles = new Vector3(this.RightDoor.localEulerAngles.x, this.Rotation, this.RightDoor.localEulerAngles.z);
			this.LeftDoor.localEulerAngles = new Vector3(this.LeftDoor.localEulerAngles.x, this.Rotation, this.LeftDoor.localEulerAngles.z);
		}
		if (this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 135f, Time.deltaTime * 10f);
			this.RightDoor.localEulerAngles = new Vector3(this.RightDoor.localEulerAngles.x, this.Rotation, this.RightDoor.localEulerAngles.z);
			this.LeftDoor.localEulerAngles = new Vector3(this.LeftDoor.localEulerAngles.x, -this.Rotation, this.LeftDoor.localEulerAngles.z);
		}
	}

	// Token: 0x060018BA RID: 6330 RVA: 0x000F3774 File Offset: 0x000F1974
	private void UpdateText(int newIndex)
	{
		if (newIndex == -1)
		{
			newIndex = 10;
		}
		if (newIndex == 0)
		{
			this.NameLabel.text = this.Names[newIndex];
			this.DescLabel.text = this.Descs[newIndex];
			return;
		}
		if (PlayerGlobals.GetShrineCollectible(newIndex))
		{
			this.NameLabel.text = this.Names[newIndex];
			this.DescLabel.text = this.Descs[newIndex];
			return;
		}
		this.NameLabel.text = "???";
		this.DescLabel.text = "I'd like to find something that Senpai touched and keep it here...";
	}

	// Token: 0x040025A2 RID: 9634
	public InputManagerScript InputManager;

	// Token: 0x040025A3 RID: 9635
	public PauseScreenScript PauseScreen;

	// Token: 0x040025A4 RID: 9636
	public HomeYandereScript HomeYandere;

	// Token: 0x040025A5 RID: 9637
	public HomeCameraScript HomeCamera;

	// Token: 0x040025A6 RID: 9638
	public HomeWindowScript HomeWindow;

	// Token: 0x040025A7 RID: 9639
	public GameObject[] Collectibles;

	// Token: 0x040025A8 RID: 9640
	public Transform[] Destinations;

	// Token: 0x040025A9 RID: 9641
	public Transform[] Targets;

	// Token: 0x040025AA RID: 9642
	public Transform RightDoor;

	// Token: 0x040025AB RID: 9643
	public Transform LeftDoor;

	// Token: 0x040025AC RID: 9644
	public UILabel NameLabel;

	// Token: 0x040025AD RID: 9645
	public UILabel DescLabel;

	// Token: 0x040025AE RID: 9646
	public string[] Names;

	// Token: 0x040025AF RID: 9647
	public string[] Descs;

	// Token: 0x040025B0 RID: 9648
	public float Rotation;

	// Token: 0x040025B1 RID: 9649
	private int Rows = 5;

	// Token: 0x040025B2 RID: 9650
	private int Columns = 3;

	// Token: 0x040025B3 RID: 9651
	private int X = 1;

	// Token: 0x040025B4 RID: 9652
	private int Y = 3;

	// Token: 0x040025B5 RID: 9653
	public bool Open;
}
