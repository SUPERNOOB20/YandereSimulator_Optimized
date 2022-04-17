﻿using System;
using UnityEngine;

// Token: 0x0200008A RID: 138
[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	// Token: 0x06000565 RID: 1381 RVA: 0x000342AC File Offset: 0x000324AC
	private void Cache()
	{
		this.mCached = true;
		this.mWidget = base.GetComponent<UIWidget>();
		if (this.mWidget != null)
		{
			return;
		}
		this.mSr = base.GetComponent<SpriteRenderer>();
		if (this.mSr != null)
		{
			return;
		}
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			this.mMat = component.material;
			return;
		}
		this.mLight = base.GetComponent<Light>();
		if (this.mLight == null)
		{
			this.mWidget = base.GetComponentInChildren<UIWidget>();
		}
	}

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000566 RID: 1382 RVA: 0x00034339 File Offset: 0x00032539
	// (set) Token: 0x06000567 RID: 1383 RVA: 0x00034341 File Offset: 0x00032541
	[Obsolete("Use 'value' instead")]
	public Color color
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000568 RID: 1384 RVA: 0x0003434C File Offset: 0x0003254C
	// (set) Token: 0x06000569 RID: 1385 RVA: 0x000343D4 File Offset: 0x000325D4
	public Color value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				return this.mWidget.color;
			}
			if (this.mMat != null)
			{
				return this.mMat.color;
			}
			if (this.mSr != null)
			{
				return this.mSr.color;
			}
			if (this.mLight != null)
			{
				return this.mLight.color;
			}
			return Color.black;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				this.mWidget.color = value;
				return;
			}
			if (this.mMat != null)
			{
				this.mMat.color = value;
				return;
			}
			if (this.mSr != null)
			{
				this.mSr.color = value;
				return;
			}
			if (this.mLight != null)
			{
				this.mLight.color = value;
				this.mLight.enabled = (value.r + value.g + value.b > 0.01f);
			}
		}
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00034480 File Offset: 0x00032680
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Color.Lerp(this.from, this.to, factor);
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0003449C File Offset: 0x0003269C
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration, 0f);
		tweenColor.from = tweenColor.value;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, true);
			tweenColor.enabled = false;
		}
		return tweenColor;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x000344E5 File Offset: 0x000326E5
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x000344F3 File Offset: 0x000326F3
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00034501 File Offset: 0x00032701
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0003450F File Offset: 0x0003270F
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x040005B5 RID: 1461
	public Color from = Color.white;

	// Token: 0x040005B6 RID: 1462
	public Color to = Color.white;

	// Token: 0x040005B7 RID: 1463
	private bool mCached;

	// Token: 0x040005B8 RID: 1464
	private UIWidget mWidget;

	// Token: 0x040005B9 RID: 1465
	private Material mMat;

	// Token: 0x040005BA RID: 1466
	private Light mLight;

	// Token: 0x040005BB RID: 1467
	private SpriteRenderer mSr;
}
