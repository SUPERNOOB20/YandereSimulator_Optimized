﻿using System;
using UnityEngine;

// Token: 0x02000117 RID: 279
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Blood")]
public class CameraFilterPack_AAA_Blood : MonoBehaviour
{
	// Token: 0x1700021B RID: 539
	// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0006A88E File Offset: 0x00068A8E
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

	// Token: 0x06000B09 RID: 2825 RVA: 0x0006A8C2 File Offset: 0x00068AC2
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_AAA_Blood1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Blood");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x0006A8F8 File Offset: 0x00068AF8
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
			this.material.SetFloat("_Value", this.LightReflect);
			this.material.SetFloat("_Value2", this.Blood1);
			this.material.SetFloat("_Value3", this.Blood2);
			this.material.SetFloat("_Value4", this.Blood3);
			this.material.SetFloat("_Value5", this.Blood4);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x0006A9EF File Offset: 0x00068BEF
	private void Update()
	{
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x0006A9F1 File Offset: 0x00068BF1
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000EE4 RID: 3812
	public Shader SCShader;

	// Token: 0x04000EE5 RID: 3813
	private float TimeX = 1f;

	// Token: 0x04000EE6 RID: 3814
	[Range(0f, 128f)]
	public float Blood1;

	// Token: 0x04000EE7 RID: 3815
	[Range(0f, 128f)]
	public float Blood2;

	// Token: 0x04000EE8 RID: 3816
	[Range(0f, 128f)]
	public float Blood3;

	// Token: 0x04000EE9 RID: 3817
	[Range(0f, 128f)]
	public float Blood4 = 1f;

	// Token: 0x04000EEA RID: 3818
	[Range(0f, 0.004f)]
	public float LightReflect = 0.002f;

	// Token: 0x04000EEB RID: 3819
	private Material SCMaterial;

	// Token: 0x04000EEC RID: 3820
	private Texture2D Texture2;
}
