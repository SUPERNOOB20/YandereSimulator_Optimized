﻿using System;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class HomeExitScript : MonoBehaviour
{
	// Token: 0x0600187D RID: 6269 RVA: 0x000ED3E8 File Offset: 0x000EB5E8
	private void Start()
	{
		UILabel uilabel = this.Labels[1];
		if (HomeGlobals.Night)
		{
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
			if (SchemeGlobals.GetSchemeStage(6) == 9 && !StudentGlobals.GetStudentDead(10 + DateGlobals.Week) && !StudentGlobals.GetStudentKidnapped(10 + DateGlobals.Week) && GameGlobals.RivalEliminationID == 0)
			{
				UILabel uilabel2 = this.Labels[4];
				uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, 1f);
				uilabel2.text = "Stalker's House";
				if (GameGlobals.Eighties)
				{
					this.Labels[4].text = "Insane Asylum";
				}
			}
			this.BringItemPrompt.SetActive(false);
			return;
		}
		if (DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
		}
	}

	// Token: 0x0600187E RID: 6270 RVA: 0x000ED510 File Offset: 0x000EB710
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.HomeDarkness.FadeOut && this.HomeWindow.Sprite.color.a > 0.9f)
		{
			if (this.InputManager.TappedDown)
			{
				this.ID++;
				if (this.ID > 4)
				{
					this.ID = 1;
				}
				this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 50f - (float)this.ID * 50f, this.Highlight.localPosition.z);
			}
			if (this.InputManager.TappedUp)
			{
				this.ID--;
				if (this.ID < 1)
				{
					this.ID = 4;
				}
				this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 50f - (float)this.ID * 50f, this.Highlight.localPosition.z);
			}
			if (Input.GetButtonDown("A") && this.Labels[this.ID].color.a == 1f)
			{
				if (this.ID == 1)
				{
					this.HomeBringItem.HomeWindow.Show = true;
					this.HomeWindow.Show = false;
				}
				else
				{
					if (this.ID == 2)
					{
						this.HomeDarkness.Sprite.color = new Color(1f, 1f, 1f, 0f);
					}
					else if (this.ID == 3)
					{
						this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
					}
					else if (this.ID == 4)
					{
						this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
					}
					this.HomeDarkness.FadeOut = true;
					this.HomeWindow.Show = false;
					base.enabled = false;
				}
			}
			if (Input.GetButtonDown("B"))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeYandere.CanMove = true;
				this.HomeWindow.Show = false;
				base.enabled = false;
			}
		}
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x000ED7A8 File Offset: 0x000EB9A8
	public void GoToSchool()
	{
		if (SchoolGlobals.SchoolAtmosphere < 0.5f || GameGlobals.LoveSick)
		{
			this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
		}
		else
		{
			this.HomeDarkness.Sprite.color = new Color(1f, 1f, 1f, 0f);
		}
		this.HomeDarkness.FadeOut = true;
		this.HomeWindow.Show = false;
		base.enabled = false;
	}

	// Token: 0x04002482 RID: 9346
	public InputManagerScript InputManager;

	// Token: 0x04002483 RID: 9347
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04002484 RID: 9348
	public HomeYandereScript HomeYandere;

	// Token: 0x04002485 RID: 9349
	public BringItemScript HomeBringItem;

	// Token: 0x04002486 RID: 9350
	public HomeCameraScript HomeCamera;

	// Token: 0x04002487 RID: 9351
	public HomeWindowScript HomeWindow;

	// Token: 0x04002488 RID: 9352
	public GameObject BringItemPrompt;

	// Token: 0x04002489 RID: 9353
	public Transform Highlight;

	// Token: 0x0400248A RID: 9354
	public UILabel[] Labels;

	// Token: 0x0400248B RID: 9355
	public int ID = 1;
}
