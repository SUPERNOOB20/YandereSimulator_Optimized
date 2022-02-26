﻿using System;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class FalconPunchScript : MonoBehaviour
{
	// Token: 0x0600148F RID: 5263 RVA: 0x000C8FC7 File Offset: 0x000C71C7
	private void Start()
	{
		if (this.Mecha)
		{
			this.MyRigidbody.AddForce(base.transform.forward * this.Speed * 10f);
		}
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x000C8FFC File Offset: 0x000C71FC
	private void Update()
	{
		if (!this.IgnoreTime)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > this.TimeLimit)
			{
				this.MyCollider.enabled = false;
			}
		}
		if (this.Shipgirl)
		{
			this.MyRigidbody.AddForce(base.transform.forward * this.Speed);
		}
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x000C9068 File Offset: 0x000C7268
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("A punch collided with something.");
		if (other.gameObject.layer == 9)
		{
			Debug.Log("A punch collided with something on the Characters layer.");
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				Debug.Log("A punch collided with a student.");
				if (component.StudentID > 1)
				{
					Debug.Log("A punch collided with a student and killed them.");
					UnityEngine.Object.Instantiate<GameObject>(this.FalconExplosion, component.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
					component.DeathType = DeathType.EasterEgg;
					component.BecomeRagdoll();
					Rigidbody rigidbody = component.Ragdoll.AllRigidbodies[0];
					rigidbody.isKinematic = false;
					Vector3 vector = rigidbody.transform.position - component.Yandere.transform.position;
					if (this.Falcon)
					{
						rigidbody.AddForce(vector * this.Strength);
					}
					else if (this.Bancho)
					{
						rigidbody.AddForce(vector.x * this.Strength, 5000f, vector.z * this.Strength);
					}
					else
					{
						rigidbody.AddForce(vector.x * this.Strength, 10000f, vector.z * this.Strength);
					}
				}
			}
		}
		if (this.Destructive && other.gameObject.layer != 2 && other.gameObject.layer != 8 && other.gameObject.layer != 9 && other.gameObject.layer != 13 && other.gameObject.layer != 17)
		{
			GameObject gameObject = null;
			StudentScript component2 = other.gameObject.transform.root.GetComponent<StudentScript>();
			if (component2 != null)
			{
				if (component2.StudentID <= 1)
				{
					gameObject = component2.gameObject;
				}
			}
			else
			{
				gameObject = other.gameObject;
			}
			if (gameObject != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.FalconExplosion, base.transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
				UnityEngine.Object.Destroy(gameObject);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04001FE2 RID: 8162
	public GameObject FalconExplosion;

	// Token: 0x04001FE3 RID: 8163
	public Rigidbody MyRigidbody;

	// Token: 0x04001FE4 RID: 8164
	public Collider MyCollider;

	// Token: 0x04001FE5 RID: 8165
	public float Strength = 100f;

	// Token: 0x04001FE6 RID: 8166
	public float Speed = 100f;

	// Token: 0x04001FE7 RID: 8167
	public bool Destructive;

	// Token: 0x04001FE8 RID: 8168
	public bool IgnoreTime;

	// Token: 0x04001FE9 RID: 8169
	public bool Shipgirl;

	// Token: 0x04001FEA RID: 8170
	public bool Bancho;

	// Token: 0x04001FEB RID: 8171
	public bool Falcon;

	// Token: 0x04001FEC RID: 8172
	public bool Mecha;

	// Token: 0x04001FED RID: 8173
	public float TimeLimit = 0.5f;

	// Token: 0x04001FEE RID: 8174
	public float Timer;
}
