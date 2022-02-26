﻿using System;
using UnityEngine;

// Token: 0x020001DE RID: 478
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Lut/Lut 2 Lut Extra")]
public class CameraFilterPack_Lut_2_Lut_Extra : MonoBehaviour
{
	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x00080E31 File Offset: 0x0007F031
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

	// Token: 0x06000FF9 RID: 4089 RVA: 0x00080E65 File Offset: 0x0007F065
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Lut_2_lut_Extra");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x00080E88 File Offset: 0x0007F088
	public void SetIdentityLut()
	{
		int num = 16;
		Color[] array = new Color[num * num * num];
		float num2 = 1f / (1f * (float)num - 1f);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					array[i + j * num + k * num * num] = new Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
				}
			}
		}
		if (this.converted3DLut)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut);
		}
		this.converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		this.converted3DLut.SetPixels(array);
		this.converted3DLut.Apply();
		if (this.converted3DLut2)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut2);
		}
		this.converted3DLut2 = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		this.converted3DLut2.SetPixels(array);
		this.converted3DLut2.Apply();
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x00080F9F File Offset: 0x0007F19F
	public bool ValidDimensions(Texture2D tex2d)
	{
		return tex2d && tex2d.height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width));
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x00080FC8 File Offset: 0x0007F1C8
	public Texture3D Convert(Texture2D temp2DTex, Texture3D cv3D)
	{
		int num = 4096;
		if (temp2DTex)
		{
			num = temp2DTex.width * temp2DTex.height;
			num = temp2DTex.height;
			if (!this.ValidDimensions(temp2DTex))
			{
				Debug.LogWarning("The given 2D texture " + temp2DTex.name + " cannot be used as a 3D LUT.");
				return cv3D;
			}
		}
		Color[] pixels = temp2DTex.GetPixels();
		Color[] array = new Color[pixels.Length];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					int num2 = num - j - 1;
					array[i + j * num + k * num * num] = pixels[k * num + i + num2 * num * num];
				}
			}
		}
		if (cv3D)
		{
			UnityEngine.Object.DestroyImmediate(cv3D);
		}
		cv3D = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		cv3D.SetPixels(array);
		cv3D.Apply();
		return cv3D;
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x000810B0 File Offset: 0x0007F2B0
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null || !SystemInfo.supports3DTextures)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			if (this.converted3DLut == null)
			{
				if (!this.LutTexture)
				{
					this.SetIdentityLut();
				}
				if (this.LutTexture)
				{
					this.converted3DLut = this.Convert(this.LutTexture, this.converted3DLut);
				}
			}
			if (this.converted3DLut2 == null)
			{
				if (!this.LutTexture2)
				{
					this.SetIdentityLut();
				}
				if (this.LutTexture2)
				{
					this.converted3DLut2 = this.Convert(this.LutTexture2, this.converted3DLut2);
				}
			}
			if (this.LutTexture)
			{
				this.converted3DLut.wrapMode = TextureWrapMode.Clamp;
			}
			if (this.LutTexture2)
			{
				this.converted3DLut2.wrapMode = TextureWrapMode.Clamp;
			}
			this.material.SetFloat("_Fade", this.FadeLut1);
			this.material.SetFloat("_Fade2", this.FadeLut2);
			this.material.SetFloat("_Pos", this.Pos);
			this.material.SetFloat("_Smooth", this.Smooth);
			this.material.SetTexture("_LutTex", this.converted3DLut);
			this.material.SetTexture("_LutTex2", this.converted3DLut2);
			Graphics.Blit(sourceTexture, destTexture, this.material, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x00081262 File Offset: 0x0007F462
	private void OnValidate()
	{
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x00081264 File Offset: 0x0007F464
	private void Update()
	{
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x00081266 File Offset: 0x0007F466
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400146D RID: 5229
	public Shader SCShader;

	// Token: 0x0400146E RID: 5230
	private float TimeX = 1f;

	// Token: 0x0400146F RID: 5231
	private Vector4 ScreenResolution;

	// Token: 0x04001470 RID: 5232
	private Material SCMaterial;

	// Token: 0x04001471 RID: 5233
	public Texture2D LutTexture;

	// Token: 0x04001472 RID: 5234
	public Texture2D LutTexture2;

	// Token: 0x04001473 RID: 5235
	private Texture3D converted3DLut;

	// Token: 0x04001474 RID: 5236
	private Texture3D converted3DLut2;

	// Token: 0x04001475 RID: 5237
	[Range(0f, 1f)]
	public float FadeLut1 = 1f;

	// Token: 0x04001476 RID: 5238
	[Range(0f, 1f)]
	public float FadeLut2 = 1f;

	// Token: 0x04001477 RID: 5239
	[Range(0f, 1f)]
	public float Pos;

	// Token: 0x04001478 RID: 5240
	[Range(0f, 1f)]
	public float Smooth = 1f;

	// Token: 0x04001479 RID: 5241
	private string MemoPath;

	// Token: 0x0400147A RID: 5242
	private string MemoPath2;
}
