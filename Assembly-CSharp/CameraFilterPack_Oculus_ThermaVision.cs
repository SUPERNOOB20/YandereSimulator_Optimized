﻿using System;
using UnityEngine;

// Token: 0x020001F4 RID: 500
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/ThermaVision")]
public class CameraFilterPack_Oculus_ThermaVision : MonoBehaviour
{
	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x0600109A RID: 4250 RVA: 0x00084539 File Offset: 0x00082739
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

	// Token: 0x0600109B RID: 4251 RVA: 0x0008456D File Offset: 0x0008276D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Oculus_ThermaVision");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x00084590 File Offset: 0x00082790
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
			this.material.SetFloat("_Value", this.Therma_Variation);
			this.material.SetFloat("_Value2", this.Contrast);
			this.material.SetFloat("_Value3", this.Burn);
			this.material.SetFloat("_Value4", this.SceneCut);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x00084688 File Offset: 0x00082888
	private void Update()
	{
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x0008468A File Offset: 0x0008288A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400151E RID: 5406
	public Shader SCShader;

	// Token: 0x0400151F RID: 5407
	private float TimeX = 1f;

	// Token: 0x04001520 RID: 5408
	private Material SCMaterial;

	// Token: 0x04001521 RID: 5409
	[Range(0f, 1f)]
	public float Therma_Variation = 0.5f;

	// Token: 0x04001522 RID: 5410
	[Range(0f, 8f)]
	private float Contrast = 3f;

	// Token: 0x04001523 RID: 5411
	[Range(0f, 4f)]
	private float Burn;

	// Token: 0x04001524 RID: 5412
	[Range(0f, 16f)]
	private float SceneCut = 1f;
}
