﻿using System;
using UnityEngine;

// Token: 0x02000501 RID: 1281
public class RivalBarScript : MonoBehaviour
{
	// Token: 0x06002124 RID: 8484 RVA: 0x001E5D3C File Offset: 0x001E3F3C
	private void Start()
	{
		for (int i = 1; i < 11; i++)
		{
			this.Bars[i].transform.localScale = new Vector3(1f, 0f, 1f);
		}
	}

	// Token: 0x06002125 RID: 8485 RVA: 0x001E5D7C File Offset: 0x001E3F7C
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.UpdateBars();
		}
		this.Timer += Time.deltaTime;
		if (this.ID < 11)
		{
			if (this.Timer > 1f)
			{
				this.UpdateBars();
				this.Timer = 0f;
			}
		}
		else if (this.Timer > 2.5f)
		{
			this.UpdateBars();
			this.Timer = 0f;
		}
		for (int i = 1; i < this.ID; i++)
		{
			this.Bars[i].transform.localScale = Vector3.Lerp(this.Bars[i].transform.localScale, new Vector3(1f, this.TargetHeights[i], 1f), Time.deltaTime * this.Speed);
			this.Bars[i].color = new Color(this.TargetHeights[i] / 7f, 1f - this.TargetHeights[i] / 7f, 0f);
			if (i == 1)
			{
				Debug.Log("R is: " + (this.TargetHeights[i] / 7f).ToString() + " G is: " + (1f - this.TargetHeights[i] / 7f).ToString());
			}
		}
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x001E5EDC File Offset: 0x001E40DC
	private void UpdateBars()
	{
		int i = 1;
		if (this.ID < 11)
		{
			this.ID++;
			return;
		}
		while (i < this.ID)
		{
			this.TargetHeights[i] = UnityEngine.Random.Range(0.7f, 7f);
			i++;
		}
	}

	// Token: 0x040048BE RID: 18622
	public int ID;

	// Token: 0x040048BF RID: 18623
	public float Speed;

	// Token: 0x040048C0 RID: 18624
	public float Timer;

	// Token: 0x040048C1 RID: 18625
	public UISprite[] Bars;

	// Token: 0x040048C2 RID: 18626
	public float[] TargetHeights;
}
