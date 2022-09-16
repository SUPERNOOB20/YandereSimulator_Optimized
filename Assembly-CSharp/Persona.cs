﻿// Decompiled with JetBrains decompiler
// Type: Persona
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEBC9029-E754-4F76-ACC2-E5BB554B97F0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class Persona
{
  [SerializeField]
  private PersonaType type;
  public static readonly PersonaTypeAndStringDictionary PersonaNames;

  public Persona(PersonaType type) => this.type = type;

  public PersonaType Type => this.type;

  static Persona()
  {
    PersonaTypeAndStringDictionary stringDictionary = new PersonaTypeAndStringDictionary();
    stringDictionary.Add(PersonaType.None, "None");
    stringDictionary.Add(PersonaType.Loner, "Loner");
    stringDictionary.Add(PersonaType.TeachersPet, "Teacher's Pet");
    stringDictionary.Add(PersonaType.Heroic, "Heroic");
    stringDictionary.Add(PersonaType.Coward, "Coward");
    stringDictionary.Add(PersonaType.Evil, "Evil");
    stringDictionary.Add(PersonaType.SocialButterfly, "Social Butterfly");
    stringDictionary.Add(PersonaType.Lovestruck, "Lovestruck");
    stringDictionary.Add(PersonaType.Dangerous, "Dangerous");
    stringDictionary.Add(PersonaType.Strict, "Strict");
    stringDictionary.Add(PersonaType.PhoneAddict, "Phone Addict");
    stringDictionary.Add(PersonaType.Fragile, "Fragile");
    stringDictionary.Add(PersonaType.Spiteful, "Spiteful");
    stringDictionary.Add(PersonaType.Sleuth, "Sleuth");
    stringDictionary.Add(PersonaType.Vengeful, "Vengeful");
    stringDictionary.Add(PersonaType.Protective, "Protective");
    stringDictionary.Add(PersonaType.Violent, "Violent");
    stringDictionary.Add(PersonaType.LandlineUser, "Snitch");
    stringDictionary.Add(PersonaType.Nemesis, "?????");
    Persona.PersonaNames = stringDictionary;
  }
}
