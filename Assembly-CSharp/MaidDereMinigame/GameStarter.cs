﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200059F RID: 1439
	public class GameStarter : MonoBehaviour
	{
		// Token: 0x06002472 RID: 9330 RVA: 0x001FC01C File Offset: 0x001FA21C
		private void Awake()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.audioSource = base.GetComponent<AudioSource>();
			base.StartCoroutine(this.CountdownToStart());
			GameController.Instance.tipPage = this.tipPage;
			GameController.Instance.whiteFadeOutPost = this.whiteFadeOutPost;
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x001FC070 File Offset: 0x001FA270
		public void SetGameTime(float gameTime)
		{
			int num = Mathf.CeilToInt(gameTime);
			if ((float)num == 10f)
			{
				SFXController.PlaySound(SFXController.Sounds.ClockTick);
			}
			if (gameTime > 3f)
			{
				return;
			}
			if (num - 1 <= 2)
			{
				this.spriteRenderer.sprite = this.numbers[num];
				return;
			}
			this.EndGame();
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x001FC0C0 File Offset: 0x001FA2C0
		public void EndGame()
		{
			base.StartCoroutine(this.EndGameRoutine());
			SFXController.PlaySound(SFXController.Sounds.GameSuccess);
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x001FC0D5 File Offset: 0x001FA2D5
		private IEnumerator CountdownToStart()
		{
			yield return new WaitForSeconds(GameController.Instance.activeDifficultyVariables.transitionTime);
			SFXController.PlaySound(SFXController.Sounds.Countdown);
			while (this.timeToStart > 0)
			{
				yield return new WaitForSeconds(1f);
				this.timeToStart--;
				this.spriteRenderer.sprite = this.numbers[this.timeToStart];
			}
			yield return new WaitForSeconds(1f);
			GameController.SetPause(false);
			this.spriteRenderer.sprite = null;
			yield break;
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x001FC0E4 File Offset: 0x001FA2E4
		private IEnumerator EndGameRoutine()
		{
			GameController.SetPause(true);
			this.spriteRenderer.sprite = this.timeUp;
			yield return new WaitForSeconds(1f);
			UnityEngine.Object.FindObjectOfType<InteractionMenu>().gameObject.SetActive(false);
			GameController.TimeUp();
			yield break;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x001FC0F3 File Offset: 0x001FA2F3
		public void SetAudioPitch(float value)
		{
			this.audioSource.pitch = value;
		}

		// Token: 0x04004C6C RID: 19564
		public List<Sprite> numbers;

		// Token: 0x04004C6D RID: 19565
		public SpriteRenderer whiteFadeOutPost;

		// Token: 0x04004C6E RID: 19566
		public Sprite timeUp;

		// Token: 0x04004C6F RID: 19567
		public TipPage tipPage;

		// Token: 0x04004C70 RID: 19568
		private AudioSource audioSource;

		// Token: 0x04004C71 RID: 19569
		private SpriteRenderer spriteRenderer;

		// Token: 0x04004C72 RID: 19570
		private int timeToStart = 3;
	}
}
