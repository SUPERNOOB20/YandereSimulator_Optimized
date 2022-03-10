﻿using System;
using UnityEngine;

// Token: 0x02000253 RID: 595
public class CollectibleScript : MonoBehaviour
{
	// Token: 0x06001288 RID: 4744 RVA: 0x000935E0 File Offset: 0x000917E0
	private void Start()
	{
		if ((this.CollectibleType == CollectibleType.BasementTape && CollectibleGlobals.GetBasementTapeCollected(this.ID)) || (this.CollectibleType == CollectibleType.Manga && CollectibleGlobals.GetMangaCollected(this.ID)) || (this.CollectibleType == CollectibleType.Tape && CollectibleGlobals.GetTapeCollected(this.ID)) || (this.CollectibleType == CollectibleType.Panty && CollectibleGlobals.GetPantyPurchased(11)))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (GameGlobals.LoveSick || MissionModeGlobals.MissionMode || (GameGlobals.Eighties && this.CollectibleType == CollectibleType.Manga) || (GameGlobals.Eighties && this.CollectibleType == CollectibleType.Tape))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06001289 RID: 4745 RVA: 0x0009368C File Offset: 0x0009188C
	public CollectibleType CollectibleType
	{
		get
		{
			if (this.Name == "HeadmasterTape")
			{
				return CollectibleType.HeadmasterTape;
			}
			if (this.Name == "BasementTape")
			{
				return CollectibleType.BasementTape;
			}
			if (this.Name == "Manga")
			{
				return CollectibleType.Manga;
			}
			if (this.Name == "Tape")
			{
				return CollectibleType.Tape;
			}
			if (this.Type == 5)
			{
				return CollectibleType.Key;
			}
			if (this.Type == 6)
			{
				return CollectibleType.Panty;
			}
			Debug.LogError("Unrecognized collectible \"" + this.Name + "\".", base.gameObject);
			return CollectibleType.Tape;
		}
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x00093720 File Offset: 0x00091920
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.CollectibleType == CollectibleType.HeadmasterTape)
			{
				this.Prompt.Yandere.StudentManager.HeadmasterTapesCollected[this.ID] = true;
			}
			else if (this.CollectibleType == CollectibleType.BasementTape)
			{
				CollectibleGlobals.SetBasementTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Manga)
			{
				this.Prompt.Yandere.StudentManager.MangaCollected[this.ID] = true;
			}
			else if (this.CollectibleType == CollectibleType.Tape)
			{
				this.Prompt.Yandere.StudentManager.TapesCollected[this.ID] = true;
			}
			else if (this.CollectibleType == CollectibleType.Key)
			{
				this.Prompt.Yandere.Inventory.MysteriousKeys++;
			}
			else if (this.CollectibleType == CollectibleType.Panty)
			{
				this.Prompt.Yandere.StudentManager.PantiesCollected[11] = true;
				this.CountPanties();
			}
			else
			{
				Debug.LogError("Collectible \"" + this.Name + "\" not implemented.", base.gameObject);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x00093860 File Offset: 0x00091A60
	private void CountPanties()
	{
		int num = 1;
		for (int i = 1; i < 11; i++)
		{
			if (CollectibleGlobals.GetPantyPurchased(i))
			{
				num++;
			}
		}
		if (num == 10)
		{
			PlayerPrefs.SetInt("PantyQueen", 1);
		}
	}

	// Token: 0x0400183B RID: 6203
	public PromptScript Prompt;

	// Token: 0x0400183C RID: 6204
	public string Name = string.Empty;

	// Token: 0x0400183D RID: 6205
	public int Type;

	// Token: 0x0400183E RID: 6206
	public int ID;
}
