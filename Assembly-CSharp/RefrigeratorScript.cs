﻿using System;
using UnityEngine;

// Token: 0x020003CF RID: 975
public class RefrigeratorScript : MonoBehaviour
{
	// Token: 0x06001B63 RID: 7011 RVA: 0x0013387C File Offset: 0x00131A7C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.CookingEvent.EventCheck = false;
				this.Yandere.EmptyHands();
				this.Yandere.CanMove = false;
				this.Yandere.Cooking = true;
			}
		}
		if (this.Yandere.Cooking)
		{
			Quaternion b = Quaternion.LookRotation(new Vector3(this.Octodogs[1].transform.position.x, this.Yandere.transform.position.y, this.Octodogs[1].transform.position.z) - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, b, Time.deltaTime * 10f);
			this.Yandere.MoveTowardsTarget(this.CookingSpot.position);
			if (this.EventPhase == 0)
			{
				this.Yandere.Character.GetComponent<Animation>().Play("f02_prepareFood_00");
				this.Octodog.transform.parent = this.Yandere.RightHand;
				this.Octodog.transform.localPosition = new Vector3(0.0129f, -0.02475f, 0.0316f);
				this.Octodog.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
				this.Sausage.transform.parent = this.Yandere.RightHand;
				this.Sausage.transform.localPosition = new Vector3(0.013f, -0.038f, 0.015f);
				this.Sausage.transform.localEulerAngles = Vector3.zero;
				this.EventPhase++;
				return;
			}
			if (this.EventPhase == 1)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time > 1f)
				{
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 2)
			{
				this.Refrigerator.GetComponent<Animation>().Play("FridgeOpen");
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time > 3f)
				{
					this.Jar.parent = this.Yandere.RightHand;
					this.Jar.localPosition = new Vector3(0f, -0.033333335f, -0.14f);
					this.Jar.localEulerAngles = new Vector3(90f, 0f, 0f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 3)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time > 5f)
				{
					this.JarLid.transform.parent = this.Yandere.LeftHand;
					this.JarLid.localPosition = new Vector3(0.033333335f, 0f, 0f);
					this.JarLid.localEulerAngles = Vector3.zero;
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 4)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time > 6f)
				{
					this.JarLid.parent = this.CookingClub;
					this.JarLid.localPosition = new Vector3(0.334585f, 1f, -0.2528915f);
					this.JarLid.localEulerAngles = new Vector3(0f, 30f, 0f);
					this.Jar.parent = this.CookingClub;
					this.Jar.localPosition = new Vector3(0.29559f, 1f, 0.2029152f);
					this.Jar.localEulerAngles = new Vector3(0f, -150f, 0f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 5)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time > 7f)
				{
					this.Knife.GetComponent<WeaponScript>().FingerprintID = 100;
					this.Knife.parent = this.Yandere.LeftHand;
					this.Knife.localPosition = new Vector3(0f, -0.01f, 0f);
					this.Knife.localEulerAngles = new Vector3(0f, 0f, -90f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 6)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time >= this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_cutFood_00");
					this.Sausage.SetActive(true);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 7)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_cutFood_00"].time > 2.66666f)
				{
					this.Octodog.SetActive(true);
					this.Sausage.SetActive(false);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 8)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_cutFood_00"].time > 3f)
				{
					this.Rotation = Mathf.MoveTowards(this.Rotation, 90f, Time.deltaTime * 360f);
					this.Octodog.transform.localEulerAngles = new Vector3(this.Rotation, this.Octodog.transform.localEulerAngles.y, this.Octodog.transform.localEulerAngles.z);
					this.Octodog.transform.localPosition = new Vector3(this.Octodog.transform.localPosition.x, this.Octodog.transform.localPosition.y, Mathf.MoveTowards(this.Octodog.transform.localPosition.z, 0.012f, Time.deltaTime));
				}
				if (this.Yandere.Character.GetComponent<Animation>()["f02_cutFood_00"].time > 6f)
				{
					this.Octodog.SetActive(false);
					for (int i = 1; i < this.Octodogs.Length; i++)
					{
						this.Octodogs[i].SetActive(true);
					}
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 9)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_cutFood_00"].time >= this.Yandere.Character.GetComponent<Animation>()["f02_cutFood_00"].length)
				{
					this.Yandere.Character.GetComponent<Animation>().Play("f02_prepareFood_00");
					this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time = this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length;
					this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].speed = -1f;
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 10)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time < this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length - 1f)
				{
					this.Knife.parent = this.CookingClub;
					this.Knife.localPosition = new Vector3(0.197f, 1.1903f, -0.33333334f);
					this.Knife.localEulerAngles = new Vector3(45f, -90f, -90f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 11)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time < this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length - 2f)
				{
					this.JarLid.parent = this.Yandere.LeftHand;
					this.JarLid.localPosition = new Vector3(0.033333335f, 0f, 0f);
					this.JarLid.localEulerAngles = Vector3.zero;
					this.Jar.parent = this.Yandere.RightHand;
					this.Jar.localPosition = new Vector3(0f, -0.033333335f, -0.14f);
					this.Jar.localEulerAngles = new Vector3(90f, 0f, 0f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 12)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time < this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length - 3f)
				{
					this.JarLid.parent = this.Jar;
					this.JarLid.localPosition = new Vector3(0f, 0.175f, 0f);
					this.JarLid.localEulerAngles = Vector3.zero;
					this.Refrigerator.GetComponent<Animation>().Play("FridgeOpen");
					this.Refrigerator.GetComponent<Animation>()["FridgeOpen"].time = this.Refrigerator.GetComponent<Animation>()["FridgeOpen"].length;
					this.Refrigerator.GetComponent<Animation>()["FridgeOpen"].speed = -1f;
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 13)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time < this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length - 5f)
				{
					this.Jar.parent = this.CookingClub;
					this.Jar.localPosition = new Vector3(0.1f, 0.941f, 0.75f);
					this.Jar.localEulerAngles = new Vector3(0f, 90f, 0f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 14 && this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time <= 0f)
			{
				this.PlateCollider.enabled = true;
				this.PlatePickUp.enabled = true;
				this.PlatePrompt.enabled = true;
				this.Yandere.Cooking = false;
				this.Yandere.CanMove = true;
				this.Empty = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.enabled = false;
			}
		}
	}

	// Token: 0x04002EB9 RID: 11961
	public CookingEventScript CookingEvent;

	// Token: 0x04002EBA RID: 11962
	public YandereScript Yandere;

	// Token: 0x04002EBB RID: 11963
	public PromptScript Prompt;

	// Token: 0x04002EBC RID: 11964
	public PickUpScript PlatePickUp;

	// Token: 0x04002EBD RID: 11965
	public PromptScript PlatePrompt;

	// Token: 0x04002EBE RID: 11966
	public Collider PlateCollider;

	// Token: 0x04002EBF RID: 11967
	public GameObject[] Octodogs;

	// Token: 0x04002EC0 RID: 11968
	public GameObject Refrigerator;

	// Token: 0x04002EC1 RID: 11969
	public GameObject Octodog;

	// Token: 0x04002EC2 RID: 11970
	public GameObject Sausage;

	// Token: 0x04002EC3 RID: 11971
	public Transform CookingSpot;

	// Token: 0x04002EC4 RID: 11972
	public Transform CookingClub;

	// Token: 0x04002EC5 RID: 11973
	public Transform JarLid;

	// Token: 0x04002EC6 RID: 11974
	public Transform Knife;

	// Token: 0x04002EC7 RID: 11975
	public Transform Jar;

	// Token: 0x04002EC8 RID: 11976
	public bool Empty;

	// Token: 0x04002EC9 RID: 11977
	public int EventPhase;

	// Token: 0x04002ECA RID: 11978
	public float Rotation;
}
