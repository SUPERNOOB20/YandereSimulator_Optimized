﻿using System;
using UnityEngine;

// Token: 0x0200028E RID: 654
public class DoorOpenerScript : MonoBehaviour
{
	// Token: 0x060013B8 RID: 5048 RVA: 0x000B9642 File Offset: 0x000B7842
	private void Start()
	{
		base.gameObject.layer = 1;
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x000B9650 File Offset: 0x000B7850
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();
			if (this.Student != null && !this.Student.Dying && !this.Door.Open && !this.Door.Locked)
			{
				this.Door.Student = this.Student;
				this.Door.OpenDoor();
			}
		}
	}

	// Token: 0x060013BA RID: 5050 RVA: 0x000B96D0 File Offset: 0x000B78D0
	private void OnTriggerStay(Collider other)
	{
		if (!this.Door.Open && other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();
			if (this.Student != null && !this.Student.Dying && !this.Door.Open && !this.Door.Locked)
			{
				this.Door.Student = this.Student;
				this.Door.OpenDoor();
			}
		}
	}

	// Token: 0x04001D48 RID: 7496
	public StudentScript Student;

	// Token: 0x04001D49 RID: 7497
	public DoorScript Door;
}
