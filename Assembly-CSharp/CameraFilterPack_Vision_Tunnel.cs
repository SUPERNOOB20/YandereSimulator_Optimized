﻿using System;
using UnityEngine;

// Token: 0x02000230 RID: 560
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Tunnel")]
public class CameraFilterPack_Vision_Tunnel : MonoBehaviour
{
	// Token: 0x17000334 RID: 820
	// (get) Token: 0x06001203 RID: 4611 RVA: 0x0008A71C File Offset: 0x0008891C
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

	// Token: 0x06001204 RID: 4612 RVA: 0x0008A750 File Offset: 0x00088950
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Tunnel");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x0008A774 File Offset: 0x00088974
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Intensity);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x0008A86C File Offset: 0x00088A6C
	private void Update()
	{
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x0008A86E File Offset: 0x00088A6E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040016A9 RID: 5801
	public Shader SCShader;

	// Token: 0x040016AA RID: 5802
	private float TimeX = 1f;

	// Token: 0x040016AB RID: 5803
	private Material SCMaterial;

	// Token: 0x040016AC RID: 5804
	[Range(0f, 1f)]
	public float Value = 0.6f;

	// Token: 0x040016AD RID: 5805
	[Range(0f, 1f)]
	public float Value2 = 0.4f;

	// Token: 0x040016AE RID: 5806
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x040016AF RID: 5807
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
