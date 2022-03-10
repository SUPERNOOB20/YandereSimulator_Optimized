﻿using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020005B0 RID: 1456
	public class Meter : MonoBehaviour
	{
		// Token: 0x060024BB RID: 9403 RVA: 0x001FCDB4 File Offset: 0x001FAFB4
		private void Awake()
		{
			this.startPos = this.fillBar.transform.localPosition.x;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x001FCDD4 File Offset: 0x001FAFD4
		public void SetFill(float interpolater)
		{
			float num = Mathf.Lerp(this.emptyPos, this.startPos, interpolater);
			num = Mathf.Round(num * 50f) / 50f;
			this.fillBar.transform.localPosition = new Vector3(num, 0f, 0f);
		}

		// Token: 0x04004CA7 RID: 19623
		public SpriteRenderer fillBar;

		// Token: 0x04004CA8 RID: 19624
		public float emptyPos;

		// Token: 0x04004CA9 RID: 19625
		private float startPos;
	}
}
