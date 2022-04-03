﻿using System;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class GenericRivalBagScript : MonoBehaviour
{
	// Token: 0x060014F0 RID: 5360 RVA: 0x000D137C File Offset: 0x000CF57C
	public void Start()
	{
		if (!this.Initialized)
		{
			this.GrabRivalInfo();
			this.Magazine.gameObject.SetActive(false);
			this.MagazineButton.SetActive(false);
			this.Magazine.localPosition = new Vector3(-700f, -1470f, 0f);
			this.Magazine.localEulerAngles = new Vector3(0f, 0f, 45f);
			base.gameObject.SetActive(false);
			this.Window.SetActive(false);
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			this.Initialized = true;
		}
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000D142C File Offset: 0x000CF62C
	private void Update()
	{
		if (!this.Window.activeInHierarchy)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Prompt.Yandere.StudentManager.CanAnyoneSeeYandere();
				if (!this.Prompt.Yandere.StudentManager.YandereVisible)
				{
					this.Prompt.Yandere.RPGCamera.enabled = false;
					this.Prompt.Yandere.CanMove = false;
					Time.timeScale = 0.0001f;
					this.Window.SetActive(true);
					this.Menu = 1;
					this.UpdateMenuLabels();
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Confirm";
					this.PromptBar.Label[4].text = "Change Selection";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;
					return;
				}
				this.Prompt.Yandere.NotificationManager.CustomText = "Can't do that! Someone is watching!";
				this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
				return;
			}
		}
		else
		{
			if (this.InputManager.TappedDown)
			{
				this.Selected++;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedUp)
			{
				this.Selected--;
				this.UpdateHighlight();
			}
			if (Input.GetButtonDown("A"))
			{
				if (this.Menu == 1)
				{
					if (this.Label[this.Selected].color.a == 1f)
					{
						if (this.Selected == 1)
						{
							this.Menu = 2;
							this.UpdateMenuLabels();
						}
						else if (this.Selected == 2)
						{
							this.Menu = 3;
							this.UpdateMenuLabels();
						}
						else if (this.Selected == 3)
						{
							this.Menu = 4;
							this.UpdateMenuLabels();
						}
						else if (this.Selected == 4)
						{
							this.CloseWindow();
						}
					}
				}
				else if (this.Menu == 2)
				{
					if (this.Label[this.Selected].color.a == 1f)
					{
						if (this.Selected == 1)
						{
							Debug.Log("The player has stolen the book that the rival borrowed from Senpai.");
							this.BorrowedBook = false;
							this.Event.Sabotage();
							ScheduleBlock scheduleBlock = this.Rival.ScheduleBlocks[4];
							scheduleBlock.destination = "Search Patrol";
							scheduleBlock.action = "Search Patrol";
							this.Rival.GetDestinations();
							Debug.Log("The rival's routine should now be adjusted to ''Search'' at lunchtime.");
							this.UpdateMenuLabels();
						}
						else if (this.Selected == 2)
						{
							this.MagazineButton.SetActive(true);
							this.Highlight.gameObject.SetActive(false);
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[1].text = "Back";
							this.PromptBar.UpdateButtons();
							this.Menu = 5;
							this.UpdateMenuLabels();
						}
						else if (this.Selected == 3)
						{
							this.Highlight.gameObject.SetActive(false);
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[1].text = "Back";
							this.PromptBar.UpdateButtons();
							this.Menu = 6;
							this.UpdateMenuLabels();
						}
						else if (this.Selected == 4)
						{
							this.Menu = 1;
							this.UpdateMenuLabels();
						}
					}
				}
				else if (this.Menu == 3)
				{
					if (this.Label[this.Selected].color.a == 1f)
					{
						if (this.Selected == 1)
						{
							if (this.Prompt.Yandere.Inventory.RatPoison)
							{
								this.Prompt.Yandere.Inventory.RatPoison = false;
							}
							else
							{
								this.Prompt.Yandere.Inventory.EmeticPoison = false;
							}
							this.Rival.MyBento.Tampered = true;
							this.Rival.MyBento.Emetic = true;
						}
						else if (this.Selected == 2)
						{
							this.Rival.MyBento.Tampered = true;
							this.Rival.MyBento.Lethal = true;
							if (this.Prompt.Yandere.Inventory.LethalPoison)
							{
								this.Prompt.Yandere.Inventory.LethalPoison = false;
							}
							else
							{
								this.Prompt.Yandere.Inventory.ChemicalPoison = false;
							}
							this.Prompt.Yandere.Inventory.LethalPoisons--;
						}
						else if (this.Selected == 3)
						{
							this.Rival.MyBento.Tampered = true;
							this.Rival.MyBento.Headache = true;
						}
						else if (this.Selected == 4)
						{
							if (this.Prompt.Yandere.Inventory.Tranquilizer)
							{
								this.Prompt.Yandere.Inventory.Tranquilizer = false;
							}
							else
							{
								this.Prompt.Yandere.Inventory.Sedative = false;
							}
							this.Rival.MyBento.Tampered = true;
							this.Rival.MyBento.Tranquil = true;
						}
						else if (this.Selected == 5)
						{
							this.Rival.ScheduleBlocks[4].action = "Shamed";
							this.Rival.GetDestinations();
							this.BentoStolen = true;
						}
						this.Menu = 1;
						this.UpdateMenuLabels();
					}
				}
				else if (this.Menu == 4)
				{
					if (this.Label[this.Selected].color.a == 1f)
					{
						if (this.Selected == 1)
						{
							this.Prompt.Yandere.Inventory.Sake = false;
							this.Alcohol = true;
						}
						else if (this.Selected == 2)
						{
							this.Prompt.Yandere.Inventory.Condoms = false;
							this.Condoms = true;
						}
						else if (this.Selected == 3)
						{
							this.Prompt.Yandere.Inventory.Cigs = false;
							this.Cigarettes = true;
						}
						else if (this.Selected == 4)
						{
							this.Prompt.Yandere.Inventory.Ring = false;
							this.StolenRing = true;
						}
						else if (this.Selected == 5)
						{
							this.Prompt.Yandere.Inventory.AnswerSheet = false;
							this.AnswerSheet = true;
						}
						else if (this.Selected == 6)
						{
							this.Prompt.Yandere.Inventory.Narcotics = false;
							this.Narcotics = true;
						}
						this.Menu = 1;
						this.UpdateMenuLabels();
					}
				}
				else if (this.Menu == 5)
				{
					this.PromptBar.Label[5].text = "Change Selection";
					this.PromptBar.UpdateButtons();
					this.Magazine.gameObject.SetActive(true);
					this.MagazineButton.SetActive(false);
					this.ShowMagazine = true;
					this.HideAllHearts();
					this.Page = 1;
					this.UpdateHearts();
				}
			}
			else if (Input.GetButtonDown("B"))
			{
				if (this.ShowMagazine)
				{
					this.PromptBar.Label[5].text = "";
					this.PromptBar.UpdateButtons();
					this.MagazineButton.SetActive(true);
					this.ShowMagazine = false;
				}
				else
				{
					this.MagazineButton.SetActive(false);
					this.Highlight.gameObject.SetActive(true);
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Confirm";
					this.PromptBar.Label[4].text = "Change Selection";
					this.PromptBar.UpdateButtons();
					if (this.Menu == 5)
					{
						this.Menu = 2;
						this.UpdateMenuLabels();
					}
					else if (this.Menu == 6)
					{
						this.Menu = 2;
						this.UpdateMenuLabels();
					}
				}
			}
			else if (this.Page == 1 && this.Prompt.Yandere.PauseScreen.InputManager.TappedRight)
			{
				this.Page++;
				this.HideAllHearts();
				this.UpdateHearts();
			}
			else if (this.Page == 2 && this.Prompt.Yandere.PauseScreen.InputManager.TappedLeft)
			{
				this.Page--;
				this.HideAllHearts();
				this.UpdateHearts();
			}
			if (this.ShowMagazine)
			{
				this.Magazine.localPosition = Vector3.Lerp(this.Magazine.localPosition, new Vector3(0f, 50f, 0f), Time.unscaledDeltaTime * 10f);
				this.Magazine.localEulerAngles = Vector3.Lerp(this.Magazine.localEulerAngles, new Vector3(0f, 0f, 0f), Time.unscaledDeltaTime * 10f);
				if (this.Page == 1)
				{
					this.Pivot.localEulerAngles = Vector3.Lerp(this.Pivot.localEulerAngles, new Vector3(0f, 0f, 0f), Time.unscaledDeltaTime * 36f);
					if (this.Pivot.localEulerAngles.y < 90f)
					{
						this.Pages[2].enabled = true;
					}
					float y = this.Pivot.localEulerAngles.y;
					return;
				}
				this.Pivot.localEulerAngles = Vector3.Lerp(this.Pivot.localEulerAngles, new Vector3(0f, 180f, 0f), Time.unscaledDeltaTime * 36f);
				if (this.Pivot.localEulerAngles.y > 90f)
				{
					this.Pages[2].enabled = false;
				}
				float y2 = this.Pivot.localEulerAngles.y;
				return;
			}
			else
			{
				this.Magazine.localPosition = Vector3.Lerp(this.Magazine.localPosition, new Vector3(-700f, -1470f, 0f), Time.unscaledDeltaTime * 10f);
				this.Magazine.localEulerAngles = Vector3.Lerp(this.Magazine.localEulerAngles, new Vector3(0f, 0f, 45f), Time.unscaledDeltaTime * 10f);
			}
		}
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x000D1EE0 File Offset: 0x000D00E0
	private void CloseWindow()
	{
		this.Selected = 1;
		this.UpdateHighlight();
		this.Prompt.Yandere.RPGCamera.enabled = true;
		this.Prompt.Yandere.CanMove = true;
		this.Window.SetActive(false);
		Time.timeScale = 1f;
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000D1F50 File Offset: 0x000D0150
	private void UpdateHighlight()
	{
		if (this.Selected > this.Limit)
		{
			this.Selected = 1;
		}
		else if (this.Selected < 1)
		{
			this.Selected = this.Limit;
		}
		this.Highlight.localPosition = new Vector3(0f, (float)(400 - this.Selected * 100), 0f);
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x000D1FB4 File Offset: 0x000D01B4
	private void UpdateMenuLabels()
	{
		this.Selected = 1;
		this.UpdateHighlight();
		for (int i = 1; i < this.Label.Length; i++)
		{
			this.Label[i].color = new Color(1f, 1f, 1f, 1f);
			this.Label[i].text = "";
		}
		this.Label[8].text = "";
		if (this.Menu == 1)
		{
			this.Label[1].text = "BOOKS";
			this.Label[2].text = "BENTO";
			this.Label[3].text = "CONTRABAND";
			this.Label[4].text = "EXIT";
			if (this.Rival == null)
			{
				this.GrabRivalInfo();
			}
			if (this.Rival.MyBento.Tampered || this.BentoStolen || this.NoBento)
			{
				this.Label[2].color = new Color(1f, 1f, 1f, 0.5f);
				if (this.BentoStolen)
				{
					this.Label[2].text = "BENTO (STOLEN)";
				}
				else if (this.NoBento)
				{
					this.Label[2].text = "NO BENTO";
				}
				else
				{
					this.Label[2].text = "BENTO (POISONED)";
				}
			}
			if (this.Prompt.Yandere.StudentManager.Clock.Period > 3)
			{
				this.Label[2].color = new Color(1f, 1f, 1f, 0.5f);
			}
			this.Limit = 4;
			return;
		}
		if (this.Menu == 2)
		{
			this.Label[1].text = "STEAL BORROWED BOOK";
			this.Label[2].text = "READ MAGAZINE";
			this.Label[3].text = "READ DIARY";
			this.Label[4].text = "BACK";
			if (!this.BorrowedBook || this.Prompt.Yandere.StudentManager.Clock.Period == 3)
			{
				this.Label[1].color = new Color(1f, 1f, 1f, 0.5f);
			}
			this.Limit = 4;
			return;
		}
		if (this.Menu == 3)
		{
			this.Label[1].text = "PUT EMETIC POISON IN BENTO";
			this.Label[2].text = "PUT LETHAL POISON IN BENTO";
			this.Label[3].text = "PUT HEADACHE POISON IN BENTO";
			this.Label[4].text = "PUT SEDATIVE POISON IN BENTO";
			this.Label[5].text = "STEAL BENTO";
			this.Label[6].text = "BACK";
			if (!this.Prompt.Yandere.Inventory.EmeticPoison && !this.Prompt.Yandere.Inventory.RatPoison)
			{
				this.Label[1].color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (this.Prompt.Yandere.Inventory.LethalPoisons == 0)
			{
				this.Label[2].color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (!this.Prompt.Yandere.Inventory.HeadachePoison)
			{
				this.Label[3].color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (!this.Prompt.Yandere.Inventory.Sedative && !this.Prompt.Yandere.Inventory.Tranquilizer)
			{
				this.Label[4].color = new Color(1f, 1f, 1f, 0.5f);
			}
			this.Limit = 6;
			return;
		}
		if (this.Menu == 4)
		{
			this.Label[1].text = "PUT ALCOHOL INTO BAG";
			this.Label[2].text = "PUT CONDOMS INTO BAG";
			this.Label[3].text = "PUT CIGARETTES INTO BAG";
			this.Label[4].text = "PUT STOLEN RING INTO BAG";
			this.Label[5].text = "PUT ANSWER SHEET INTO BAG";
			this.Label[6].text = "PUT NARCOTICS INTO BAG";
			this.Label[7].text = "BACK";
			if (!this.Prompt.Yandere.Inventory.Sake)
			{
				this.Label[1].color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (!this.Prompt.Yandere.Inventory.Condoms)
			{
				this.Label[2].color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (!this.Prompt.Yandere.Inventory.Cigs)
			{
				this.Label[3].color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (!this.Prompt.Yandere.Inventory.Ring)
			{
				this.Label[4].color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (!this.Prompt.Yandere.Inventory.AnswerSheet)
			{
				this.Label[5].color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (!this.Prompt.Yandere.Inventory.Narcotics)
			{
				this.Label[6].color = new Color(1f, 1f, 1f, 0.5f);
			}
			this.Limit = 7;
			return;
		}
		if (this.Menu == 5)
		{
			this.Label[8].text = "You find a magazine in your rival's bookbag.\n\nIt's a fashion magazine with lots of cute boys in it.\n\nYour rival has drawn hearts next to certain boys.\n\nYou could use this information to determine what traits she's attracted to.\n\nTo look inside the magazine, press:\n\n";
			return;
		}
		if (this.Menu == 6)
		{
			this.Label[8].text = string.Concat(new string[]
			{
				"You look through your rival's diary. \n\nShe likes ",
				this.RivalLikes[1],
				", ",
				this.RivalLikes[2],
				", ",
				this.RivalLikes[3],
				", ",
				this.RivalLikes[4],
				", and ",
				this.RivalLikes[5],
				".\n\nShe dislikes ",
				this.RivalDislikes[1],
				", ",
				this.RivalDislikes[2],
				", ",
				this.RivalDislikes[3],
				", ",
				this.RivalDislikes[4],
				", and ",
				this.RivalDislikes[5],
				".\n\nYou learn some embarassing secrets that will help you spread gossip about her.\n\nMost recent diary entry: \n\n''I can't sleep! I'm too worried about that EVIL PHOTOGRAPHER!''\n\nPerhaps you should befriend her and put a note in her locker to arrange a discussion about that topic."
			});
			this.Prompt.Yandere.Police.EndOfDay.LearnedAboutPhotographer = true;
			this.Prompt.Yandere.StudentManager.EmbarassingSecret = true;
		}
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x000D2700 File Offset: 0x000D0900
	public void GrabRivalInfo()
	{
		this.Rival = this.Prompt.Yandere.StudentManager.Students[this.Prompt.Yandere.StudentManager.RivalID];
		int week = DateGlobals.Week;
		if (week < 11)
		{
			if (week == 4)
			{
				this.NoBento = true;
			}
			int num = 10 + week;
			this.RivalOpinions = this.JSON.Topics[num].Topics;
			int i = 1;
			this.Likes = 0;
			this.Dislikes = 0;
			while (i < 26)
			{
				if (this.RivalOpinions[i] == 2)
				{
					this.RivalLikes[this.Likes] = this.Subjects[i];
					this.Likes++;
				}
				else if (this.RivalOpinions[i] == 1)
				{
					this.RivalDislikes[this.Dislikes] = this.Subjects[i];
					this.Dislikes++;
				}
				i++;
			}
			num -= 10;
			this.DesiredHairstyle = this.DesiredHairstyles[num];
			this.DesiredAccessory = this.DesiredAccessories[num];
			this.DesiredEyewear = this.DesiredEyewears[num];
			this.DesiredSkin = this.DesiredSkins[num];
			this.DesiredHairColor = this.DesiredHairColors[num];
			this.DesiredJewelry = this.DesiredJewelries[num];
			this.DesiredTrait = this.DesiredTraits[num];
		}
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x000D2854 File Offset: 0x000D0A54
	private void HideAllHearts()
	{
		this.PonytailHearts.SetActive(false);
		this.SlickHearts.SetActive(false);
		this.PiercingHearts.SetActive(false);
		this.BandanaHearts.SetActive(false);
		this.GlassesHearts.SetActive(false);
		this.SunglassesHearts.SetActive(false);
		this.TanHearts.SetActive(false);
		this.DarkHairHearts.SetActive(false);
		this.JewleryHearts.SetActive(false);
		this.CourageHearts.SetActive(false);
		this.SmartHearts.SetActive(false);
		this.StrongHearts.SetActive(false);
		this.QuoteBoxes.SetActive(false);
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x000D2900 File Offset: 0x000D0B00
	private void UpdateHearts()
	{
		if (this.Page == 1)
		{
			this.QuoteBoxes.SetActive(true);
			if (this.DesiredAccessory == 17)
			{
				this.PiercingHearts.SetActive(true);
			}
			else if (this.DesiredAccessory == 1)
			{
				this.BandanaHearts.SetActive(true);
			}
			if (this.DesiredEyewear == 6)
			{
				this.GlassesHearts.SetActive(true);
			}
			else if (this.DesiredEyewear == 3)
			{
				this.SunglassesHearts.SetActive(true);
			}
			if (this.DesiredSkin == 6)
			{
				this.TanHearts.SetActive(true);
			}
			if (this.DesiredHairColor == "SolidBlack")
			{
				this.DarkHairHearts.SetActive(true);
			}
			if (this.DesiredJewelry)
			{
				this.JewleryHearts.SetActive(true);
			}
			if (this.DesiredTrait == 1)
			{
				this.CourageHearts.SetActive(true);
				return;
			}
			if (this.DesiredTrait == 2)
			{
				this.SmartHearts.SetActive(true);
				return;
			}
			if (this.DesiredTrait == 3)
			{
				this.StrongHearts.SetActive(true);
				return;
			}
		}
		else
		{
			if (this.DesiredHairstyle == 55)
			{
				this.PonytailHearts.SetActive(true);
				return;
			}
			if (this.DesiredHairstyle == 56)
			{
				this.SlickHearts.SetActive(true);
			}
		}
	}

	// Token: 0x0400213B RID: 8507
	public InputManagerScript InputManager;

	// Token: 0x0400213C RID: 8508
	public GenericRivalEventScript Event;

	// Token: 0x0400213D RID: 8509
	public PromptBarScript PromptBar;

	// Token: 0x0400213E RID: 8510
	public StudentScript Rival;

	// Token: 0x0400213F RID: 8511
	public PromptScript Prompt;

	// Token: 0x04002140 RID: 8512
	public JsonScript JSON;

	// Token: 0x04002141 RID: 8513
	public Transform Highlight;

	// Token: 0x04002142 RID: 8514
	public GameObject Window;

	// Token: 0x04002143 RID: 8515
	public UILabel[] Label;

	// Token: 0x04002144 RID: 8516
	public bool BorrowedBook;

	// Token: 0x04002145 RID: 8517
	public bool Alcohol;

	// Token: 0x04002146 RID: 8518
	public bool Condoms;

	// Token: 0x04002147 RID: 8519
	public bool Cigarettes;

	// Token: 0x04002148 RID: 8520
	public bool StolenRing;

	// Token: 0x04002149 RID: 8521
	public bool AnswerSheet;

	// Token: 0x0400214A RID: 8522
	public bool Narcotics;

	// Token: 0x0400214B RID: 8523
	public bool ShowMagazine;

	// Token: 0x0400214C RID: 8524
	public bool BentoStolen;

	// Token: 0x0400214D RID: 8525
	public bool NoBento;

	// Token: 0x0400214E RID: 8526
	public int Selected;

	// Token: 0x0400214F RID: 8527
	public int Limit;

	// Token: 0x04002150 RID: 8528
	public int Menu;

	// Token: 0x04002151 RID: 8529
	public int[] RivalOpinions;

	// Token: 0x04002152 RID: 8530
	public string[] Subjects;

	// Token: 0x04002153 RID: 8531
	public string[] RivalDislikes;

	// Token: 0x04002154 RID: 8532
	public string[] RivalLikes;

	// Token: 0x04002155 RID: 8533
	public int Dislikes = 1;

	// Token: 0x04002156 RID: 8534
	public int Likes = 1;

	// Token: 0x04002157 RID: 8535
	public int[] DesiredHairstyles;

	// Token: 0x04002158 RID: 8536
	public int[] DesiredAccessories;

	// Token: 0x04002159 RID: 8537
	public int[] DesiredEyewears;

	// Token: 0x0400215A RID: 8538
	public int[] DesiredSkins;

	// Token: 0x0400215B RID: 8539
	public string[] DesiredHairColors;

	// Token: 0x0400215C RID: 8540
	public bool[] DesiredJewelries;

	// Token: 0x0400215D RID: 8541
	public int[] DesiredTraits;

	// Token: 0x0400215E RID: 8542
	public int DesiredHairstyle;

	// Token: 0x0400215F RID: 8543
	public int DesiredAccessory;

	// Token: 0x04002160 RID: 8544
	public int DesiredEyewear;

	// Token: 0x04002161 RID: 8545
	public int DesiredSkin;

	// Token: 0x04002162 RID: 8546
	public string DesiredHairColor;

	// Token: 0x04002163 RID: 8547
	public bool DesiredJewelry;

	// Token: 0x04002164 RID: 8548
	public int DesiredTrait;

	// Token: 0x04002165 RID: 8549
	public string DesiredHairstyleText;

	// Token: 0x04002166 RID: 8550
	public string DesiredAccessoryText;

	// Token: 0x04002167 RID: 8551
	public string DesiredEyewearText;

	// Token: 0x04002168 RID: 8552
	public string DesiredSkinText;

	// Token: 0x04002169 RID: 8553
	public string DesiredHairColorText;

	// Token: 0x0400216A RID: 8554
	public string DesiredJewelryText;

	// Token: 0x0400216B RID: 8555
	public string DesiredTraitText;

	// Token: 0x0400216C RID: 8556
	public bool Initialized;

	// Token: 0x0400216D RID: 8557
	public GameObject MagazineButton;

	// Token: 0x0400216E RID: 8558
	public Transform Magazine;

	// Token: 0x0400216F RID: 8559
	public int Page = 1;

	// Token: 0x04002170 RID: 8560
	public GameObject TanHearts;

	// Token: 0x04002171 RID: 8561
	public GameObject PiercingHearts;

	// Token: 0x04002172 RID: 8562
	public GameObject DarkHairHearts;

	// Token: 0x04002173 RID: 8563
	public GameObject GlassesHearts;

	// Token: 0x04002174 RID: 8564
	public GameObject BandanaHearts;

	// Token: 0x04002175 RID: 8565
	public GameObject JewleryHearts;

	// Token: 0x04002176 RID: 8566
	public GameObject SunglassesHearts;

	// Token: 0x04002177 RID: 8567
	public GameObject PonytailHearts;

	// Token: 0x04002178 RID: 8568
	public GameObject SlickHearts;

	// Token: 0x04002179 RID: 8569
	public GameObject CourageHearts;

	// Token: 0x0400217A RID: 8570
	public GameObject SmartHearts;

	// Token: 0x0400217B RID: 8571
	public GameObject StrongHearts;

	// Token: 0x0400217C RID: 8572
	public GameObject QuoteBoxes;

	// Token: 0x0400217D RID: 8573
	public Transform Pivot;

	// Token: 0x0400217E RID: 8574
	public UITexture[] Pages;
}
