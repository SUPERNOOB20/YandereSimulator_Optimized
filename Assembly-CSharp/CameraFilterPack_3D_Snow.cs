﻿using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Snow")]
public class CameraFilterPack_3D_Snow : MonoBehaviour
{
	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0006A740 File Offset: 0x00068940
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

	// Token: 0x06000B03 RID: 2819 RVA: 0x0006A774 File Offset: 0x00068974
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Blizzard1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/3D_Snow");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x0006A7AC File Offset: 0x000689AC
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("_Value4", this.Speed * 6f);
			this.material.SetFloat("_Value5", this.Size);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("Drop_Near", this.Snow_Near);
			this.material.SetFloat("Drop_Far", this.Snow_Far);
			this.material.SetFloat("Drop_With_Obj", this.SnowWithoutObject);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x0006A941 File Offset: 0x00068B41
	private void Update()
	{
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x0006A943 File Offset: 0x00068B43
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000EE0 RID: 3808
	public Shader SCShader;

	// Token: 0x04000EE1 RID: 3809
	public bool _Visualize;

	// Token: 0x04000EE2 RID: 3810
	private float TimeX = 1f;

	// Token: 0x04000EE3 RID: 3811
	private Material SCMaterial;

	// Token: 0x04000EE4 RID: 3812
	[Range(0f, 100f)]
	public float _FixDistance = 5f;

	// Token: 0x04000EE5 RID: 3813
	[Range(-0.5f, 0.99f)]
	public float Snow_Near = 0.08f;

	// Token: 0x04000EE6 RID: 3814
	[Range(0f, 1f)]
	public float Snow_Far = 0.55f;

	// Token: 0x04000EE7 RID: 3815
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000EE8 RID: 3816
	[Range(0f, 2f)]
	public float Intensity = 1f;

	// Token: 0x04000EE9 RID: 3817
	[Range(0.4f, 2f)]
	public float Size = 1f;

	// Token: 0x04000EEA RID: 3818
	[Range(0f, 0.5f)]
	public float Speed = 0.275f;

	// Token: 0x04000EEB RID: 3819
	[Range(0f, 1f)]
	public float SnowWithoutObject = 1f;

	// Token: 0x04000EEC RID: 3820
	private Texture2D Texture2;
}
