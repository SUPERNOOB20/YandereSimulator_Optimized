﻿using System;
using UnityEngine;

// Token: 0x02000337 RID: 823
public class InputManagerScript : MonoBehaviour
{
	// Token: 0x060018EB RID: 6379 RVA: 0x000F686C File Offset: 0x000F4A6C
	private void Update()
	{
		this.TappedUp = false;
		this.TappedDown = false;
		this.TappedRight = false;
		this.TappedLeft = false;
		if (Input.GetAxisRaw("DpadY") > 0.5f)
		{
			this.TappedUp = !this.DPadUp;
			this.DPadUp = true;
		}
		else if (Input.GetAxisRaw("DpadY") < -0.5f)
		{
			this.TappedDown = !this.DPadDown;
			this.DPadDown = true;
		}
		else
		{
			this.DPadUp = false;
			this.DPadDown = false;
		}
		if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
		{
			if (Input.GetAxisRaw("Vertical") > 0.5f)
			{
				this.TappedUp = !this.StickUp;
				this.StickUp = !this.TappedDown;
			}
			else if (Input.GetAxisRaw("Vertical") < -0.5f)
			{
				this.TappedDown = !this.StickDown;
				this.StickDown = !this.TappedUp;
			}
			else
			{
				this.StickUp = false;
				this.StickDown = false;
			}
		}
		if (Input.GetAxisRaw("DpadX") > 0.5f)
		{
			this.TappedRight = !this.DPadRight;
			this.DPadRight = true;
		}
		else if (Input.GetAxisRaw("DpadX") < -0.5f)
		{
			this.TappedLeft = !this.DPadLeft;
			this.DPadLeft = true;
		}
		else
		{
			this.DPadRight = false;
			this.DPadLeft = false;
		}
		if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			if (Input.GetAxisRaw("Horizontal") > 0.5f)
			{
				this.TappedRight = !this.StickRight;
				this.StickRight = true;
			}
			else if (Input.GetAxisRaw("Horizontal") < -0.5f)
			{
				this.TappedLeft = !this.StickLeft;
				this.StickLeft = true;
			}
			else
			{
				this.StickRight = false;
				this.StickLeft = false;
			}
		}
		if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f && Input.GetAxisRaw("DpadX") < 0.5f && Input.GetAxisRaw("DpadX") > -0.5f)
		{
			this.TappedRight = false;
			this.TappedLeft = false;
		}
		if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f && Input.GetAxisRaw("DpadY") < 0.5f && Input.GetAxisRaw("DpadY") > -0.5f)
		{
			this.TappedUp = false;
			this.TappedDown = false;
		}
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.TappedUp = true;
			this.NoStick();
		}
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			this.TappedDown = true;
			this.NoStick();
		}
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.TappedLeft = true;
			this.NoStick();
		}
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.TappedRight = true;
			this.NoStick();
		}
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x000F6B75 File Offset: 0x000F4D75
	private void NoStick()
	{
		this.StickUp = false;
		this.StickDown = false;
		this.StickLeft = false;
		this.StickRight = false;
	}

	// Token: 0x0400265B RID: 9819
	public bool TappedUp;

	// Token: 0x0400265C RID: 9820
	public bool TappedDown;

	// Token: 0x0400265D RID: 9821
	public bool TappedRight;

	// Token: 0x0400265E RID: 9822
	public bool TappedLeft;

	// Token: 0x0400265F RID: 9823
	public bool DPadUp;

	// Token: 0x04002660 RID: 9824
	public bool DPadDown;

	// Token: 0x04002661 RID: 9825
	public bool DPadRight;

	// Token: 0x04002662 RID: 9826
	public bool DPadLeft;

	// Token: 0x04002663 RID: 9827
	public bool StickUp;

	// Token: 0x04002664 RID: 9828
	public bool StickDown;

	// Token: 0x04002665 RID: 9829
	public bool StickRight;

	// Token: 0x04002666 RID: 9830
	public bool StickLeft;
}
