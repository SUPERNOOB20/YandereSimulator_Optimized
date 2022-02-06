﻿using System;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class DemonArmScript : MonoBehaviour
{
	// Token: 0x06001371 RID: 4977 RVA: 0x000B2998 File Offset: 0x000B0B98
	private void Start()
	{
		this.MyAnimation = base.GetComponent<Animation>();
		if (!this.Rising)
		{
			this.MyAnimation[this.IdleAnim].speed = this.AnimSpeed * 0.5f;
		}
		this.MyAnimation[this.AttackAnim].speed = 1f;
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x000B29F8 File Offset: 0x000B0BF8
	private void Update()
	{
		if (!this.Rising)
		{
			if (!this.Attacking)
			{
				this.MyAnimation.CrossFade(this.IdleAnim);
				return;
			}
			if (!this.Attacked)
			{
				if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length * 0.15f)
				{
					this.ClawCollider.enabled = true;
					this.Attacked = true;
					return;
				}
			}
			else
			{
				if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length * 0.4f)
				{
					this.ClawCollider.enabled = false;
				}
				if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length)
				{
					this.MyAnimation.CrossFade(this.IdleAnim);
					this.ClawCollider.enabled = false;
					this.Attacking = false;
					this.Attacked = false;
					this.AnimTime = 0f;
					return;
				}
			}
		}
		else if (this.MyAnimation["DemonArmRise"].time >= this.MyAnimation["DemonArmRise"].length)
		{
			this.Rising = false;
		}
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x000B2B58 File Offset: 0x000B0D58
	private void OnTriggerEnter(Collider other)
	{
		StudentScript component = other.gameObject.GetComponent<StudentScript>();
		if (component != null && component.StudentID > 1)
		{
			AudioSource component2 = base.GetComponent<AudioSource>();
			component2.clip = this.Whoosh;
			component2.pitch = UnityEngine.Random.Range(-0.9f, 1.1f);
			component2.Play();
			base.GetComponent<Animation>().CrossFade(this.AttackAnim);
			this.Attacking = true;
		}
	}

	// Token: 0x04001C94 RID: 7316
	public GameObject DismembermentCollider;

	// Token: 0x04001C95 RID: 7317
	public Animation MyAnimation;

	// Token: 0x04001C96 RID: 7318
	public Collider ClawCollider;

	// Token: 0x04001C97 RID: 7319
	public bool Attacking;

	// Token: 0x04001C98 RID: 7320
	public bool Attacked;

	// Token: 0x04001C99 RID: 7321
	public bool Rising = true;

	// Token: 0x04001C9A RID: 7322
	public string IdleAnim = "DemonArmIdle";

	// Token: 0x04001C9B RID: 7323
	public string AttackAnim = "DemonArmAttack";

	// Token: 0x04001C9C RID: 7324
	public AudioClip Whoosh;

	// Token: 0x04001C9D RID: 7325
	public float AnimSpeed = 1f;

	// Token: 0x04001C9E RID: 7326
	public float AnimTime;
}
