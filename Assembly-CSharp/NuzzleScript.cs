﻿using System;
using UnityEngine;

// Token: 0x02000384 RID: 900
public class NuzzleScript : MonoBehaviour
{
	// Token: 0x06001A3D RID: 6717 RVA: 0x00114DED File Offset: 0x00112FED
	private void Start()
	{
		this.OriginalRotation = base.transform.localEulerAngles;
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x00114E00 File Offset: 0x00113000
	private void Update()
	{
		if (!this.Down)
		{
			this.Rotate += Time.deltaTime * this.Speed;
			if (this.Rotate > this.Limit)
			{
				this.Down = true;
			}
		}
		else
		{
			this.Rotate -= Time.deltaTime * this.Speed;
			if (this.Rotate < -1f * this.Limit)
			{
				this.Down = false;
			}
		}
		base.transform.localEulerAngles = this.OriginalRotation + new Vector3(this.Rotate, 0f, 0f);
	}

	// Token: 0x04002AE8 RID: 10984
	public Vector3 OriginalRotation;

	// Token: 0x04002AE9 RID: 10985
	public float Rotate;

	// Token: 0x04002AEA RID: 10986
	public float Limit;

	// Token: 0x04002AEB RID: 10987
	public float Speed;

	// Token: 0x04002AEC RID: 10988
	private bool Down;
}
