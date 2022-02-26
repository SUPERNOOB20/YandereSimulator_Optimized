﻿using System;
using UnityEngine;

// Token: 0x0200019D RID: 413
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Toon")]
public class CameraFilterPack_Drawing_Toon : MonoBehaviour
{
	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0007A396 File Offset: 0x00078596
	private Material material
	{
		get
		{
			if (this.SCMaterial == null)
			{
				this.SCMaterial = new Material(this.SCShader);
				this.SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.SCMaterial;
		}
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x0007A3CA File Offset: 0x000785CA
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Toon");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x0007A3EC File Offset: 0x000785EC
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Distortion", this.Threshold);
			this.material.SetFloat("_DotSize", this.DotSize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x0007A488 File Offset: 0x00078688
	private void Update()
	{
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0007A48A File Offset: 0x0007868A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012C1 RID: 4801
	public Shader SCShader;

	// Token: 0x040012C2 RID: 4802
	private Material SCMaterial;

	// Token: 0x040012C3 RID: 4803
	private float TimeX = 1f;

	// Token: 0x040012C4 RID: 4804
	[Range(0f, 2f)]
	public float Threshold = 1f;

	// Token: 0x040012C5 RID: 4805
	[Range(0f, 8f)]
	public float DotSize = 1f;
}
