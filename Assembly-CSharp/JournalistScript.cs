﻿using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class JournalistScript : MonoBehaviour
{
	// Token: 0x0600191C RID: 6428 RVA: 0x000FC458 File Offset: 0x000FA658
	private void Start()
	{
		if (!GameGlobals.Eighties || GameGlobals.EightiesTutorial || DateGlobals.Week > 10)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			this.Pathfinding.target = this.Destinations[0];
		}
		this.PepperSpray.SetActive(false);
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x000FC4AC File Offset: 0x000FA6AC
	private void Update()
	{
		if (base.transform.position.z > -95f)
		{
			this.DistanceToDestination = Vector3.Distance(base.transform.position, this.Pathfinding.target.position);
			this.DistanceToPlayer = Vector3.Distance(base.transform.position, this.Yandere.transform.position);
			if (!this.Chasing)
			{
				if (this.DistanceToPlayer < 5f && this.CanSeeYandere())
				{
					this.MyAnimation.CrossFade("idleTough_00");
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.targetRotation = Quaternion.LookRotation(this.Yandere.transform.position - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					if (this.DistanceToPlayer > 1f)
					{
						this.SpeechTimer -= Time.deltaTime;
						if (this.SpeechTimer <= 0f && this.SpeechID < this.SpeechLines.Length)
						{
							AudioSource.PlayClipAtPoint(this.SpeechClips[this.SpeechID], base.transform.position);
							if (this.Subtitle.EventSubtitle.text == "" || this.Subtitle.EventSubtitle.transform.localScale.x < 1f)
							{
								this.Subtitle.CustomText = this.SpeechLines[this.SpeechID];
								this.Subtitle.UpdateLabel(SubtitleType.Custom, 0, 4f);
							}
							this.SpeechTimer = 5f;
							this.SpeechID++;
						}
					}
					else
					{
						this.ThreatTimer -= Time.deltaTime;
						if (this.ThreatTimer <= 0f && this.ThreatID < this.ThreatLines.Length)
						{
							AudioSource.PlayClipAtPoint(this.ThreatClips[this.ThreatID], base.transform.position);
							if (this.Subtitle.EventSubtitle.text == "" || this.Subtitle.EventSubtitle.transform.localScale.x < 1f)
							{
								this.Subtitle.PreviousSubtitle = SubtitleType.AcceptFood;
								this.Subtitle.CustomText = this.ThreatLines[this.ThreatID];
								this.Subtitle.UpdateLabel(SubtitleType.Custom, 0, 4f);
							}
							this.ThreatTimer = 5f;
							this.ThreatID++;
						}
						if (this.Yandere.Armed)
						{
							this.Chase();
						}
					}
				}
				else if (this.DistanceToDestination < 1f)
				{
					this.MyAnimation.CrossFade("leaning_00");
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.targetRotation = Quaternion.LookRotation(this.LookTarget.position - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					this.Timer += Time.deltaTime;
					if (this.Timer > 5f)
					{
						if (this.Pathfinding.target != this.Destinations[1])
						{
							this.Pathfinding.target = this.Destinations[1];
						}
						else
						{
							this.Pathfinding.target = this.Destinations[2];
						}
						this.Timer = 0f;
					}
				}
				else
				{
					this.MyAnimation.CrossFade("walkTough_00");
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
				}
				if (this.DistanceToPlayer < 15f && this.CanSeeYandere())
				{
					this.CheckBehavior();
					return;
				}
			}
			else
			{
				this.targetRotation = Quaternion.LookRotation(base.transform.position - this.Yandere.transform.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				this.ChaseTimer += Time.deltaTime;
				if (this.ChaseTimer > 1f)
				{
					if (this.DistanceToPlayer > 1f)
					{
						this.MyAnimation.CrossFade("sprint_00");
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.Pathfinding.speed = 5f;
						return;
					}
					if (!this.Yandere.Sprayed)
					{
						AudioSource.PlayClipAtPoint(this.PepperSpraySFX, base.transform.position);
					}
					this.targetRotation = Quaternion.LookRotation(this.Yandere.transform.position - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					this.Yandere.CharacterAnimation.CrossFade("f02_sprayed_00");
					this.Yandere.SprayedByJournalist = true;
					this.Yandere.YandereVision = false;
					this.Yandere.NearSenpai = false;
					this.Yandere.Attacking = false;
					this.Yandere.FollowHips = true;
					this.Yandere.Punching = false;
					this.Yandere.CanMove = false;
					this.Yandere.Sprayed = true;
					this.Yandere.StudentManager.YandereDying = true;
					this.Yandere.StudentManager.StopMoving();
					this.Yandere.Jukebox.Volume = 0f;
					this.Yandere.Blur.Size = 1f;
					if (this.Yandere.DelinquentFighting)
					{
						this.Yandere.StudentManager.CombatMinigame.Stop();
					}
					this.MyAnimation.CrossFade("spray_00");
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.Pathfinding.speed = 0f;
					this.PepperSpray.SetActive(true);
					if ((double)this.MyAnimation["spray_00"].time > 0.66666)
					{
						this.PepperSprayEffect.Play();
						if (this.Yandere.Armed)
						{
							this.Yandere.EquippedWeapon.Drop();
						}
						this.Yandere.EmptyHands();
						base.enabled = false;
						return;
					}
				}
				else
				{
					this.targetRotation = Quaternion.LookRotation(this.Yandere.transform.position - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				}
			}
		}
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x000FCC08 File Offset: 0x000FAE08
	private void CheckBehavior()
	{
		if (this.Yandere.CanMove && !this.Yandere.Egg && (this.Yandere.Chased || this.Yandere.Chasers > 0 || (this.Yandere.Armed && this.Yandere.EquippedWeapon.Bloody) || (this.Yandere.Carrying && !this.Yandere.CurrentRagdoll.Concealed) || (this.Yandere.Dragging && !this.Yandere.CurrentRagdoll.Concealed) || (this.Yandere.Bloodiness + (float)this.Yandere.GloveBlood > 0f && !this.Yandere.Paint) || (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart && !this.Yandere.PickUp.Garbage)))
		{
			this.Chase();
		}
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x000FCD28 File Offset: 0x000FAF28
	public bool CanSeeYandere()
	{
		if (!this.Yandere.Egg)
		{
			Vector3 position = this.Head.position;
			Vector3 end = new Vector3(this.Yandere.transform.position.x, this.Yandere.Head.position.y, this.Yandere.transform.position.z);
			RaycastHit raycastHit;
			if (Physics.Linecast(position, end, out raycastHit, this.Mask) && raycastHit.collider.gameObject == this.Yandere.gameObject)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x000FCDCC File Offset: 0x000FAFCC
	private void Chase()
	{
		this.Face.name = "RENAMED";
		this.Subtitle.PreviousSubtitle = SubtitleType.AcceptFood;
		AudioSource.PlayClipAtPoint(this.ChaseVoice, this.Yandere.MainCamera.transform.position);
		this.Subtitle.CustomText = "I knew it was you!";
		this.Subtitle.UpdateLabel(SubtitleType.Custom, 0, 4f);
		this.MyAnimation.CrossFade("readyToFight_00");
		this.Yandere.CharacterAnimation.CrossFade("f02_readyToFight_00");
		this.Yandere.CanMove = false;
		this.Pathfinding.target = this.Yandere.transform;
		this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;
		if (this.Yandere.Carrying)
		{
			this.Yandere.EmptyHands();
		}
		this.Chasing = true;
	}

	// Token: 0x0400277F RID: 10111
	public ParticleSystem PepperSprayEffect;

	// Token: 0x04002780 RID: 10112
	public float DistanceToDestination;

	// Token: 0x04002781 RID: 10113
	public float DistanceToPlayer;

	// Token: 0x04002782 RID: 10114
	public float SpeechTimer;

	// Token: 0x04002783 RID: 10115
	public float ThreatTimer;

	// Token: 0x04002784 RID: 10116
	public float ChaseTimer;

	// Token: 0x04002785 RID: 10117
	public float Timer;

	// Token: 0x04002786 RID: 10118
	public Quaternion targetRotation;

	// Token: 0x04002787 RID: 10119
	public AudioClip PepperSpraySFX;

	// Token: 0x04002788 RID: 10120
	public AudioClip ChaseVoice;

	// Token: 0x04002789 RID: 10121
	public Transform[] Destinations;

	// Token: 0x0400278A RID: 10122
	public AudioClip[] SpeechClips;

	// Token: 0x0400278B RID: 10123
	public AudioClip[] ThreatClips;

	// Token: 0x0400278C RID: 10124
	public string[] SpeechLines;

	// Token: 0x0400278D RID: 10125
	public string[] ThreatLines;

	// Token: 0x0400278E RID: 10126
	public SubtitleScript Subtitle;

	// Token: 0x0400278F RID: 10127
	public YandereScript Yandere;

	// Token: 0x04002790 RID: 10128
	public GameObject PepperSpray;

	// Token: 0x04002791 RID: 10129
	public GameObject Face;

	// Token: 0x04002792 RID: 10130
	public Animation MyAnimation;

	// Token: 0x04002793 RID: 10131
	public Transform LookTarget;

	// Token: 0x04002794 RID: 10132
	public AIPath Pathfinding;

	// Token: 0x04002795 RID: 10133
	public bool Chasing;

	// Token: 0x04002796 RID: 10134
	public int SpeechID;

	// Token: 0x04002797 RID: 10135
	public int ThreatID;

	// Token: 0x04002798 RID: 10136
	public Transform Head;

	// Token: 0x04002799 RID: 10137
	public LayerMask Mask;
}
