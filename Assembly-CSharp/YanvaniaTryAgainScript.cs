﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004E4 RID: 1252
public class YanvaniaTryAgainScript : MonoBehaviour
{
	// Token: 0x060020BB RID: 8379 RVA: 0x001E1B84 File Offset: 0x001DFD84
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x060020BC RID: 8380 RVA: 0x001E1B98 File Offset: 0x001DFD98
	private void Update()
	{
		if (!this.FadeOut)
		{
			if (base.transform.localScale.x > 0.9f)
			{
				if (this.InputManager.TappedLeft)
				{
					this.Selected = 1;
				}
				if (this.InputManager.TappedRight)
				{
					this.Selected = 2;
				}
				if (this.Selected == 1)
				{
					this.Highlight.localPosition = new Vector3(Mathf.Lerp(this.Highlight.localPosition.x, -100f, Time.deltaTime * 10f), this.Highlight.localPosition.y, this.Highlight.localPosition.z);
					this.Highlight.localScale = new Vector3(Mathf.Lerp(this.Highlight.localScale.x, -1f, Time.deltaTime * 10f), this.Highlight.localScale.y, this.Highlight.localScale.z);
				}
				else
				{
					this.Highlight.localPosition = new Vector3(Mathf.Lerp(this.Highlight.localPosition.x, 100f, Time.deltaTime * 10f), this.Highlight.localPosition.y, this.Highlight.localPosition.z);
					this.Highlight.localScale = new Vector3(Mathf.Lerp(this.Highlight.localScale.x, 1f, Time.deltaTime * 10f), this.Highlight.localScale.y, this.Highlight.localScale.z);
				}
				if (Input.GetButtonDown("A") || Input.GetKeyDown("z") || Input.GetKeyDown("x"))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ButtonEffect, this.Highlight.position, Quaternion.identity);
					gameObject.transform.parent = this.Highlight;
					gameObject.transform.localPosition = Vector3.zero;
					this.FadeOut = true;
					return;
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				if (this.Selected == 1)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					return;
				}
				SceneManager.LoadScene("YanvaniaTitleScene");
			}
		}
	}

	// Token: 0x040047CE RID: 18382
	public InputManagerScript InputManager;

	// Token: 0x040047CF RID: 18383
	public GameObject ButtonEffect;

	// Token: 0x040047D0 RID: 18384
	public Transform Highlight;

	// Token: 0x040047D1 RID: 18385
	public UISprite Darkness;

	// Token: 0x040047D2 RID: 18386
	public bool FadeOut;

	// Token: 0x040047D3 RID: 18387
	public int Selected = 1;
}
