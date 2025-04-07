// Assembly-CSharp, Version=1.5.9214.33606, Culture=neutral, PublicKeyToken=null
// RimWorld.StorytellerCompProperties_MechanitorComplexQuest
using System.Collections.Generic;
using RimWorld;

namespace eridanus_quests
{
	public class StorytellerCompProperties_FactionQuest : StorytellerCompProperties
	{
		public IncidentDef incident;

		public int mtbDays = 60;

		public float minSpacingDays;

		public List<FactionDef> allowedFactions;	// player must be a subset of these

		public List<QuestScriptDef> blockedByQueuedOrActiveQuests;

		public StorytellerCompProperties_FactionQuest()
		{
			compClass = typeof(StorytellerCompProperties_FactionQuest);
		}
	}
}