﻿using System;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class FamilyVoiceScript : MonoBehaviour
{
	// Token: 0x06001498 RID: 5272 RVA: 0x000C95CE File Offset: 0x000C77CE
	private void Start()
	{
		this.Subtitle.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x000C95F4 File Offset: 0x000C77F4
	private void Update()
	{
		if (!this.GameOver)
		{
			if (this.Investigating)
			{
				this.LookForYandere();
				base.transform.parent.position = Vector3.MoveTowards(base.transform.parent.position, this.Node[this.CurrentNode].position, Time.deltaTime);
				if (this.FixTimer == 0f || this.FixTimer == 5f)
				{
					Quaternion b = Quaternion.LookRotation(this.Node[this.CurrentNode].position - base.transform.parent.position);
					base.transform.parent.rotation = Quaternion.Slerp(base.transform.parent.rotation, b, Time.deltaTime * 10f);
				}
				if (Vector3.Distance(base.transform.parent.position, this.Node[this.CurrentNode].position) < 0.1f)
				{
					if (this.CurrentNode == this.Node.Length - 1)
					{
						this.Return = true;
					}
					if (!this.Return)
					{
						this.CurrentNode++;
						if (this.CurrentNode == 3)
						{
							AudioSource.PlayClipAtPoint(this.DoorOpen, this.Door.transform.position);
							this.OpenFrontDoor = true;
						}
					}
					else if (this.FixTimer == 5f)
					{
						this.BreakerDoor.Label.text = "Open";
						this.BreakerDoor.Open = false;
						this.MyAnimation.CrossFade("walkConfident_00");
						this.CurrentNode--;
						if (this.CurrentNode == 1)
						{
							AudioSource.PlayClipAtPoint(this.DoorOpen, this.Door.transform.position);
							this.OpenFrontDoor = false;
						}
						if (this.CurrentNode < 0)
						{
							this.MyAnimation.CrossFade("fatherFixing_00");
							this.BreakerDoor.ServedPurpose = false;
							this.Investigating = false;
							this.Return = false;
							this.CurrentNode = 1;
							this.FixTimer = 0f;
						}
					}
					else
					{
						this.FixTimer = Mathf.MoveTowards(this.FixTimer, 5f, Time.deltaTime);
						if (this.FixTimer >= 4f && !this.Lights.activeInHierarchy)
						{
							AudioSource.PlayClipAtPoint(this.PowerOn, Camera.main.transform.position);
							this.Lights.SetActive(true);
						}
						this.MyAnimation.CrossFade("rummage_00");
					}
				}
				if (this.OpenFrontDoor)
				{
					this.Rotation = Mathf.Lerp(this.Rotation, this.TargetRotation, Time.deltaTime * 3.6f);
				}
				else
				{
					this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 3.6f);
				}
				this.Door.transform.localEulerAngles = new Vector3(this.Door.transform.localEulerAngles.x, this.Rotation, this.Door.transform.localEulerAngles.z);
				return;
			}
			if (this.Yandere.transform.position.y > base.transform.position.y - 1f && this.Yandere.transform.position.y < base.transform.position.y + 1f)
			{
				this.Distance = Vector3.Distance(this.Yandere.transform.position, base.transform.position);
				if (this.Distance < this.MinimumDistance)
				{
					if (!this.Started)
					{
						this.Subtitle.text = this.SpeechText[0];
						this.MyAudio.Play();
						this.Started = true;
					}
					else
					{
						this.MyAudio.pitch = Time.timeScale;
						if (this.MultiClip)
						{
							if (this.MyAnimation["fatherFixing_00"].time > this.MyAnimation["fatherFixing_00"].length)
							{
								this.MyAnimation["fatherFixing_00"].time = this.MyAnimation["fatherFixing_00"].time - this.MyAnimation["fatherFixing_00"].length;
							}
							if (this.AnimPhase == 0)
							{
								if (this.MyAnimation["fatherFixing_00"].time > 18f && this.MyAnimation["fatherFixing_00"].time < 18.1f)
								{
									this.Subtitle.text = this.SpeechText[this.SpeechPhase];
									this.MyAudio.clip = this.SpeechClip[this.SpeechPhase];
									this.MyAudio.Play();
									this.SpeechPhase++;
									this.AnimPhase = 1;
								}
							}
							else if (this.MyAnimation["fatherFixing_00"].time < 1f)
							{
								this.Subtitle.text = this.SpeechText[this.SpeechPhase];
								this.MyAudio.clip = this.SpeechClip[this.SpeechPhase];
								this.MyAudio.Play();
								this.SpeechPhase++;
								this.AnimPhase = 0;
							}
						}
						else if (this.SpeechPhase < this.SpeechTime.Length && this.MyAudio.time > this.SpeechTime[this.SpeechPhase])
						{
							this.Subtitle.text = this.SpeechText[this.SpeechPhase];
							this.SpeechPhase++;
						}
						this.Scale = Mathf.Abs(1f - (this.Distance - 1f) / (this.MinimumDistance - 1f));
						if (this.Scale < 0f)
						{
							this.Scale = 0f;
						}
						if (this.Scale > 1f)
						{
							this.Scale = 1f;
						}
						this.Jukebox.volume = 1f - 0.9f * this.Scale;
						this.Subtitle.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
						this.MyAudio.volume = this.Scale;
					}
					for (int i = 0; i < this.Boundary.Length; i++)
					{
						Transform transform = this.Boundary[i];
						if (transform != null && Vector3.Distance(this.Yandere.transform.position, transform.position) < 0.33333f)
						{
							Debug.Log("Got a ''proximity'' game over from " + base.gameObject.name);
							AudioSource.PlayClipAtPoint(this.CrunchSound, Camera.main.transform.position);
							this.TransitionToGameOver();
						}
					}
					this.LookForYandere();
				}
				else if (this.Distance < this.MinimumDistance + 1f)
				{
					this.Jukebox.volume = 1f;
					this.MyAudio.volume = 0f;
					this.Subtitle.transform.localScale = new Vector3(0f, 0f, 0f);
				}
			}
			if (this.Node.Length != 0)
			{
				Quaternion b2 = Quaternion.LookRotation(new Vector3(5.4f, 0f, -100f) - base.transform.parent.position);
				base.transform.parent.rotation = Quaternion.Slerp(base.transform.parent.rotation, b2, Time.deltaTime * 10f);
				return;
			}
		}
		else if (this.GameOverPhase == 0)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f && !this.MyAudio.isPlaying)
			{
				Debug.Log("Should be updating the subtitle with the Game Over text.");
				this.Subtitle.transform.localScale = new Vector3(1f, 1f, 1f);
				this.Subtitle.text = (this.GameOverText ?? "");
				this.MyAudio.clip = this.GameOverLine;
				this.MyAudio.Play();
				this.GameOverPhase++;
				return;
			}
		}
		else if (!this.MyAudio.isPlaying || Input.GetButton("A"))
		{
			this.Heartbroken.SetActive(true);
			this.Subtitle.text = "";
			base.enabled = false;
			this.MyAudio.Stop();
		}
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x000C9ECC File Offset: 0x000C80CC
	private bool YandereIsInFOV()
	{
		Vector3 to = this.Yandere.transform.position - this.Head.position;
		float num = 90f;
		return Vector3.Angle(this.Head.forward, to) <= num;
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x000C9F18 File Offset: 0x000C8118
	private bool YandereIsInLOS()
	{
		Debug.DrawLine(this.Head.position, new Vector3(this.Yandere.transform.position.x, this.YandereHead.position.y, this.Yandere.transform.position.z), Color.red);
		RaycastHit raycastHit;
		return Physics.Linecast(this.Head.position, new Vector3(this.Yandere.transform.position.x, this.YandereHead.position.y, this.Yandere.transform.position.z), out raycastHit) && raycastHit.collider.gameObject.layer == 13;
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x000C9FE4 File Offset: 0x000C81E4
	private void TransitionToGameOver()
	{
		this.Marker.Tex.transform.localScale = new Vector3(1f, 0f, 1f);
		this.Marker.Tex.color = new Color(1f, 0f, 0f, 0f);
		this.Darkness.material.color = new Color(0f, 0f, 0f, 1f);
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.CanMove = false;
		this.Subtitle.text = "";
		this.GameOver = true;
		this.Jukebox.Stop();
		this.MyAudio.Stop();
		this.Alpha = 0f;
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x000CA0C4 File Offset: 0x000C82C4
	private void LookForYandere()
	{
		if (this.Yandere.Hidden && this.Yandere.Stance.Current == StanceType.Crouching)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * this.NoticeSpeed);
		}
		else
		{
			if (this.YandereIsInFOV())
			{
				if (this.YandereIsInLOS())
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * this.NoticeSpeed);
				}
				else
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * this.NoticeSpeed);
				}
			}
			else
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * this.NoticeSpeed);
			}
			if (this.Investigating && Vector3.Distance(this.Yandere.transform.position, base.transform.parent.position) < 1f)
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * this.NoticeSpeed * 2f);
			}
		}
		if (this.Alpha == 1f)
		{
			Debug.Log("Got a ''witnessed'' game over from " + base.gameObject.name);
			AudioSource.PlayClipAtPoint(this.GameOverSound, Camera.main.transform.position);
			this.TransitionToGameOver();
		}
		this.Marker.Tex.transform.localScale = new Vector3(1f, this.Alpha, 1f);
		this.Marker.Tex.color = new Color(1f, 0f, 0f, this.Alpha);
	}

	// Token: 0x04001FF4 RID: 8180
	public StalkerPromptScript BreakerDoor;

	// Token: 0x04001FF5 RID: 8181
	public StalkerYandereScript Yandere;

	// Token: 0x04001FF6 RID: 8182
	public DetectionMarkerScript Marker;

	// Token: 0x04001FF7 RID: 8183
	public AudioClip GameOverSound;

	// Token: 0x04001FF8 RID: 8184
	public AudioClip GameOverLine;

	// Token: 0x04001FF9 RID: 8185
	public AudioClip CrunchSound;

	// Token: 0x04001FFA RID: 8186
	public GameObject Heartbroken;

	// Token: 0x04001FFB RID: 8187
	public GameObject Lights;

	// Token: 0x04001FFC RID: 8188
	public Animation MyAnimation;

	// Token: 0x04001FFD RID: 8189
	public Transform YandereHead;

	// Token: 0x04001FFE RID: 8190
	public Transform Door;

	// Token: 0x04001FFF RID: 8191
	public Transform Head;

	// Token: 0x04002000 RID: 8192
	public AudioSource Jukebox;

	// Token: 0x04002001 RID: 8193
	public AudioSource MyAudio;

	// Token: 0x04002002 RID: 8194
	public Renderer Darkness;

	// Token: 0x04002003 RID: 8195
	public UILabel Subtitle;

	// Token: 0x04002004 RID: 8196
	public AudioClip[] SpeechClip;

	// Token: 0x04002005 RID: 8197
	public AudioClip DoorOpen;

	// Token: 0x04002006 RID: 8198
	public AudioClip PowerOn;

	// Token: 0x04002007 RID: 8199
	public Transform[] Boundary;

	// Token: 0x04002008 RID: 8200
	public Transform[] Node;

	// Token: 0x04002009 RID: 8201
	public string[] SpeechText;

	// Token: 0x0400200A RID: 8202
	public float[] SpeechTime;

	// Token: 0x0400200B RID: 8203
	public string GameOverText;

	// Token: 0x0400200C RID: 8204
	public float MinimumDistance;

	// Token: 0x0400200D RID: 8205
	public float NoticeSpeed;

	// Token: 0x0400200E RID: 8206
	public float Distance;

	// Token: 0x0400200F RID: 8207
	public float FixTimer;

	// Token: 0x04002010 RID: 8208
	public float Alpha;

	// Token: 0x04002011 RID: 8209
	public float Scale;

	// Token: 0x04002012 RID: 8210
	public float Timer;

	// Token: 0x04002013 RID: 8211
	public float TargetRotation;

	// Token: 0x04002014 RID: 8212
	public float Rotation;

	// Token: 0x04002015 RID: 8213
	public int GameOverPhase;

	// Token: 0x04002016 RID: 8214
	public int CurrentNode;

	// Token: 0x04002017 RID: 8215
	public int SpeechPhase;

	// Token: 0x04002018 RID: 8216
	public int AnimPhase;

	// Token: 0x04002019 RID: 8217
	public bool Investigating;

	// Token: 0x0400201A RID: 8218
	public bool OpenFrontDoor;

	// Token: 0x0400201B RID: 8219
	public bool MultiClip;

	// Token: 0x0400201C RID: 8220
	public bool GameOver;

	// Token: 0x0400201D RID: 8221
	public bool Started;

	// Token: 0x0400201E RID: 8222
	public bool Return;
}
