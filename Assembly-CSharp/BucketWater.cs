﻿using System;
using UnityEngine;

// Token: 0x020000FB RID: 251
[Serializable]
public class BucketWater : BucketContents
{
	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0005CF5F File Offset: 0x0005B15F
	// (set) Token: 0x06000A72 RID: 2674 RVA: 0x0005CF67 File Offset: 0x0005B167
	public float Bloodiness
	{
		get
		{
			return this.bloodiness;
		}
		set
		{
			this.bloodiness = Mathf.Clamp01(value);
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0005CF75 File Offset: 0x0005B175
	// (set) Token: 0x06000A74 RID: 2676 RVA: 0x0005CF7D File Offset: 0x0005B17D
	public bool HasBleach
	{
		get
		{
			return this.hasBleach;
		}
		set
		{
			this.hasBleach = value;
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0005CF86 File Offset: 0x0005B186
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Water;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0005CF89 File Offset: 0x0005B189
	public override bool IsCleaningAgent
	{
		get
		{
			return this.hasBleach;
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0005CF91 File Offset: 0x0005B191
	public override bool IsFlammable
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0005CF94 File Offset: 0x0005B194
	public override bool CanBeLifted(int strength)
	{
		return true;
	}

	// Token: 0x04000C3E RID: 3134
	[SerializeField]
	private float bloodiness;

	// Token: 0x04000C3F RID: 3135
	[SerializeField]
	private bool hasBleach;
}
