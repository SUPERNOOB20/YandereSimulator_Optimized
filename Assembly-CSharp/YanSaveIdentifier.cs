﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000519 RID: 1305
public class YanSaveIdentifier : MonoBehaviour
{
	// Token: 0x0600217A RID: 8570 RVA: 0x001EDB38 File Offset: 0x001EBD38
	public static GameObject GetObject(string id)
	{
		foreach (YanSaveIdentifier yanSaveIdentifier in YanSaveIdentifier.Identifiers)
		{
			if (yanSaveIdentifier.ObjectID == id)
			{
				return yanSaveIdentifier.gameObject;
			}
		}
		return null;
	}

	// Token: 0x0600217B RID: 8571 RVA: 0x001EDBA0 File Offset: 0x001EBDA0
	public static GameObject GetObject(SerializedGameObject serializedGameObject)
	{
		foreach (YanSaveIdentifier yanSaveIdentifier in YanSaveIdentifier.Identifiers)
		{
			if (yanSaveIdentifier.ObjectID == serializedGameObject.ObjectID)
			{
				return yanSaveIdentifier.gameObject;
			}
		}
		return null;
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x001EDC0C File Offset: 0x001EBE0C
	public void OnEnable()
	{
		if (!YanSaveIdentifier.Identifiers.Contains(this))
		{
			YanSaveIdentifier.Identifiers.Add(this);
		}
		if (string.IsNullOrEmpty(this.ObjectID))
		{
			this.ObjectID = base.gameObject.name;
		}
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x001EDC44 File Offset: 0x001EBE44
	public void OnDestroy()
	{
		YanSaveIdentifier.Identifiers.Remove(this);
	}

	// Token: 0x040049B4 RID: 18868
	public string ObjectID;

	// Token: 0x040049B5 RID: 18869
	[HideInInspector]
	public bool AutoGenerated;

	// Token: 0x040049B6 RID: 18870
	[HideInInspector]
	public List<Component> EnabledComponents = new List<Component>();

	// Token: 0x040049B7 RID: 18871
	[HideInInspector]
	public List<DisabledYanSaveMember> DisabledProperties = new List<DisabledYanSaveMember>();

	// Token: 0x040049B8 RID: 18872
	[HideInInspector]
	public List<DisabledYanSaveMember> DisabledFields = new List<DisabledYanSaveMember>();

	// Token: 0x040049B9 RID: 18873
	[HideInInspector]
	public bool InitializedInspector;

	// Token: 0x040049BA RID: 18874
	private static List<YanSaveIdentifier> Identifiers = new List<YanSaveIdentifier>();
}
