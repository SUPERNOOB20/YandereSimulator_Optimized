﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000072 RID: 114
[Serializable]
public class EventDelegate
{
	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000334 RID: 820 RVA: 0x00020D69 File Offset: 0x0001EF69
	// (set) Token: 0x06000335 RID: 821 RVA: 0x00020D71 File Offset: 0x0001EF71
	public MonoBehaviour target
	{
		get
		{
			return this.mTarget;
		}
		set
		{
			this.mTarget = value;
			this.mCachedCallback = null;
			this.mRawDelegate = false;
			this.mCached = false;
			this.mMethod = null;
			this.mParameterInfos = null;
			this.mParameters = null;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000336 RID: 822 RVA: 0x00020DA4 File Offset: 0x0001EFA4
	// (set) Token: 0x06000337 RID: 823 RVA: 0x00020DAC File Offset: 0x0001EFAC
	public string methodName
	{
		get
		{
			return this.mMethodName;
		}
		set
		{
			this.mMethodName = value;
			this.mCachedCallback = null;
			this.mRawDelegate = false;
			this.mCached = false;
			this.mMethod = null;
			this.mParameterInfos = null;
			this.mParameters = null;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000338 RID: 824 RVA: 0x00020DDF File Offset: 0x0001EFDF
	public EventDelegate.Parameter[] parameters
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			return this.mParameters;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000339 RID: 825 RVA: 0x00020DF5 File Offset: 0x0001EFF5
	public bool isValid
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			return (this.mRawDelegate && this.mCachedCallback != null) || (this.mTarget != null && !string.IsNullOrEmpty(this.mMethodName));
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x0600033A RID: 826 RVA: 0x00020E38 File Offset: 0x0001F038
	public bool isEnabled
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRawDelegate && this.mCachedCallback != null)
			{
				return true;
			}
			if (this.mTarget == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = this.mTarget;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00020E8D File Offset: 0x0001F08D
	public EventDelegate()
	{
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00020E95 File Offset: 0x0001F095
	public EventDelegate(EventDelegate.Callback call)
	{
		this.Set(call);
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00020EA4 File Offset: 0x0001F0A4
	public EventDelegate(MonoBehaviour target, string methodName)
	{
		this.Set(target, methodName);
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00020EB4 File Offset: 0x0001F0B4
	private static string GetMethodName(EventDelegate.Callback callback)
	{
		return callback.Method.Name;
	}

	// Token: 0x0600033F RID: 831 RVA: 0x00020EC1 File Offset: 0x0001F0C1
	private static bool IsValid(EventDelegate.Callback callback)
	{
		return callback != null && callback.Method != null;
	}

	// Token: 0x06000340 RID: 832 RVA: 0x00020ED4 File Offset: 0x0001F0D4
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !this.isValid;
		}
		if (obj is EventDelegate.Callback)
		{
			EventDelegate.Callback callback = obj as EventDelegate.Callback;
			if (callback.Equals(this.mCachedCallback))
			{
				return true;
			}
			MonoBehaviour y = callback.Target as MonoBehaviour;
			return this.mTarget == y && string.Equals(this.mMethodName, EventDelegate.GetMethodName(callback));
		}
		else
		{
			if (obj is EventDelegate)
			{
				EventDelegate eventDelegate = obj as EventDelegate;
				return this.mTarget == eventDelegate.mTarget && string.Equals(this.mMethodName, eventDelegate.mMethodName);
			}
			return false;
		}
	}

	// Token: 0x06000341 RID: 833 RVA: 0x00020F72 File Offset: 0x0001F172
	public override int GetHashCode()
	{
		return EventDelegate.s_Hash;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x00020F7C File Offset: 0x0001F17C
	private void Set(EventDelegate.Callback call)
	{
		this.Clear();
		if (call != null && EventDelegate.IsValid(call))
		{
			this.mTarget = (call.Target as MonoBehaviour);
			if (this.mTarget == null)
			{
				this.mRawDelegate = true;
				this.mCachedCallback = call;
				this.mMethodName = null;
				return;
			}
			this.mMethodName = EventDelegate.GetMethodName(call);
			this.mRawDelegate = false;
		}
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00020FE2 File Offset: 0x0001F1E2
	public void Set(MonoBehaviour target, string methodName)
	{
		this.Clear();
		this.mTarget = target;
		this.mMethodName = methodName;
	}

	// Token: 0x06000344 RID: 836 RVA: 0x00020FF8 File Offset: 0x0001F1F8
	private void Cache()
	{
		this.mCached = true;
		if (this.mRawDelegate)
		{
			return;
		}
		if ((this.mCachedCallback == null || this.mCachedCallback.Target as MonoBehaviour != this.mTarget || EventDelegate.GetMethodName(this.mCachedCallback) != this.mMethodName) && this.mTarget != null && !string.IsNullOrEmpty(this.mMethodName))
		{
			Type type = this.mTarget.GetType();
			this.mMethod = null;
			while (type != null)
			{
				try
				{
					this.mMethod = type.GetMethod(this.mMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (this.mMethod != null)
					{
						break;
					}
				}
				catch (Exception)
				{
				}
				type = type.BaseType;
			}
			if (this.mMethod == null)
			{
				string str = "Could not find method '";
				string str2 = this.mMethodName;
				string str3 = "' on ";
				Type type2 = this.mTarget.GetType();
				Debug.LogError(str + str2 + str3 + ((type2 != null) ? type2.ToString() : null), this.mTarget);
				return;
			}
			if (this.mMethod.ReturnType != typeof(void))
			{
				Type type3 = this.mTarget.GetType();
				Debug.LogError(((type3 != null) ? type3.ToString() : null) + "." + this.mMethodName + " must have a 'void' return type.", this.mTarget);
				return;
			}
			this.mParameterInfos = this.mMethod.GetParameters();
			if (this.mParameterInfos.Length == 0)
			{
				this.mCachedCallback = (EventDelegate.Callback)Delegate.CreateDelegate(typeof(EventDelegate.Callback), this.mTarget, this.mMethodName);
				this.mArgs = null;
				this.mParameters = null;
				return;
			}
			this.mCachedCallback = null;
			if (this.mParameters == null || this.mParameters.Length != this.mParameterInfos.Length)
			{
				this.mParameters = new EventDelegate.Parameter[this.mParameterInfos.Length];
				int i = 0;
				int num = this.mParameters.Length;
				while (i < num)
				{
					this.mParameters[i] = new EventDelegate.Parameter();
					i++;
				}
			}
			int j = 0;
			int num2 = this.mParameters.Length;
			while (j < num2)
			{
				this.mParameters[j].expectedType = this.mParameterInfos[j].ParameterType;
				j++;
			}
		}
	}

	// Token: 0x06000345 RID: 837 RVA: 0x00021248 File Offset: 0x0001F448
	public bool Execute()
	{
		if (!this.mCached)
		{
			this.Cache();
		}
		if (this.mCachedCallback != null)
		{
			this.mCachedCallback();
			return true;
		}
		if (this.mMethod != null)
		{
			if (this.mParameters == null || this.mParameters.Length == 0)
			{
				this.mMethod.Invoke(this.mTarget, null);
			}
			else
			{
				if (this.mArgs == null || this.mArgs.Length != this.mParameters.Length)
				{
					this.mArgs = new object[this.mParameters.Length];
				}
				int i = 0;
				int num = this.mParameters.Length;
				while (i < num)
				{
					this.mArgs[i] = this.mParameters[i].value;
					i++;
				}
				try
				{
					this.mMethod.Invoke(this.mTarget, this.mArgs);
				}
				catch (ArgumentException ex)
				{
					string text = "Error calling ";
					if (this.mTarget == null)
					{
						text += this.mMethod.Name;
					}
					else
					{
						string str = text;
						Type type = this.mTarget.GetType();
						text = str + ((type != null) ? type.ToString() : null) + "." + this.mMethod.Name;
					}
					text = text + ": " + ex.Message;
					text += "\n  Expected: ";
					if (this.mParameterInfos.Length == 0)
					{
						text += "no arguments";
					}
					else
					{
						string str2 = text;
						ParameterInfo parameterInfo = this.mParameterInfos[0];
						text = str2 + ((parameterInfo != null) ? parameterInfo.ToString() : null);
						for (int j = 1; j < this.mParameterInfos.Length; j++)
						{
							string str3 = text;
							string str4 = ", ";
							Type parameterType = this.mParameterInfos[j].ParameterType;
							text = str3 + str4 + ((parameterType != null) ? parameterType.ToString() : null);
						}
					}
					text += "\n  Received: ";
					if (this.mParameters.Length == 0)
					{
						text += "no arguments";
					}
					else
					{
						string str5 = text;
						Type type2 = this.mParameters[0].type;
						text = str5 + ((type2 != null) ? type2.ToString() : null);
						for (int k = 1; k < this.mParameters.Length; k++)
						{
							string str6 = text;
							string str7 = ", ";
							Type type3 = this.mParameters[k].type;
							text = str6 + str7 + ((type3 != null) ? type3.ToString() : null);
						}
					}
					text += "\n";
					Debug.LogError(text);
				}
				int l = 0;
				int num2 = this.mArgs.Length;
				while (l < num2)
				{
					if (this.mParameterInfos[l].IsIn || this.mParameterInfos[l].IsOut)
					{
						this.mParameters[l].value = this.mArgs[l];
					}
					this.mArgs[l] = null;
					l++;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000346 RID: 838 RVA: 0x00021520 File Offset: 0x0001F720
	public void Clear()
	{
		this.mTarget = null;
		this.mMethodName = null;
		this.mRawDelegate = false;
		this.mCachedCallback = null;
		this.mParameters = null;
		this.mCached = false;
		this.mMethod = null;
		this.mParameterInfos = null;
		this.mArgs = null;
	}

	// Token: 0x06000347 RID: 839 RVA: 0x0002156C File Offset: 0x0001F76C
	public override string ToString()
	{
		if (this.mTarget != null)
		{
			string text = this.mTarget.GetType().ToString();
			int num = text.LastIndexOf('.');
			if (num > 0)
			{
				text = text.Substring(num + 1);
			}
			if (!string.IsNullOrEmpty(this.methodName))
			{
				return text + "/" + this.methodName;
			}
			return text + "/[delegate]";
		}
		else
		{
			if (!this.mRawDelegate)
			{
				return null;
			}
			return "[delegate]";
		}
	}

	// Token: 0x06000348 RID: 840 RVA: 0x000215EC File Offset: 0x0001F7EC
	public static void Execute(List<EventDelegate> list)
	{
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null)
				{
					try
					{
						eventDelegate.Execute();
					}
					catch (Exception ex)
					{
						if (ex.InnerException != null)
						{
							Debug.LogException(ex.InnerException);
						}
						else
						{
							Debug.LogException(ex);
						}
					}
					if (i >= list.Count)
					{
						break;
					}
					if (list[i] != eventDelegate)
					{
						continue;
					}
					if (eventDelegate.oneShot)
					{
						list.RemoveAt(i);
						continue;
					}
				}
			}
		}
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00021674 File Offset: 0x0001F874
	public static bool IsValid(List<EventDelegate> list)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.isValid)
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x0600034A RID: 842 RVA: 0x000216B0 File Offset: 0x0001F8B0
	public static EventDelegate Set(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		if (list != null)
		{
			EventDelegate eventDelegate = new EventDelegate(callback);
			list.Clear();
			list.Add(eventDelegate);
			return eventDelegate;
		}
		return null;
	}

	// Token: 0x0600034B RID: 843 RVA: 0x000216D7 File Offset: 0x0001F8D7
	public static void Set(List<EventDelegate> list, EventDelegate del)
	{
		if (list != null)
		{
			list.Clear();
			list.Add(del);
		}
	}

	// Token: 0x0600034C RID: 844 RVA: 0x000216E9 File Offset: 0x0001F8E9
	public static EventDelegate Add(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		return EventDelegate.Add(list, callback, false);
	}

	// Token: 0x0600034D RID: 845 RVA: 0x000216F4 File Offset: 0x0001F8F4
	public static EventDelegate Add(List<EventDelegate> list, EventDelegate.Callback callback, bool oneShot)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(callback))
				{
					return eventDelegate;
				}
				i++;
			}
			EventDelegate eventDelegate2 = new EventDelegate(callback);
			eventDelegate2.oneShot = oneShot;
			list.Add(eventDelegate2);
			return eventDelegate2;
		}
		Debug.LogWarning("Attempting to add a callback to a list that's null");
		return null;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0002174F File Offset: 0x0001F94F
	public static void Add(List<EventDelegate> list, EventDelegate ev)
	{
		EventDelegate.Add(list, ev, ev.oneShot);
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00021760 File Offset: 0x0001F960
	public static void Add(List<EventDelegate> list, EventDelegate ev, bool oneShot)
	{
		if (ev.mRawDelegate || ev.target == null || string.IsNullOrEmpty(ev.methodName))
		{
			EventDelegate.Add(list, ev.mCachedCallback, oneShot);
			return;
		}
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(ev))
				{
					return;
				}
				i++;
			}
			EventDelegate eventDelegate2 = new EventDelegate(ev.target, ev.methodName);
			eventDelegate2.oneShot = oneShot;
			if (ev.mParameters != null && ev.mParameters.Length != 0)
			{
				eventDelegate2.mParameters = new EventDelegate.Parameter[ev.mParameters.Length];
				for (int j = 0; j < ev.mParameters.Length; j++)
				{
					eventDelegate2.mParameters[j] = ev.mParameters[j];
				}
			}
			list.Add(eventDelegate2);
			return;
		}
		Debug.LogWarning("Attempting to add a callback to a list that's null");
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00021848 File Offset: 0x0001FA48
	public static bool Remove(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(callback))
				{
					list.RemoveAt(i);
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0002188C File Offset: 0x0001FA8C
	public static bool Remove(List<EventDelegate> list, EventDelegate ev)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(ev))
				{
					list.RemoveAt(i);
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x040004A9 RID: 1193
	[SerializeField]
	private MonoBehaviour mTarget;

	// Token: 0x040004AA RID: 1194
	[SerializeField]
	private string mMethodName;

	// Token: 0x040004AB RID: 1195
	[SerializeField]
	private EventDelegate.Parameter[] mParameters;

	// Token: 0x040004AC RID: 1196
	public bool oneShot;

	// Token: 0x040004AD RID: 1197
	[NonSerialized]
	private EventDelegate.Callback mCachedCallback;

	// Token: 0x040004AE RID: 1198
	[NonSerialized]
	private bool mRawDelegate;

	// Token: 0x040004AF RID: 1199
	[NonSerialized]
	private bool mCached;

	// Token: 0x040004B0 RID: 1200
	[NonSerialized]
	private MethodInfo mMethod;

	// Token: 0x040004B1 RID: 1201
	[NonSerialized]
	private ParameterInfo[] mParameterInfos;

	// Token: 0x040004B2 RID: 1202
	[NonSerialized]
	private object[] mArgs;

	// Token: 0x040004B3 RID: 1203
	private static int s_Hash = "EventDelegate".GetHashCode();

	// Token: 0x020005F0 RID: 1520
	[Serializable]
	public class Parameter
	{
		// Token: 0x0600257A RID: 9594 RVA: 0x00202612 File Offset: 0x00200812
		public Parameter()
		{
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x0020262A File Offset: 0x0020082A
		public Parameter(UnityEngine.Object obj, string field)
		{
			this.obj = obj;
			this.field = field;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x00202650 File Offset: 0x00200850
		public Parameter(object val)
		{
			this.mValue = val;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x00202670 File Offset: 0x00200870
		// (set) Token: 0x0600257E RID: 9598 RVA: 0x00202781 File Offset: 0x00200981
		public object value
		{
			get
			{
				if (this.mValue != null)
				{
					return this.mValue;
				}
				if (!this.cached)
				{
					this.cached = true;
					this.fieldInfo = null;
					this.propInfo = null;
					if (this.obj != null && !string.IsNullOrEmpty(this.field))
					{
						Type type = this.obj.GetType();
						this.propInfo = type.GetProperty(this.field);
						if (this.propInfo == null)
						{
							this.fieldInfo = type.GetField(this.field);
						}
					}
				}
				if (this.propInfo != null)
				{
					return this.propInfo.GetValue(this.obj, null);
				}
				if (this.fieldInfo != null)
				{
					return this.fieldInfo.GetValue(this.obj);
				}
				if (this.obj != null)
				{
					return this.obj;
				}
				if (this.expectedType != null && this.expectedType.IsValueType)
				{
					return null;
				}
				return Convert.ChangeType(null, this.expectedType);
			}
			set
			{
				this.mValue = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x0020278A File Offset: 0x0020098A
		public Type type
		{
			get
			{
				if (this.mValue != null)
				{
					return this.mValue.GetType();
				}
				if (this.obj == null)
				{
					return typeof(void);
				}
				return this.obj.GetType();
			}
		}

		// Token: 0x04004E51 RID: 20049
		public UnityEngine.Object obj;

		// Token: 0x04004E52 RID: 20050
		public string field;

		// Token: 0x04004E53 RID: 20051
		[NonSerialized]
		private object mValue;

		// Token: 0x04004E54 RID: 20052
		[NonSerialized]
		public Type expectedType = typeof(void);

		// Token: 0x04004E55 RID: 20053
		[NonSerialized]
		public bool cached;

		// Token: 0x04004E56 RID: 20054
		[NonSerialized]
		public PropertyInfo propInfo;

		// Token: 0x04004E57 RID: 20055
		[NonSerialized]
		public FieldInfo fieldInfo;
	}

	// Token: 0x020005F1 RID: 1521
	// (Invoke) Token: 0x06002581 RID: 9601
	public delegate void Callback();
}
