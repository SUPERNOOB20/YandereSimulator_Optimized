﻿using System;
using UnityEngine;

// Token: 0x020004C2 RID: 1218
public class WorkbenchScript : MonoBehaviour
{
	// Token: 0x06001FE0 RID: 8160 RVA: 0x001C36BB File Offset: 0x001C18BB
	private void Start()
	{
		this.RemoveCheckmarks();
	}

	// Token: 0x06001FE1 RID: 8161 RVA: 0x001C36C4 File Offset: 0x001C18C4
	private void Update()
	{
		if (!this.Show)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0)
				{
					this.Prompt.Yandere.MainCamera.transform.position = new Vector3(26f, 5.55f, 5f);
					this.Prompt.Yandere.MainCamera.transform.eulerAngles = new Vector3(54f, 0f, 0f);
					this.Prompt.Yandere.transform.position = new Vector3(26f, 4f, 4f);
					this.Prompt.Yandere.MyController.enabled = false;
					this.Prompt.Yandere.RPGCamera.enabled = false;
					this.Prompt.Yandere.CanMove = false;
					this.WorkbenchWindow.gameObject.SetActive(true);
					this.PromptBar.Label[0].text = "Select";
					this.PromptBar.Label[1].text = "Exit";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;
					this.CheckInventory();
					this.Show = true;
					return;
				}
			}
		}
		else
		{
			if (this.MakeshiftKnife != null)
			{
				Debug.Log("Telling the damn knife to use gravity.");
				this.MakeshiftKnife.MyRigidbody.useGravity = true;
				this.MakeshiftKnife.MyRigidbody.isKinematic = false;
				this.MakeshiftKnife = null;
			}
			if (!this.CraftingSequence)
			{
				if (!this.ConfirmationWindow.activeInHierarchy)
				{
					if (this.InputManager.TappedUp)
					{
						this.Selection--;
						this.UpdateHighlight();
					}
					else if (this.InputManager.TappedDown)
					{
						this.Selection++;
						this.UpdateHighlight();
					}
					if (Input.GetButtonDown("A"))
					{
						if (this.InStock[this.Selection] && this.Label[this.Selection].alpha == 1f)
						{
							this.MaterialModel[this.Selection].SetActive(!this.MaterialModel[this.Selection].activeInHierarchy);
							this.Checkmark[this.Selection].SetActive(!this.Checkmark[this.Selection].activeInHierarchy);
							if (this.Checkmark[this.Selection].activeInHierarchy)
							{
								this.Material[this.Checkmarks + 1] = this.Selection;
							}
							else
							{
								this.Material[this.Checkmarks] = 0;
							}
							this.CountCheckmarks();
							this.PlayRandomSound();
							return;
						}
					}
					else
					{
						if (Input.GetButtonDown("B"))
						{
							this.WorkbenchWindow.gameObject.SetActive(false);
							this.Prompt.Yandere.MyController.enabled = true;
							this.Prompt.Yandere.RPGCamera.enabled = true;
							this.Prompt.Yandere.CanMove = true;
							this.PromptBar.ClearButtons();
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = false;
							this.RemoveCheckmarks();
							this.Material[1] = 0;
							this.Material[2] = 0;
							this.Material[3] = 0;
							this.Triple = false;
							this.Show = false;
							return;
						}
						if (Input.GetButtonDown("X") && this.PromptBar.Label[2].text != "")
						{
							this.PromptBar.Label[0].text = "Yes";
							this.PromptBar.Label[1].text = "No";
							this.PromptBar.Label[2].text = "";
							this.PromptBar.UpdateButtons();
							this.ConfirmationWindow.SetActive(true);
							this.ConfirmationLabel.text = "Combine these objects to make " + this.Outcome + "?";
							return;
						}
					}
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.ConfirmationWindow.SetActive(false);
						this.OutcomeModel[this.OutcomeID].transform.localScale = new Vector3(0f, 0f, 0f);
						this.OutcomeModel[this.OutcomeID].SetActive(true);
						this.OutcomeCamera.SetActive(true);
						this.CraftingSequence = true;
						this.PromptBar.Label[0].text = "Continue";
						this.PromptBar.Label[1].text = "";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;
						this.PlayRandomSound();
						return;
					}
					if (Input.GetButtonDown("B"))
					{
						this.ConfirmationWindow.SetActive(false);
						this.PromptBar.Label[0].text = "Select";
						this.PromptBar.Label[1].text = "Exit";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;
						return;
					}
				}
			}
			else if (!this.Return)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 360f, Time.deltaTime * 10f);
				this.OutcomeModel[this.OutcomeID].transform.localScale = Vector3.Lerp(this.OutcomeModel[this.OutcomeID].transform.localScale, Vector3.one, Time.deltaTime * 10f);
				this.OutcomeModel[this.OutcomeID].transform.localEulerAngles = new Vector3(0f, this.Rotation, 0f);
				this.Darkness.alpha = Mathf.Lerp(this.Darkness.alpha, 0.5f, Time.deltaTime * 10f);
				if (this.Darkness.alpha > 0.49f && Input.GetButtonDown("A"))
				{
					if (this.OutcomeID == 1)
					{
						this.Inventory.Ammonium = false;
						this.Inventory.Balloons = false;
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prompt.Yandere.PauseScreen.FavorMenu.DropsMenu.InfoChanWindow.Drops[13], this.Prompt.Yandere.transform.position + new Vector3(0f, 1f, 0.5f), Quaternion.identity);
						gameObject.GetComponent<Rigidbody>().useGravity = true;
						gameObject.GetComponent<Rigidbody>().isKinematic = false;
						gameObject.name = "Box of Stink Bombs";
					}
					else if (this.OutcomeID == 2)
					{
						this.Inventory.Hairpins = false;
						this.Inventory.PaperClips = false;
						this.Inventory.LockPick = true;
					}
					else if (this.OutcomeID == 3)
					{
						this.Inventory.SilverFulminate = false;
						this.Inventory.Paper = false;
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.Prompt.Yandere.PauseScreen.FavorMenu.DropsMenu.InfoChanWindow.Drops[12], this.Prompt.Yandere.transform.position + new Vector3(0f, 1f, 0.5f), Quaternion.identity);
						gameObject2.GetComponent<Rigidbody>().useGravity = true;
						gameObject2.GetComponent<Rigidbody>().isKinematic = false;
						gameObject2.name = "Box of Bang Snaps";
					}
					else if (this.OutcomeID == 4)
					{
						this.Inventory.Nails = false;
						this.Prompt.Yandere.EquippedWeapon.Nails.SetActive(true);
					}
					else if (this.OutcomeID == 5)
					{
						this.Inventory.Bandages = false;
						this.Inventory.WoodenSticks = false;
						this.Inventory.Glass = false;
						this.MakeshiftKnife = this.Prompt.Yandere.WeaponManager.Weapons[45];
						this.MakeshiftKnife.gameObject.SetActive(true);
						this.MakeshiftKnife.transform.position = this.Prompt.Yandere.transform.position + new Vector3(0f, 1f, 0.5f);
						this.MakeshiftKnife.Start();
						this.MakeshiftKnife.MyRigidbody.useGravity = true;
						this.MakeshiftKnife.MyRigidbody.isKinematic = false;
					}
					this.RemoveCheckmarks();
					this.CheckInventory();
					this.Return = true;
					return;
				}
			}
			else
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
				this.OutcomeModel[this.OutcomeID].transform.localScale = Vector3.Lerp(this.OutcomeModel[this.OutcomeID].transform.localScale, Vector3.zero, Time.deltaTime * 10f);
				this.OutcomeModel[this.OutcomeID].transform.localEulerAngles = new Vector3(0f, this.Rotation, 0f);
				this.Darkness.alpha = Mathf.MoveTowards(this.Darkness.alpha, 0f, Time.deltaTime);
				if (this.Darkness.alpha == 0f)
				{
					this.OutcomeModel[this.OutcomeID].transform.localScale = Vector3.zero;
					this.OutcomeModel[this.OutcomeID].SetActive(false);
					this.OutcomeCamera.SetActive(false);
					this.PromptBar.Label[0].text = "Select";
					this.PromptBar.Label[1].text = "Exit";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;
					this.CraftingSequence = false;
					this.Return = false;
				}
			}
		}
	}

	// Token: 0x06001FE2 RID: 8162 RVA: 0x001C4120 File Offset: 0x001C2320
	private void PlayRandomSound()
	{
		this.MyAudio.clip = this.SFX[UnityEngine.Random.Range(1, this.SFX.Length)];
		this.MyAudio.Play();
	}

	// Token: 0x06001FE3 RID: 8163 RVA: 0x001C4150 File Offset: 0x001C2350
	private void CheckInventory()
	{
		Debug.Log("The game is now checking what items are currently in Yandere-chan's inventory.");
		for (int i = 1; i < this.Checkmark.Length; i++)
		{
			this.Label[i].color = new Color(0f, 0f, 0f, 0.5f);
			this.Label[i].text = "?????";
			this.InStock[i] = false;
		}
		if (this.Inventory.Ammonium)
		{
			this.Label[1].text = "Ammonium";
			this.InStock[1] = true;
		}
		if (this.Inventory.Balloons)
		{
			this.Label[2].text = "Balloons";
			this.InStock[2] = true;
		}
		if (this.Inventory.Bandages)
		{
			this.Label[3].text = "Bandages";
			this.InStock[3] = true;
		}
		if (this.Inventory.Glass)
		{
			this.Label[4].text = "Glass Shards";
			this.InStock[4] = true;
		}
		if (this.Inventory.Hairpins)
		{
			this.Label[5].text = "Hair Pins";
			this.InStock[5] = true;
		}
		if (this.Inventory.Nails)
		{
			this.Label[6].text = "Nails";
			this.InStock[6] = true;
		}
		if (this.Inventory.Paper)
		{
			this.Label[7].text = "Paper";
			this.InStock[7] = true;
		}
		if (this.Inventory.PaperClips)
		{
			this.Label[8].text = "Paper Clips";
			this.InStock[8] = true;
		}
		if (this.Inventory.SilverFulminate)
		{
			this.Label[9].text = "Silver Fulminate";
			this.InStock[9] = true;
		}
		if (this.Inventory.WoodenSticks)
		{
			this.Label[10].text = "Wooden Sticks";
			this.InStock[10] = true;
		}
		if (this.Prompt.Yandere.Armed && this.Prompt.Yandere.EquippedWeapon.WeaponID == 9 && !this.Prompt.Yandere.EquippedWeapon.Nails.activeInHierarchy)
		{
			this.Label[11].text = "Baseball Bat";
			this.InStock[11] = true;
		}
		for (int i = 1; i < this.Checkmark.Length; i++)
		{
			if (this.Label[i].text != "?????")
			{
				this.Label[i].color = new Color(0f, 0f, 0f, 1f);
			}
		}
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x001C4408 File Offset: 0x001C2608
	private void UpdateHighlight()
	{
		if (this.Selection > 11)
		{
			this.Selection = 1;
		}
		else if (this.Selection < 1)
		{
			this.Selection = 11;
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 440f - 80f * (float)this.Selection, this.Highlight.localPosition.z);
	}

	// Token: 0x06001FE5 RID: 8165 RVA: 0x001C447C File Offset: 0x001C267C
	private void CountCheckmarks()
	{
		Debug.Log("The game is now counting how many checkmarks are currently displayed.");
		this.PromptBar.Label[2].text = "";
		this.Checkmarks = 0;
		this.Triple = false;
		for (int i = 1; i < this.Checkmark.Length; i++)
		{
			if (this.Checkmark[i].activeInHierarchy)
			{
				this.Checkmarks++;
				if (i == 3 || i == 4 || i == 10)
				{
					this.Triple = true;
				}
			}
		}
		if (!this.Triple && this.Checkmarks == 2)
		{
			this.PromptBar.Label[2].text = "Combine";
		}
		else if (this.Checkmarks == 3)
		{
			this.PromptBar.Label[2].text = "Combine";
		}
		this.PromptBar.UpdateButtons();
		this.DisableInvalidOptions();
	}

	// Token: 0x06001FE6 RID: 8166 RVA: 0x001C4558 File Offset: 0x001C2758
	private void RemoveCheckmarks()
	{
		for (int i = 1; i < this.Checkmark.Length; i++)
		{
			this.MaterialModel[i].SetActive(false);
			this.Checkmark[i].SetActive(false);
		}
		this.Checkmarks = 0;
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x001C459C File Offset: 0x001C279C
	private void DisableInvalidOptions()
	{
		Debug.Log("The player has picked a material, and the game is now disabling the materials that cannot be applied to that material.");
		for (int i = 1; i < this.Checkmark.Length; i++)
		{
			if (this.Checkmarks > 0)
			{
				this.Label[i].color = new Color(0f, 0f, 0f, 0.5f);
			}
			else
			{
				this.Label[i].color = new Color(0f, 0f, 0f, 1f);
			}
		}
		if (!this.Triple)
		{
			if (this.Material[1] == 1 || this.Material[1] == 2)
			{
				this.Label[1].color = new Color(0f, 0f, 0f, 1f);
				this.Label[2].color = new Color(0f, 0f, 0f, 1f);
				this.Outcome = "five stink bombs";
				this.OutcomeID = 1;
			}
			else if (this.Material[1] == 5 || this.Material[1] == 8)
			{
				this.Label[5].color = new Color(0f, 0f, 0f, 1f);
				this.Label[8].color = new Color(0f, 0f, 0f, 1f);
				this.Outcome = "a lockpick";
				this.OutcomeID = 2;
			}
			else if (this.Material[1] == 7 || this.Material[1] == 9)
			{
				this.Label[7].color = new Color(0f, 0f, 0f, 1f);
				this.Label[9].color = new Color(0f, 0f, 0f, 1f);
				this.Outcome = "five bang snaps";
				this.OutcomeID = 3;
			}
			else if (this.Material[1] == 6 || this.Material[1] == 11)
			{
				this.Label[11].color = new Color(0f, 0f, 0f, 1f);
				this.Label[6].color = new Color(0f, 0f, 0f, 1f);
				this.Outcome = "a spiked baseball bat";
				this.OutcomeID = 4;
			}
		}
		if (this.Triple && (this.Material[1] == 3 || this.Material[2] == 3 || this.Material[1] == 4 || this.Material[2] == 4 || this.Material[1] == 10 || this.Material[2] == 10))
		{
			this.Label[3].color = new Color(0f, 0f, 0f, 1f);
			this.Label[4].color = new Color(0f, 0f, 0f, 1f);
			this.Label[10].color = new Color(0f, 0f, 0f, 1f);
			this.Outcome = "a makeshift knife";
			this.OutcomeID = 5;
		}
		for (int i = 1; i < this.Checkmark.Length; i++)
		{
			if (this.Label[i].text == "?????")
			{
				this.Label[i].color = new Color(0f, 0f, 0f, 0.5f);
			}
		}
	}

	// Token: 0x040042E1 RID: 17121
	public InputManagerScript InputManager;

	// Token: 0x040042E2 RID: 17122
	public WeaponScript MakeshiftKnife;

	// Token: 0x040042E3 RID: 17123
	public InventoryScript Inventory;

	// Token: 0x040042E4 RID: 17124
	public PromptBarScript PromptBar;

	// Token: 0x040042E5 RID: 17125
	public PromptScript Prompt;

	// Token: 0x040042E6 RID: 17126
	public GameObject ConfirmationWindow;

	// Token: 0x040042E7 RID: 17127
	public GameObject OutcomeCamera;

	// Token: 0x040042E8 RID: 17128
	public Transform WorkbenchWindow;

	// Token: 0x040042E9 RID: 17129
	public Transform Highlight;

	// Token: 0x040042EA RID: 17130
	public UILabel ConfirmationLabel;

	// Token: 0x040042EB RID: 17131
	public AudioSource MyAudio;

	// Token: 0x040042EC RID: 17132
	public UISprite Darkness;

	// Token: 0x040042ED RID: 17133
	public GameObject[] MaterialModel;

	// Token: 0x040042EE RID: 17134
	public GameObject[] OutcomeModel;

	// Token: 0x040042EF RID: 17135
	public GameObject[] Checkmark;

	// Token: 0x040042F0 RID: 17136
	public AudioClip[] SFX;

	// Token: 0x040042F1 RID: 17137
	public UILabel[] Label;

	// Token: 0x040042F2 RID: 17138
	public bool[] InStock;

	// Token: 0x040042F3 RID: 17139
	public int[] Material;

	// Token: 0x040042F4 RID: 17140
	public bool CraftingSequence;

	// Token: 0x040042F5 RID: 17141
	public bool Triple;

	// Token: 0x040042F6 RID: 17142
	public bool Return;

	// Token: 0x040042F7 RID: 17143
	public bool Show;

	// Token: 0x040042F8 RID: 17144
	public string Outcome = "";

	// Token: 0x040042F9 RID: 17145
	public int Checkmarks;

	// Token: 0x040042FA RID: 17146
	public int Selection = 1;

	// Token: 0x040042FB RID: 17147
	public int OutcomeID = 1;

	// Token: 0x040042FC RID: 17148
	public float Rotation;
}
