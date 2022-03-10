﻿using System;
using UnityEngine;

// Token: 0x02000454 RID: 1108
public class StudentInfoScript : MonoBehaviour
{
	// Token: 0x06001D50 RID: 7504 RVA: 0x00160EF8 File Offset: 0x0015F0F8
	private void Start()
	{
		StudentGlobals.SetStudentPhotographed(98, true);
		StudentGlobals.SetStudentPhotographed(99, true);
		StudentGlobals.SetStudentPhotographed(100, true);
		this.Topics.SetActive(false);
		this.Eighties = GameGlobals.Eighties;
		if (this.Eighties)
		{
			this.InfoLabel.transform.localPosition += new Vector3(0f, -10f, 0f);
		}
		if (!this.UpdatedOnce)
		{
			this.UpdateInfo(this.StudentInfoMenu.StudentID);
		}
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x00160F84 File Offset: 0x0015F184
	public void UpdateInfo(int ID)
	{
		if (!this.UpdatedOnce)
		{
			this.Eighties = GameGlobals.Eighties;
		}
		this.UpdatedOnce = true;
		StudentJson studentJson = this.JSON.Students[ID];
		if (studentJson.RealName == "")
		{
			this.NameLabel.transform.localPosition = new Vector3(-228f, 195f, 0f);
			this.RealNameLabel.text = "";
		}
		else
		{
			this.NameLabel.transform.localPosition = new Vector3(-228f, 210f, 0f);
			this.RealNameLabel.text = "Real Name: " + studentJson.RealName;
		}
		this.NameLabel.text = studentJson.Name;
		string text = studentJson.Class.ToString() ?? "";
		text = text.Insert(1, "-");
		this.ClassLabel.text = "Class " + text;
		if (ID == 90 || ID > 96)
		{
			this.ClassLabel.text = "";
		}
		float num;
		if (this.StudentManager != null)
		{
			num = this.StudentManager.StudentReps[ID];
		}
		else
		{
			num = (float)StudentGlobals.GetStudentReputation(ID);
		}
		if (num < 0f)
		{
			this.ReputationLabel.text = (num.ToString() ?? "");
		}
		else if (num > 0f)
		{
			this.ReputationLabel.text = "+" + num.ToString();
		}
		else
		{
			this.ReputationLabel.text = "0";
		}
		this.ReputationBar.localPosition = new Vector3(num * 0.96f, this.ReputationBar.localPosition.y, this.ReputationBar.localPosition.z);
		if (this.ReputationBar.localPosition.x > 96f)
		{
			this.ReputationBar.localPosition = new Vector3(96f, this.ReputationBar.localPosition.y, this.ReputationBar.localPosition.z);
		}
		if (this.ReputationBar.localPosition.x < -96f)
		{
			this.ReputationBar.localPosition = new Vector3(-96f, this.ReputationBar.localPosition.y, this.ReputationBar.localPosition.z);
		}
		this.PersonaLabel.text = Persona.PersonaNames[studentJson.Persona];
		if (studentJson.Persona == PersonaType.Strict && studentJson.Club == ClubType.GymTeacher && !StudentGlobals.GetStudentReplaced(ID))
		{
			this.PersonaLabel.text = "Friendly but Strict";
		}
		if (studentJson.Crush == 0)
		{
			this.CrushLabel.text = "None";
		}
		else if (studentJson.Crush == 99)
		{
			this.CrushLabel.text = "?????";
		}
		else
		{
			this.CrushLabel.text = this.JSON.Students[studentJson.Crush].Name;
		}
		if (studentJson.Club < ClubType.Teacher)
		{
			this.OccupationLabel.text = "Club";
		}
		else
		{
			this.OccupationLabel.text = "Occupation";
		}
		if (studentJson.Club < ClubType.Teacher)
		{
			this.ClubLabel.text = Club.ClubNames[studentJson.Club];
		}
		else
		{
			this.ClubLabel.text = Club.TeacherClubNames[studentJson.Class];
		}
		if (ClubGlobals.GetClubClosed(studentJson.Club))
		{
			this.ClubLabel.text = "No Club";
		}
		this.StrengthLabel.text = StudentInfoScript.StrengthStrings[studentJson.Strength];
		AudioSource component = base.GetComponent<AudioSource>();
		component.enabled = false;
		this.Static.SetActive(false);
		component.volume = 0f;
		component.Stop();
		string text2 = "";
		if (this.Eighties)
		{
			text2 = "1989";
		}
		if (ID < 98)
		{
			if (this.Eighties || (!this.Eighties && ID < 12) || (!this.Eighties && ID > 20))
			{
				WWW www = new WWW(string.Concat(new string[]
				{
					"file:///",
					Application.streamingAssetsPath,
					"/Portraits",
					text2,
					"/Student_",
					ID.ToString(),
					".png"
				}));
				if (!StudentGlobals.GetStudentReplaced(ID))
				{
					this.Portrait.mainTexture = www.texture;
				}
				else
				{
					this.Portrait.mainTexture = this.BlankPortrait;
				}
			}
			else
			{
				this.Portrait.mainTexture = this.StudentInfoMenu.RivalPortraits[ID];
			}
		}
		else if (ID == 98)
		{
			this.Portrait.mainTexture = this.StudentInfoMenu.Counselor;
		}
		else if (ID == 99)
		{
			this.Portrait.mainTexture = this.StudentInfoMenu.Headmaster;
		}
		else if (ID == 100)
		{
			this.Portrait.mainTexture = this.StudentInfoMenu.InfoChan;
			if (!this.Eighties)
			{
				this.Static.SetActive(true);
				if (!this.StudentInfoMenu.Gossiping && !this.StudentInfoMenu.Distracting && !this.StudentInfoMenu.CyberBullying && !this.StudentInfoMenu.CyberStalking)
				{
					component.enabled = true;
					component.volume = 1f;
					component.Play();
				}
			}
		}
		this.UpdateAdditionalInfo(ID);
		this.CurrentStudent = ID;
		this.UpdateRepChart();
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x00161518 File Offset: 0x0015F718
	private void Update()
	{
		if (this.CurrentStudent == 100)
		{
			this.UpdateRepChart();
		}
		if (Input.GetButtonDown("A"))
		{
			if (this.StudentInfoMenu.Gossiping)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.DialogueWheel.Victim = this.CurrentStudent;
				this.StudentInfoMenu.Gossiping = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 0.0001f;
				this.DialogueWheel.TopicInterface.Socializing = false;
				this.DialogueWheel.TopicInterface.StudentID = this.Yandere.TargetStudent.StudentID;
				this.DialogueWheel.TopicInterface.Student = this.Yandere.TargetStudent;
				this.DialogueWheel.TopicInterface.TargetStudentID = this.CurrentStudent;
				this.DialogueWheel.TopicInterface.TargetStudent = this.StudentManager.Students[this.CurrentStudent];
				this.DialogueWheel.TopicInterface.UpdateOpinions();
				this.DialogueWheel.TopicInterface.UpdateTopicHighlight();
				this.DialogueWheel.TopicInterface.gameObject.SetActive(true);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Speak";
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.Label[2].text = "Positive/Negative";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
			else if (this.StudentInfoMenu.Distracting)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.DialogueWheel.Victim = this.CurrentStudent;
				this.StudentInfoMenu.Distracting = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.CyberBullying)
			{
				this.HomeInternet.PostLabels[1].text = this.JSON.Students[this.CurrentStudent].Name;
				this.HomeInternet.Student = this.CurrentStudent;
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.StudentInfoMenu.CyberBullying = false;
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.CyberStalking)
			{
				this.HomeInternet.HomeCamera.CyberstalkWindow.SetActive(true);
				this.HomeInternet.Student = this.CurrentStudent;
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.StudentInfoMenu.CyberStalking = false;
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.MatchMaking)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.DialogueWheel.Victim = this.CurrentStudent;
				this.StudentInfoMenu.MatchMaking = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.Targeting)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.Yandere.TargetStudent.HuntTarget = this.StudentManager.Students[this.CurrentStudent];
				this.Yandere.TargetStudent.HuntTarget.Hunted = true;
				this.Yandere.TargetStudent.GoCommitMurder();
				this.Yandere.RPGCamera.enabled = true;
				this.Yandere.TargetStudent = null;
				this.StudentInfoMenu.Targeting = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.SendingHome)
			{
				if (this.CurrentStudent == 10 || this.CurrentStudent == this.StudentManager.RivalID)
				{
					this.StudentInfoMenu.PauseScreen.ServiceMenu.TextMessageManager.SpawnMessage(11);
					base.gameObject.SetActive(false);
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = string.Empty;
					this.PromptBar.Label[1].text = "Back";
					this.PromptBar.UpdateButtons();
				}
				else if (this.StudentManager.Students[this.CurrentStudent].Routine && !this.StudentManager.Students[this.CurrentStudent].InEvent && !this.StudentManager.Students[this.CurrentStudent].TargetedForDistraction && this.StudentManager.Students[this.CurrentStudent].ClubActivityPhase < 16 && !this.StudentManager.Students[this.CurrentStudent].MyBento.Tampered)
				{
					this.StudentManager.Students[this.CurrentStudent].Routine = false;
					this.StudentManager.Students[this.CurrentStudent].SentHome = true;
					this.StudentManager.Students[this.CurrentStudent].CameraReacting = false;
					this.StudentManager.Students[this.CurrentStudent].SpeechLines.Stop();
					this.StudentManager.Students[this.CurrentStudent].EmptyHands();
					this.StudentInfoMenu.PauseScreen.ServiceMenu.gameObject.SetActive(true);
					this.StudentInfoMenu.PauseScreen.ServiceMenu.UpdateList();
					this.StudentInfoMenu.PauseScreen.ServiceMenu.UpdateDesc();
					this.StudentInfoMenu.PauseScreen.ServiceMenu.Purchase();
					this.StudentInfoMenu.SendingHome = false;
					base.gameObject.SetActive(false);
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
				}
				else
				{
					this.StudentInfoMenu.PauseScreen.ServiceMenu.TextMessageManager.SpawnMessage(0);
					base.gameObject.SetActive(false);
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = string.Empty;
					this.PromptBar.Label[1].text = "Back";
					this.PromptBar.UpdateButtons();
				}
			}
			else if (this.StudentInfoMenu.FindingLocker)
			{
				this.NoteLocker.gameObject.SetActive(true);
				this.NoteLocker.transform.position = this.StudentManager.Students[this.StudentInfoMenu.StudentID].MyLocker.position;
				this.NoteLocker.transform.position += new Vector3(0f, 1.355f, 0f);
				this.NoteLocker.transform.position += this.StudentManager.Students[this.StudentInfoMenu.StudentID].MyLocker.forward * 0.33333f;
				this.NoteLocker.Prompt.Label[0].text = "     Leave note for " + this.StudentManager.Students[this.StudentInfoMenu.StudentID].Name;
				this.NoteLocker.Student = this.StudentManager.Students[this.StudentInfoMenu.StudentID];
				this.NoteLocker.LockerOwner = this.StudentInfoMenu.StudentID;
				this.NoteLocker.Prompt.enabled = true;
				this.NoteLocker.transform.GetChild(0).gameObject.SetActive(true);
				this.NoteLocker.CheckingNote = false;
				this.NoteLocker.CanLeaveNote = true;
				this.NoteLocker.SpawnedNote = false;
				this.NoteLocker.NoteLeft = false;
				this.NoteLocker.Success = false;
				this.NoteLocker.Timer = 0f;
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.StudentInfoMenu.FindingLocker = false;
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Yandere.RPGCamera.enabled = true;
				Time.timeScale = 1f;
				if (this.StudentInfoMenu.StudentID == 11 && SchemeGlobals.GetSchemeStage(6) == 4)
				{
					SchemeGlobals.SetSchemeStage(6, 5);
					this.Yandere.PauseScreen.Schemes.UpdateInstructions();
				}
			}
			else if (this.StudentInfoMenu.FiringCouncilMember)
			{
				if (this.StudentManager.Students[this.CurrentStudent].Routine && !this.StudentManager.Students[this.CurrentStudent].InEvent && !this.StudentManager.Students[this.CurrentStudent].TargetedForDistraction && this.StudentManager.Students[this.CurrentStudent].ClubActivityPhase < 16 && !this.StudentManager.Students[this.CurrentStudent].MyBento.Tampered)
				{
					this.StudentManager.Students[this.CurrentStudent].OriginalPersona = PersonaType.Heroic;
					this.StudentManager.Students[this.CurrentStudent].Persona = PersonaType.Heroic;
					this.StudentManager.Students[this.CurrentStudent].Club = ClubType.None;
					this.StudentManager.Students[this.CurrentStudent].CameraReacting = false;
					this.StudentManager.Students[this.CurrentStudent].SpeechLines.Stop();
					this.StudentManager.Students[this.CurrentStudent].EmptyHands();
					this.StudentManager.Students[this.CurrentStudent].IdleAnim = this.StudentManager.Students[this.CurrentStudent].BulliedIdleAnim;
					this.StudentManager.Students[this.CurrentStudent].WalkAnim = this.StudentManager.Students[this.CurrentStudent].BulliedWalkAnim;
					this.StudentManager.Students[this.CurrentStudent].Armband.SetActive(false);
					StudentScript studentScript = this.StudentManager.Students[this.CurrentStudent];
					ScheduleBlock scheduleBlock = studentScript.ScheduleBlocks[3];
					scheduleBlock.destination = "LunchSpot";
					scheduleBlock.action = "Eat";
					studentScript.GetDestinations();
					studentScript.CurrentDestination = studentScript.Destinations[studentScript.Phase];
					studentScript.Pathfinding.target = studentScript.Destinations[studentScript.Phase];
					this.StudentInfoMenu.PauseScreen.ServiceMenu.gameObject.SetActive(true);
					this.StudentInfoMenu.PauseScreen.ServiceMenu.UpdateList();
					this.StudentInfoMenu.PauseScreen.ServiceMenu.UpdateDesc();
					this.StudentInfoMenu.PauseScreen.ServiceMenu.Purchase();
					this.StudentInfoMenu.FiringCouncilMember = false;
					this.StudentInfoMenu.PauseScreen.ServiceMenu.TextMessageManager.SpawnMessage(9);
				}
				else
				{
					this.StudentInfoMenu.PauseScreen.ServiceMenu.TextMessageManager.SpawnMessage(0);
				}
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.UpdateButtons();
			}
			else if (this.StudentInfoMenu.GettingOpinions)
			{
				for (int i = 1; i < 26; i++)
				{
					ConversationGlobals.SetTopicDiscovered(i, true);
					ConversationGlobals.SetTopicLearnedByStudent(i, this.CurrentStudent, true);
				}
				this.StudentInfoMenu.PauseScreen.ServiceMenu.Purchase();
				base.gameObject.SetActive(false);
				this.StudentInfoMenu.GettingOpinions = false;
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.UpdateButtons();
			}
		}
		if (Input.GetButtonDown("B"))
		{
			this.ShowRep = false;
			this.Topics.SetActive(false);
			base.GetComponent<AudioSource>().Stop();
			this.ReputationChart.transform.localScale = new Vector3(0f, 0f, 0f);
			if (this.Shutter != null)
			{
				if (!this.Shutter.PhotoIcons.activeInHierarchy)
				{
					this.Back = true;
				}
			}
			else
			{
				this.Back = true;
			}
			if (this.Back)
			{
				this.StudentInfoMenu.gameObject.SetActive(true);
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "View Info";
				if (!this.StudentInfoMenu.Gossiping)
				{
					this.PromptBar.Label[1].text = "Back";
				}
				this.PromptBar.UpdateButtons();
				this.Back = false;
			}
		}
		if (Input.GetButtonDown("X") && this.PromptBar.Button[2].enabled)
		{
			if (this.StudentManager.Tag.Target != this.StudentManager.Students[this.CurrentStudent].Head)
			{
				this.StudentManager.Tag.Target = this.StudentManager.Students[this.CurrentStudent].Head;
				this.StudentManager.TagStudentID = this.CurrentStudent;
				this.PromptBar.Label[2].text = "Untag";
			}
			else
			{
				this.StudentManager.TagStudentID = 0;
				this.StudentManager.Tag.Target = null;
				this.PromptBar.Label[2].text = "Tag";
			}
		}
		if (Input.GetButtonDown("Y") && this.PromptBar.Button[3].enabled)
		{
			if (!this.Topics.activeInHierarchy)
			{
				this.PromptBar.Label[3].text = "Basic Info";
				this.PromptBar.UpdateButtons();
				this.Topics.SetActive(true);
				this.UpdateTopics();
			}
			else
			{
				this.PromptBar.Label[3].text = "Interests";
				this.PromptBar.UpdateButtons();
				this.Topics.SetActive(false);
			}
		}
		if (Input.GetButtonDown("LB"))
		{
			this.UpdateRepChart();
			this.ShowRep = !this.ShowRep;
		}
		if (this.Yandere != null && !this.Yandere.NoDebug)
		{
			if (Input.GetKeyDown(KeyCode.Equals))
			{
				this.StudentManager.StudentReps[this.CurrentStudent] += 10f;
				this.UpdateInfo(this.CurrentStudent);
			}
			if (Input.GetKeyDown(KeyCode.Minus))
			{
				this.StudentManager.StudentReps[this.CurrentStudent] -= 10f;
				this.UpdateInfo(this.CurrentStudent);
			}
		}
		StudentInfoMenuScript studentInfoMenu = this.StudentInfoMenu;
		if (!studentInfoMenu.CyberBullying && !studentInfoMenu.CyberStalking && !studentInfoMenu.FindingLocker && !studentInfoMenu.UsingLifeNote && !studentInfoMenu.GettingInfo && !studentInfoMenu.MatchMaking && !studentInfoMenu.Distracting && !studentInfoMenu.SendingHome && !studentInfoMenu.Gossiping && !studentInfoMenu.Targeting && !studentInfoMenu.GettingOpinions && !studentInfoMenu.Dead)
		{
			if (this.StudentInfoMenu.PauseScreen.InputManager.TappedRight)
			{
				this.CurrentStudent++;
				if (this.CurrentStudent > 100)
				{
					this.CurrentStudent = 1;
				}
				while (!StudentGlobals.GetStudentPhotographed(this.CurrentStudent))
				{
					this.CurrentStudent++;
					if (this.CurrentStudent > 100)
					{
						this.CurrentStudent = 1;
					}
				}
				this.UpdateInfo(this.CurrentStudent);
				this.UpdateTopics();
			}
			if (this.StudentInfoMenu.PauseScreen.InputManager.TappedLeft)
			{
				this.CurrentStudent--;
				if (this.CurrentStudent < 1)
				{
					this.CurrentStudent = 100;
				}
				while (!StudentGlobals.GetStudentPhotographed(this.CurrentStudent))
				{
					this.CurrentStudent--;
					if (this.CurrentStudent < 1)
					{
						this.CurrentStudent = 100;
					}
				}
				this.UpdateInfo(this.CurrentStudent);
				this.UpdateTopics();
			}
		}
		if (this.ShowRep)
		{
			this.ReputationChart.transform.localScale = Vector3.Lerp(this.ReputationChart.transform.localScale, new Vector3(138f, 138f, 138f), Time.unscaledDeltaTime * 10f);
			return;
		}
		this.ReputationChart.transform.localScale = Vector3.Lerp(this.ReputationChart.transform.localScale, new Vector3(0f, 0f, 0f), Time.unscaledDeltaTime * 10f);
	}

	// Token: 0x06001D53 RID: 7507 RVA: 0x001627B8 File Offset: 0x001609B8
	private void UpdateAdditionalInfo(int ID)
	{
		if (!this.Eighties)
		{
			if (ID == 11)
			{
				if (this.Yandere != null)
				{
					this.Strings[1] = (this.Yandere.Police.EndOfDay.LearnedOsanaInfo1 ? "May be a victim of blackmail." : "?????");
					this.Strings[2] = (this.Yandere.Police.EndOfDay.LearnedOsanaInfo2 ? "Has a stalker." : "?????");
				}
				else
				{
					this.Strings[1] = "?????";
					this.Strings[2] = "?????";
				}
				this.InfoLabel.text = this.Strings[1] + "\n\n" + this.Strings[2];
				return;
			}
			if (ID == 51)
			{
				if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
				{
					this.InfoLabel.text = "Disbanded the Light Music Club, dyed her hair back to its original color, removed her piercings, and stopped socializing with others.";
					return;
				}
				this.InfoLabel.text = this.JSON.Students[ID].Info;
				return;
			}
			else
			{
				if (StudentGlobals.GetStudentReplaced(ID))
				{
					this.InfoLabel.text = "No additional information is available at this time.";
					return;
				}
				if (this.JSON.Students[ID].Info == string.Empty)
				{
					this.InfoLabel.text = "No additional information is available at this time.";
					return;
				}
				this.InfoLabel.text = this.JSON.Students[ID].Info;
				return;
			}
		}
		else
		{
			if (StudentGlobals.GetStudentReplaced(ID))
			{
				this.InfoLabel.text = "No additional information is available at this time.";
				return;
			}
			if (this.JSON.Students[ID].Info == string.Empty)
			{
				this.InfoLabel.text = "No additional information is available at this time.";
				return;
			}
			this.InfoLabel.text = this.JSON.Students[ID].Info;
			return;
		}
	}

	// Token: 0x06001D54 RID: 7508 RVA: 0x00162988 File Offset: 0x00160B88
	private void UpdateTopics()
	{
		int num = 0;
		int num2 = 0;
		for (int i = 1; i < this.TopicIcons.Length; i++)
		{
			this.TopicIcons[i].spriteName = ((!ConversationGlobals.GetTopicDiscovered(i)) ? 0 : i).ToString();
		}
		for (int j = 1; j <= 25; j++)
		{
			UISprite uisprite = this.TopicOpinionIcons[j];
			if (!ConversationGlobals.GetTopicLearnedByStudent(j, this.CurrentStudent))
			{
				uisprite.spriteName = "Unknown";
			}
			else
			{
				int[] topics = this.JSON.Topics[this.CurrentStudent].Topics;
				uisprite.spriteName = this.OpinionSpriteNames[topics[j]];
				if (topics[j] == 1)
				{
					num2++;
				}
				if (topics[j] == 2)
				{
					num++;
				}
			}
		}
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x00162A4C File Offset: 0x00160C4C
	private void UpdateRepChart()
	{
		Vector3 vector = Vector3.zero;
		if (this.CurrentStudent < 100)
		{
			vector = StudentGlobals.GetReputationTriangle(this.CurrentStudent);
		}
		else if (!this.Eighties)
		{
			vector = new Vector3((float)UnityEngine.Random.Range(-100, 101), (float)UnityEngine.Random.Range(-100, 101), (float)UnityEngine.Random.Range(-100, 101));
		}
		else
		{
			vector = new Vector3(0f, 50f, 100f);
		}
		this.ReputationChart.fields[0].Value = vector.x;
		this.ReputationChart.fields[1].Value = vector.y;
		this.ReputationChart.fields[2].Value = vector.z;
	}

	// Token: 0x040035F6 RID: 13814
	public StudentInfoMenuScript StudentInfoMenu;

	// Token: 0x040035F7 RID: 13815
	public StudentManagerScript StudentManager;

	// Token: 0x040035F8 RID: 13816
	public DialogueWheelScript DialogueWheel;

	// Token: 0x040035F9 RID: 13817
	public HomeInternetScript HomeInternet;

	// Token: 0x040035FA RID: 13818
	public TopicManagerScript TopicManager;

	// Token: 0x040035FB RID: 13819
	public NoteLockerScript NoteLocker;

	// Token: 0x040035FC RID: 13820
	public RadarChart ReputationChart;

	// Token: 0x040035FD RID: 13821
	public PromptBarScript PromptBar;

	// Token: 0x040035FE RID: 13822
	public ShutterScript Shutter;

	// Token: 0x040035FF RID: 13823
	public YandereScript Yandere;

	// Token: 0x04003600 RID: 13824
	public JsonScript JSON;

	// Token: 0x04003601 RID: 13825
	public Texture GuidanceCounselor;

	// Token: 0x04003602 RID: 13826
	public Texture DefaultPortrait;

	// Token: 0x04003603 RID: 13827
	public Texture BlankPortrait;

	// Token: 0x04003604 RID: 13828
	public Texture Headmaster;

	// Token: 0x04003605 RID: 13829
	public Texture InfoChan;

	// Token: 0x04003606 RID: 13830
	public Transform ReputationBar;

	// Token: 0x04003607 RID: 13831
	public GameObject Static;

	// Token: 0x04003608 RID: 13832
	public GameObject Topics;

	// Token: 0x04003609 RID: 13833
	public UILabel OccupationLabel;

	// Token: 0x0400360A RID: 13834
	public UILabel ReputationLabel;

	// Token: 0x0400360B RID: 13835
	public UILabel RealNameLabel;

	// Token: 0x0400360C RID: 13836
	public UILabel StrengthLabel;

	// Token: 0x0400360D RID: 13837
	public UILabel PersonaLabel;

	// Token: 0x0400360E RID: 13838
	public UILabel ClassLabel;

	// Token: 0x0400360F RID: 13839
	public UILabel CrushLabel;

	// Token: 0x04003610 RID: 13840
	public UILabel ClubLabel;

	// Token: 0x04003611 RID: 13841
	public UILabel InfoLabel;

	// Token: 0x04003612 RID: 13842
	public UILabel NameLabel;

	// Token: 0x04003613 RID: 13843
	public UITexture Portrait;

	// Token: 0x04003614 RID: 13844
	public string[] OpinionSpriteNames;

	// Token: 0x04003615 RID: 13845
	public string[] Strings;

	// Token: 0x04003616 RID: 13846
	public int CurrentStudent;

	// Token: 0x04003617 RID: 13847
	public bool UpdatedOnce;

	// Token: 0x04003618 RID: 13848
	public bool Eighties;

	// Token: 0x04003619 RID: 13849
	public bool ShowRep;

	// Token: 0x0400361A RID: 13850
	public bool Back;

	// Token: 0x0400361B RID: 13851
	public UISprite[] TopicIcons;

	// Token: 0x0400361C RID: 13852
	public UISprite[] TopicOpinionIcons;

	// Token: 0x0400361D RID: 13853
	private static readonly IntAndStringDictionary StrengthStrings = new IntAndStringDictionary
	{
		{
			0,
			"Incapable"
		},
		{
			1,
			"Very Weak"
		},
		{
			2,
			"Weak"
		},
		{
			3,
			"Strong"
		},
		{
			4,
			"Very Strong"
		},
		{
			5,
			"Peak Physical Strength"
		},
		{
			6,
			"Extensive Training"
		},
		{
			7,
			"Carries Pepper Spray"
		},
		{
			8,
			"Armed"
		},
		{
			9,
			"Invincible"
		},
		{
			99,
			"?????"
		}
	};
}
