﻿using System;
using UnityEngine;

// Token: 0x0200016D RID: 365
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/HSV")]
public class CameraFilterPack_Colors_HSV : MonoBehaviour
{
	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00075D6F File Offset: 0x00073F6F
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

	// Token: 0x06000D4E RID: 3406 RVA: 0x00075DA3 File Offset: 0x00073FA3
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x00075DB4 File Offset: 0x00073FB4
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetFloat("_HueShift", this._HueShift);
			this.material.SetFloat("_Sat", this._Saturation);
			this.material.SetFloat("_Val", this._ValueBrightness);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x00075E26 File Offset: 0x00074026
	private void Update()
	{
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x00075E28 File Offset: 0x00074028
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011A0 RID: 4512
	public Shader SCShader;

	// Token: 0x040011A1 RID: 4513
	[Range(0f, 360f)]
	public float _HueShift = 180f;

	// Token: 0x040011A2 RID: 4514
	[Range(-32f, 32f)]
	public float _Saturation = 1f;

	// Token: 0x040011A3 RID: 4515
	[Range(-32f, 32f)]
	public float _ValueBrightness = 1f;

	// Token: 0x040011A4 RID: 4516
	private Material SCMaterial;
}
