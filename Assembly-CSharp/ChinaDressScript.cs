﻿using System;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class ChinaDressScript : MonoBehaviour
{
	// Token: 0x0600124B RID: 4683 RVA: 0x0008CA74 File Offset: 0x0008AC74
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.WearChinaDress();
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
		}
	}

	// Token: 0x04001720 RID: 5920
	public PromptScript Prompt;
}
