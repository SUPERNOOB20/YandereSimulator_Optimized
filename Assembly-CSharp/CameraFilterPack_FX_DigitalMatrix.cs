﻿using System;
using UnityEngine;

// Token: 0x020001AC RID: 428
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/DigitalMatrix")]
public class CameraFilterPack_FX_DigitalMatrix : MonoBehaviour
{
	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0007BA64 File Offset: 0x00079C64
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

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0007BA98 File Offset: 0x00079C98
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_DigitalMatrix");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0007BABC File Offset: 0x00079CBC
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.ColorR);
			this.material.SetFloat("_Value3", this.ColorG);
			this.material.SetFloat("_Value4", this.ColorB);
			this.material.SetFloat("_Value5", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0007BBCA File Offset: 0x00079DCA
	private void Update()
	{
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0007BBCC File Offset: 0x00079DCC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400131F RID: 4895
	public Shader SCShader;

	// Token: 0x04001320 RID: 4896
	private float TimeX = 1f;

	// Token: 0x04001321 RID: 4897
	private Material SCMaterial;

	// Token: 0x04001322 RID: 4898
	[Range(0.4f, 5f)]
	public float Size = 1f;

	// Token: 0x04001323 RID: 4899
	[Range(-10f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001324 RID: 4900
	[Range(-1f, 1f)]
	public float ColorR = -1f;

	// Token: 0x04001325 RID: 4901
	[Range(-1f, 1f)]
	public float ColorG = 1f;

	// Token: 0x04001326 RID: 4902
	[Range(-1f, 1f)]
	public float ColorB = -1f;
}
