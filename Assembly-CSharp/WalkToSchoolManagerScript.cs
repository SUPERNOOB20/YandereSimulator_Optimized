﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004B6 RID: 1206
public class WalkToSchoolManagerScript : MonoBehaviour
{
	// Token: 0x06001F9A RID: 8090 RVA: 0x001BBBDC File Offset: 0x001B9DDC
	private void Start()
	{
		Application.targetFrameRate = 60;
		if (SchoolGlobals.SchoolAtmosphere < 0.5f || GameGlobals.LoveSick)
		{
			this.Darkness.color = new Color(0f, 0f, 0f, 1f);
		}
		else
		{
			this.Darkness.color = new Color(1f, 1f, 1f, 1f);
		}
		this.Window.localScale = new Vector3(0f, 0f, 0f);
		this.Yandere.Character.GetComponent<Animation>()["f02_newWalk_00"].time = UnityEngine.Random.Range(0f, this.Yandere.Character.GetComponent<Animation>()["f02_newWalk_00"].length);
		this.Yandere.WearOutdoorShoes();
		this.Senpai.WearOutdoorShoes();
		this.Rival.WearOutdoorShoes();
		if (GameGlobals.BlondeHair)
		{
			this.PonytailRenderer.material.mainTexture = this.BlondePony;
		}
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x001BBCF4 File Offset: 0x001B9EF4
	private void Update()
	{
		for (int i = 1; i < 3; i++)
		{
			Transform transform = this.Neighborhood[i];
			transform.position = new Vector3(transform.position.x - Time.deltaTime * this.ScrollSpeed, transform.position.y, transform.position.z);
			if (transform.position.x < -160f)
			{
				transform.position = new Vector3(transform.position.x + 320f, transform.position.y, transform.position.z);
			}
		}
		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				if (!this.ShowWindow)
				{
					if (!this.Ending)
					{
						if (Input.GetButtonDown("A"))
						{
							this.Timer = 1f;
						}
						this.Timer += Time.deltaTime;
						if (this.Timer > 1f)
						{
							this.RivalEyeRTarget = this.RivalEyeR.localEulerAngles.y;
							this.RivalEyeLTarget = this.RivalEyeL.localEulerAngles.y;
							this.SenpaiEyeRTarget = this.SenpaiEyeR.localEulerAngles.y;
							this.SenpaiEyeLTarget = this.SenpaiEyeL.localEulerAngles.y;
							this.ShowWindow = true;
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Continue";
							this.PromptBar.Label[2].text = "Skip";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
						}
					}
					else
					{
						this.Window.localScale = Vector3.Lerp(this.Window.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
						if ((double)this.Window.localScale.x < 0.01)
						{
							this.Timer += Time.deltaTime;
							if (this.Timer > 1f)
							{
								this.FadeOut = true;
							}
						}
					}
				}
				else
				{
					this.Window.localScale = Vector3.Lerp(this.Window.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
					if ((double)this.Window.localScale.x > 0.99)
					{
						if (this.Frame > 3)
						{
							this.Typewriter.mLabel.color = new Color(1f, 1f, 1f, 1f);
						}
						this.Frame++;
					}
					if (!this.Talk)
					{
						if ((double)this.Window.localScale.x > 0.99)
						{
							this.Talk = true;
							this.UpdateNameLabel();
							this.Typewriter.enabled = true;
							this.Typewriter.ResetToBeginning();
							this.Typewriter.mFullText = this.Lines[this.ID];
							this.Typewriter.mLabel.text = this.Lines[this.ID];
							this.Typewriter.mLabel.color = new Color(1f, 1f, 1f, 0f);
							this.MyAudio.clip = this.Speech[this.ID];
							this.MyAudio.Play();
						}
					}
					else
					{
						if (this.Auto && !this.MyAudio.isPlaying)
						{
							this.AutoTimer += Time.deltaTime;
						}
						if (Input.GetButtonDown("A") || this.AutoTimer > 1f)
						{
							Debug.Log("Detected button press.");
							this.AutoTimer = 0f;
							if (this.ID < this.Lines.Length - 1)
							{
								if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
								{
									Debug.Log("Line not finished yet.");
									this.Typewriter.Finish();
									this.Typewriter.mCurrentOffset = this.Typewriter.mFullText.Length;
								}
								else
								{
									Debug.Log("Line finished.");
									this.ID++;
									this.Frame = 0;
									this.Typewriter.ResetToBeginning();
									this.Typewriter.mFullText = this.Lines[this.ID];
									this.Typewriter.mLabel.text = this.Lines[this.ID];
									this.Typewriter.mLabel.color = new Color(1f, 1f, 1f, 0f);
									this.MyAudio.clip = this.Speech[this.ID];
									this.MyAudio.Play();
									this.UpdateNameLabel();
								}
							}
							else if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
							{
								this.Typewriter.Finish();
							}
							else
							{
								this.End();
							}
						}
						if (Input.GetButtonDown("X"))
						{
							this.End();
						}
					}
				}
			}
		}
		else
		{
			this.MyAudio.volume -= Time.deltaTime;
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f && !this.Debugging)
			{
				SceneManager.LoadScene("LoadingScene");
			}
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 10f;
		}
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 10f;
		}
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x001BC3A0 File Offset: 0x001BA5A0
	private void LateUpdate()
	{
		if (this.Talk)
		{
			if (!this.Ending)
			{
				this.RivalNeckTarget = Mathf.Lerp(this.RivalNeckTarget, 15f, Time.deltaTime * 3.6f);
				this.RivalHeadTarget = Mathf.Lerp(this.RivalHeadTarget, 15f, Time.deltaTime * 3.6f);
				this.RivalEyeRTarget = Mathf.Lerp(this.RivalEyeRTarget, 95f, Time.deltaTime * 3.6f);
				this.RivalEyeLTarget = Mathf.Lerp(this.RivalEyeLTarget, 275f, Time.deltaTime * 3.6f);
				this.SenpaiNeckTarget = Mathf.Lerp(this.SenpaiNeckTarget, -15f, Time.deltaTime * 3.6f);
				this.SenpaiHeadTarget = Mathf.Lerp(this.SenpaiHeadTarget, -15f, Time.deltaTime * 3.6f);
				this.SenpaiEyeRTarget = Mathf.Lerp(this.SenpaiEyeRTarget, 85f, Time.deltaTime * 3.6f);
				this.SenpaiEyeLTarget = Mathf.Lerp(this.SenpaiEyeLTarget, 265f, Time.deltaTime * 3.6f);
				this.YandereNeckTarget = Mathf.Lerp(this.YandereNeckTarget, 7.5f, Time.deltaTime * 3.6f);
				this.YandereHeadTarget = Mathf.Lerp(this.YandereHeadTarget, 7.5f, Time.deltaTime * 3.6f);
			}
			else
			{
				this.RivalNeckTarget = Mathf.Lerp(this.RivalNeckTarget, 0f, Time.deltaTime * 3.6f);
				this.RivalHeadTarget = Mathf.Lerp(this.RivalHeadTarget, 0f, Time.deltaTime * 3.6f);
				this.RivalEyeRTarget = Mathf.Lerp(this.RivalEyeRTarget, 90f, Time.deltaTime * 3.6f);
				this.RivalEyeLTarget = Mathf.Lerp(this.RivalEyeLTarget, 270f, Time.deltaTime * 3.6f);
				this.SenpaiNeckTarget = Mathf.Lerp(this.SenpaiNeckTarget, 0f, Time.deltaTime * 3.6f);
				this.SenpaiHeadTarget = Mathf.Lerp(this.SenpaiHeadTarget, 0f, Time.deltaTime * 3.6f);
				this.SenpaiEyeRTarget = Mathf.Lerp(this.SenpaiEyeRTarget, 90f, Time.deltaTime * 3.6f);
				this.SenpaiEyeLTarget = Mathf.Lerp(this.SenpaiEyeLTarget, 270f, Time.deltaTime * 3.6f);
				this.YandereNeckTarget = Mathf.Lerp(this.YandereNeckTarget, 0f, Time.deltaTime * 3.6f);
				this.YandereHeadTarget = Mathf.Lerp(this.YandereHeadTarget, 0f, Time.deltaTime * 3.6f);
			}
			this.RivalNeck.localEulerAngles = new Vector3(this.RivalNeck.localEulerAngles.x, this.RivalNeckTarget, this.RivalNeck.localEulerAngles.z);
			this.RivalHead.localEulerAngles = new Vector3(this.RivalHead.localEulerAngles.x, this.RivalHeadTarget, this.RivalHead.localEulerAngles.z);
			this.RivalEyeR.localEulerAngles = new Vector3(this.RivalEyeR.localEulerAngles.x, this.RivalEyeRTarget, this.RivalEyeR.localEulerAngles.z);
			this.RivalEyeL.localEulerAngles = new Vector3(this.RivalEyeL.localEulerAngles.x, this.RivalEyeLTarget, this.RivalEyeL.localEulerAngles.z);
			this.SenpaiNeck.localEulerAngles = new Vector3(this.SenpaiNeck.localEulerAngles.x, this.SenpaiNeckTarget, this.SenpaiNeck.localEulerAngles.z);
			this.SenpaiHead.localEulerAngles = new Vector3(this.SenpaiHead.localEulerAngles.x, this.SenpaiHeadTarget, this.SenpaiHead.localEulerAngles.z);
			this.SenpaiEyeR.localEulerAngles = new Vector3(this.SenpaiEyeR.localEulerAngles.x, this.SenpaiEyeRTarget, this.SenpaiEyeR.localEulerAngles.z);
			this.SenpaiEyeL.localEulerAngles = new Vector3(this.SenpaiEyeL.localEulerAngles.x, this.SenpaiEyeLTarget, this.SenpaiEyeL.localEulerAngles.z);
			this.YandereNeck.localEulerAngles = new Vector3(this.YandereNeck.localEulerAngles.x, this.YandereNeckTarget, this.YandereNeck.localEulerAngles.z);
			this.YandereHead.localEulerAngles = new Vector3(this.YandereHead.localEulerAngles.x, this.YandereHeadTarget, this.YandereHead.localEulerAngles.z);
			if (this.MyAudio.isPlaying)
			{
				this.MouthTimer += Time.deltaTime;
				if (this.MouthTimer > this.TimerLimit)
				{
					this.MouthTarget = UnityEngine.Random.Range(40f, 40f + this.MouthExtent);
					this.MouthTimer = 0f;
				}
				if (this.Speakers[this.ID])
				{
					this.RivalJaw.localEulerAngles = new Vector3(this.RivalJaw.localEulerAngles.x, this.RivalJaw.localEulerAngles.y, Mathf.Lerp(this.RivalJaw.localEulerAngles.z, this.MouthTarget, Time.deltaTime * this.TalkSpeed));
					this.RivalLipL.localPosition = new Vector3(this.RivalLipL.localPosition.x, Mathf.Lerp(this.RivalLipL.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.RivalLipL.localPosition.z);
					this.RivalLipR.localPosition = new Vector3(this.RivalLipR.localPosition.x, Mathf.Lerp(this.RivalLipR.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.RivalLipR.localPosition.z);
					return;
				}
				this.SenpaiJaw.localEulerAngles = new Vector3(this.SenpaiJaw.localEulerAngles.x, this.SenpaiJaw.localEulerAngles.y, Mathf.Lerp(this.SenpaiJaw.localEulerAngles.z, this.MouthTarget, Time.deltaTime * this.TalkSpeed));
				this.SenpaiLipL.localPosition = new Vector3(this.SenpaiLipL.localPosition.x, Mathf.Lerp(this.SenpaiLipL.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.SenpaiLipL.localPosition.z);
				this.SenpaiLipR.localPosition = new Vector3(this.SenpaiLipR.localPosition.x, Mathf.Lerp(this.SenpaiLipR.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.SenpaiLipR.localPosition.z);
			}
		}
	}

	// Token: 0x06001F9D RID: 8093 RVA: 0x001BCB20 File Offset: 0x001BAD20
	public void UpdateNameLabel()
	{
		if (this.Speakers[this.ID])
		{
			this.NameLabel.text = "Osana-chan";
			return;
		}
		this.NameLabel.text = "Senpai-kun";
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x001BCB52 File Offset: 0x001BAD52
	public void End()
	{
		this.PromptBar.Show = false;
		this.ShowWindow = false;
		this.Ending = true;
		this.Timer = 0f;
	}

	// Token: 0x040041BC RID: 16828
	public PromptBarScript PromptBar;

	// Token: 0x040041BD RID: 16829
	public CosmeticScript Yandere;

	// Token: 0x040041BE RID: 16830
	public CosmeticScript Senpai;

	// Token: 0x040041BF RID: 16831
	public CosmeticScript Rival;

	// Token: 0x040041C0 RID: 16832
	public UISprite Darkness;

	// Token: 0x040041C1 RID: 16833
	public Transform[] Neighborhood;

	// Token: 0x040041C2 RID: 16834
	public Transform Window;

	// Token: 0x040041C3 RID: 16835
	public Transform RivalNeck;

	// Token: 0x040041C4 RID: 16836
	public Transform RivalHead;

	// Token: 0x040041C5 RID: 16837
	public Transform RivalEyeR;

	// Token: 0x040041C6 RID: 16838
	public Transform RivalEyeL;

	// Token: 0x040041C7 RID: 16839
	public Transform RivalJaw;

	// Token: 0x040041C8 RID: 16840
	public Transform RivalLipL;

	// Token: 0x040041C9 RID: 16841
	public Transform RivalLipR;

	// Token: 0x040041CA RID: 16842
	public Transform SenpaiNeck;

	// Token: 0x040041CB RID: 16843
	public Transform SenpaiHead;

	// Token: 0x040041CC RID: 16844
	public Transform SenpaiEyeR;

	// Token: 0x040041CD RID: 16845
	public Transform SenpaiEyeL;

	// Token: 0x040041CE RID: 16846
	public Transform SenpaiJaw;

	// Token: 0x040041CF RID: 16847
	public Transform SenpaiLipL;

	// Token: 0x040041D0 RID: 16848
	public Transform SenpaiLipR;

	// Token: 0x040041D1 RID: 16849
	public Transform YandereNeck;

	// Token: 0x040041D2 RID: 16850
	public Transform YandereHead;

	// Token: 0x040041D3 RID: 16851
	public Transform YandereEyeR;

	// Token: 0x040041D4 RID: 16852
	public Transform YandereEyeL;

	// Token: 0x040041D5 RID: 16853
	public AudioSource MyAudio;

	// Token: 0x040041D6 RID: 16854
	public float ScrollSpeed = 1f;

	// Token: 0x040041D7 RID: 16855
	public float LipStrength = 0.0001f;

	// Token: 0x040041D8 RID: 16856
	public float TimerLimit = 0.1f;

	// Token: 0x040041D9 RID: 16857
	public float TalkSpeed = 10f;

	// Token: 0x040041DA RID: 16858
	public float AutoTimer;

	// Token: 0x040041DB RID: 16859
	public float Timer;

	// Token: 0x040041DC RID: 16860
	public float MouthExtent = 5f;

	// Token: 0x040041DD RID: 16861
	public float MouthTarget;

	// Token: 0x040041DE RID: 16862
	public float MouthTimer;

	// Token: 0x040041DF RID: 16863
	public float RivalNeckTarget;

	// Token: 0x040041E0 RID: 16864
	public float RivalHeadTarget;

	// Token: 0x040041E1 RID: 16865
	public float RivalEyeRTarget;

	// Token: 0x040041E2 RID: 16866
	public float RivalEyeLTarget;

	// Token: 0x040041E3 RID: 16867
	public float SenpaiNeckTarget;

	// Token: 0x040041E4 RID: 16868
	public float SenpaiHeadTarget;

	// Token: 0x040041E5 RID: 16869
	public float SenpaiEyeRTarget;

	// Token: 0x040041E6 RID: 16870
	public float SenpaiEyeLTarget;

	// Token: 0x040041E7 RID: 16871
	public float YandereNeckTarget;

	// Token: 0x040041E8 RID: 16872
	public float YandereHeadTarget;

	// Token: 0x040041E9 RID: 16873
	public bool ShowWindow;

	// Token: 0x040041EA RID: 16874
	public bool Debugging;

	// Token: 0x040041EB RID: 16875
	public bool FadeOut;

	// Token: 0x040041EC RID: 16876
	public bool Ending;

	// Token: 0x040041ED RID: 16877
	public bool Auto;

	// Token: 0x040041EE RID: 16878
	public bool Talk;

	// Token: 0x040041EF RID: 16879
	public TypewriterEffect Typewriter;

	// Token: 0x040041F0 RID: 16880
	public UILabel NameLabel;

	// Token: 0x040041F1 RID: 16881
	public AudioClip[] Speech;

	// Token: 0x040041F2 RID: 16882
	public string[] Lines;

	// Token: 0x040041F3 RID: 16883
	public bool[] Speakers;

	// Token: 0x040041F4 RID: 16884
	public int Frame;

	// Token: 0x040041F5 RID: 16885
	public int ID;

	// Token: 0x040041F6 RID: 16886
	public Renderer PonytailRenderer;

	// Token: 0x040041F7 RID: 16887
	public Texture BlondePony;
}
