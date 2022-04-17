﻿using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class DelinquentMaskScript : MonoBehaviour
{
	// Token: 0x0600136E RID: 4974 RVA: 0x000B1E70 File Offset: 0x000B0070
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.ID++;
			if (this.ID > 4)
			{
				this.ID = 0;
			}
			this.MyRenderer.mesh = this.Meshes[this.ID];
		}
	}

	// Token: 0x04001C6C RID: 7276
	public MeshFilter MyRenderer;

	// Token: 0x04001C6D RID: 7277
	public Mesh[] Meshes;

	// Token: 0x04001C6E RID: 7278
	public int ID;
}
