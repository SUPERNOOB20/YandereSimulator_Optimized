﻿using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class ExampleWheelController : MonoBehaviour
{
	// Token: 0x06000963 RID: 2403 RVA: 0x0004B3C7 File Offset: 0x000495C7
	private void Start()
	{
		this.m_Rigidbody = base.GetComponent<Rigidbody>();
		this.m_Rigidbody.maxAngularVelocity = 100f;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0004B3E8 File Offset: 0x000495E8
	private void Update()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.m_Rigidbody.AddRelativeTorque(new Vector3(-1f * this.acceleration, 0f, 0f), ForceMode.Acceleration);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.m_Rigidbody.AddRelativeTorque(new Vector3(1f * this.acceleration, 0f, 0f), ForceMode.Acceleration);
		}
		float value = -this.m_Rigidbody.angularVelocity.x / 100f;
		if (this.motionVectorRenderer)
		{
			this.motionVectorRenderer.material.SetFloat(ExampleWheelController.Uniforms._MotionAmount, Mathf.Clamp(value, -0.25f, 0.25f));
		}
	}

	// Token: 0x0400080A RID: 2058
	public float acceleration;

	// Token: 0x0400080B RID: 2059
	public Renderer motionVectorRenderer;

	// Token: 0x0400080C RID: 2060
	private Rigidbody m_Rigidbody;

	// Token: 0x02000647 RID: 1607
	private static class Uniforms
	{
		// Token: 0x04004EBB RID: 20155
		internal static readonly int _MotionAmount = Shader.PropertyToID("_MotionAmount");
	}
}
