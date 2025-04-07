// Assembly-CSharp, Version=1.5.9214.33606, Culture=neutral, PublicKeyToken=null
// RimWorld.StorytellerComp_MechanitorComplexQuest
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace eridanus_quests
{
	public class StorytellerComp_FactionQuest : StorytellerComp
	{
		private StorytellerCompProperties_FactionQuest Props => (StorytellerCompProperties_FactionQuest)props;

		public override IEnumerable<FiringIncident> MakeIntervalIncidents(IIncidentTarget target)
		{
			List<Quest> questsListForReading = Find.QuestManager.QuestsListForReading;
			if (!Props.blockedByQueuedOrActiveQuests.NullOrEmpty())
			{
				for (int i = 0; i < questsListForReading.Count; i++)
				{
					if (Props.blockedByQueuedOrActiveQuests.Contains(questsListForReading[i].root) && (questsListForReading[i].State == QuestState.NotYetAccepted || questsListForReading[i].State == QuestState.Ongoing))
					{
						yield break;
					}
				}
			}
			int num = -1;
			for (int j = 0; j < questsListForReading.Count; j++)
			{
				if (questsListForReading[j].root == Props.incident.questScriptDef && questsListForReading[j].cleanupTick > num)
				{
					num = questsListForReading[j].cleanupTick;
				}
			}
			if (num > 0 && (Find.TickManager.TicksGame - num).TicksToDays() < Props.minSpacingDays)
			{
				yield break;
			}

			bool playerFactionFound = false;
			if (FactionDefOf.PlayerColony != null)
			{
				for (int i = 0; i < Props.allowedFactions.Count; i++)
				{
					if (Props.allowedFactions[i].Equals(FactionDefOf.PlayerColony)) // Need to write a function/class to store player faction they want to be
					{
						playerFactionFound = true;
						break;
					}
				}
			}
			if (!playerFactionFound)
			{
                yield break;
            }
            if (Rand.MTBEventOccurs(Props.mtbDays, 60000f, 1000f))
			{
				IncidentParms parms = GenerateParms(Props.incident.category, target);
				if (Props.incident.Worker.CanFireNow(parms))
				{
					yield return new FiringIncident(Props.incident, this, parms);
				}
			}
		}
	}
}