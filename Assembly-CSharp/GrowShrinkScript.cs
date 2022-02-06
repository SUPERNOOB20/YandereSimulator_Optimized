﻿using System;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class GrowShrinkScript : MonoBehaviour
{
	// Token: 0x06001823 RID: 6179 RVA: 0x000E4C14 File Offset: 0x000E2E14
	private void Start()
	{
		this.OriginalPosition = base.transform.localPosition;
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x000E4C38 File Offset: 0x000E2E38
	private void Update()
	{
		this.Timer += Time.deltaTime * 2f;
		this.Scale += Time.deltaTime * (this.Strength * this.Speed) * 2f;
		if (!this.Shrink)
		{
			this.Strength += Time.deltaTime * this.Speed * 2f;
			if (this.Strength > this.Threshold)
			{
				this.Strength = this.Threshold;
			}
			if (this.Scale > this.Target)
			{
				this.Threshold *= this.Slowdown;
				this.Shrink = true;
			}
		}
		else
		{
			this.Strength -= Time.deltaTime * this.Speed * 2f;
			float num = this.Threshold * -1f;
			if (this.Strength < num)
			{
				this.Strength = num;
			}
			if (this.Scale < this.Target)
			{
				this.Threshold *= this.Slowdown;
				this.Shrink = false;
			}
		}
		if (this.Timer > 3.33333f)
		{
			this.FallSpeed += Time.deltaTime * 10f * 2f;
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y - this.FallSpeed * this.FallSpeed * 2f, base.transform.localPosition.z);
		}
		base.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x000E4DF8 File Offset: 0x000E2FF8
	public void Return()
	{
		base.transform.localPosition = this.OriginalPosition;
		base.transform.localScale = Vector3.zero;
		this.FallSpeed = 0f;
		this.Threshold = 1f;
		this.Slowdown = 0.5f;
		this.Strength = 1f;
		this.Target = 1f;
		this.Scale = 0f;
		this.Speed = 5f;
		this.Timer = 0f;
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400230A RID: 8970
	public float FallSpeed;

	// Token: 0x0400230B RID: 8971
	public float Threshold = 1f;

	// Token: 0x0400230C RID: 8972
	public float Slowdown = 0.5f;

	// Token: 0x0400230D RID: 8973
	public float Strength = 1f;

	// Token: 0x0400230E RID: 8974
	public float Target = 1f;

	// Token: 0x0400230F RID: 8975
	public float Scale;

	// Token: 0x04002310 RID: 8976
	public float Speed = 5f;

	// Token: 0x04002311 RID: 8977
	public float Timer;

	// Token: 0x04002312 RID: 8978
	public bool Shrink;

	// Token: 0x04002313 RID: 8979
	public Vector3 OriginalPosition;
}
