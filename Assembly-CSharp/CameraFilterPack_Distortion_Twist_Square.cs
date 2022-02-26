﻿using System;
using UnityEngine;

// Token: 0x02000183 RID: 387
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Twist_Square")]
public class CameraFilterPack_Distortion_Twist_Square : MonoBehaviour
{
	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00077B1C File Offset: 0x00075D1C
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

	// Token: 0x06000DD2 RID: 3538 RVA: 0x00077B50 File Offset: 0x00075D50
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Twist_Square");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00077B74 File Offset: 0x00075D74
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
			this.material.SetFloat("_CenterX", this.CenterX);
			this.material.SetFloat("_CenterY", this.CenterY);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetFloat("_Size", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x00077C65 File Offset: 0x00075E65
	private void Update()
	{
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x00077C67 File Offset: 0x00075E67
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001214 RID: 4628
	public Shader SCShader;

	// Token: 0x04001215 RID: 4629
	private float TimeX = 1f;

	// Token: 0x04001216 RID: 4630
	private Material SCMaterial;

	// Token: 0x04001217 RID: 4631
	[Range(-2f, 2f)]
	public float CenterX = 0.5f;

	// Token: 0x04001218 RID: 4632
	[Range(-2f, 2f)]
	public float CenterY = 0.5f;

	// Token: 0x04001219 RID: 4633
	[Range(-3.14f, 3.14f)]
	public float Distortion = 0.5f;

	// Token: 0x0400121A RID: 4634
	[Range(-2f, 2f)]
	public float Size = 0.25f;
}
