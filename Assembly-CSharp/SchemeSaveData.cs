﻿using System;
using System.Collections.Generic;

// Token: 0x02000407 RID: 1031
[Serializable]
public class SchemeSaveData
{
	// Token: 0x06001C38 RID: 7224 RVA: 0x00149504 File Offset: 0x00147704
	public static SchemeSaveData ReadFromGlobals()
	{
		SchemeSaveData schemeSaveData = new SchemeSaveData();
		schemeSaveData.currentScheme = SchemeGlobals.CurrentScheme;
		schemeSaveData.darkSecret = SchemeGlobals.EmbarassingSecret;
		foreach (int num in SchemeGlobals.KeysOfSchemePreviousStage())
		{
			schemeSaveData.schemePreviousStage.Add(num, SchemeGlobals.GetSchemePreviousStage(num));
		}
		foreach (int num2 in SchemeGlobals.KeysOfSchemeStage())
		{
			schemeSaveData.schemeStage.Add(num2, SchemeGlobals.GetSchemeStage(num2));
		}
		foreach (int num3 in SchemeGlobals.KeysOfSchemeStatus())
		{
			if (SchemeGlobals.GetSchemeStatus(num3))
			{
				schemeSaveData.schemeStatus.Add(num3);
			}
		}
		foreach (int num4 in SchemeGlobals.KeysOfSchemeUnlocked())
		{
			if (SchemeGlobals.GetSchemeUnlocked(num4))
			{
				schemeSaveData.schemeUnlocked.Add(num4);
			}
		}
		foreach (int num5 in SchemeGlobals.KeysOfServicePurchased())
		{
			if (SchemeGlobals.GetServicePurchased(num5))
			{
				schemeSaveData.servicePurchased.Add(num5);
			}
		}
		return schemeSaveData;
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x00149618 File Offset: 0x00147818
	public static void WriteToGlobals(SchemeSaveData data)
	{
		SchemeGlobals.CurrentScheme = data.currentScheme;
		SchemeGlobals.EmbarassingSecret = data.darkSecret;
		foreach (KeyValuePair<int, int> keyValuePair in data.schemePreviousStage)
		{
			SchemeGlobals.SetSchemePreviousStage(keyValuePair.Key, keyValuePair.Value);
		}
		foreach (KeyValuePair<int, int> keyValuePair2 in data.schemeStage)
		{
			SchemeGlobals.SetSchemeStage(keyValuePair2.Key, keyValuePair2.Value);
		}
		foreach (int schemeID in data.schemeStatus)
		{
			SchemeGlobals.SetSchemeStatus(schemeID, true);
		}
		foreach (int schemeID2 in data.schemeUnlocked)
		{
			SchemeGlobals.SetSchemeUnlocked(schemeID2, true);
		}
		foreach (int serviceID in data.servicePurchased)
		{
			SchemeGlobals.SetServicePurchased(serviceID, true);
		}
	}

	// Token: 0x040031C3 RID: 12739
	public int currentScheme;

	// Token: 0x040031C4 RID: 12740
	public bool darkSecret;

	// Token: 0x040031C5 RID: 12741
	public IntAndIntDictionary schemePreviousStage = new IntAndIntDictionary();

	// Token: 0x040031C6 RID: 12742
	public IntAndIntDictionary schemeStage = new IntAndIntDictionary();

	// Token: 0x040031C7 RID: 12743
	public IntHashSet schemeStatus = new IntHashSet();

	// Token: 0x040031C8 RID: 12744
	public IntHashSet schemeUnlocked = new IntHashSet();

	// Token: 0x040031C9 RID: 12745
	public IntHashSet servicePurchased = new IntHashSet();
}
