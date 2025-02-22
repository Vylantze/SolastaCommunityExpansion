﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HarmonyLib;
using SolastaCommunityExpansion.CustomInterfaces;

namespace SolastaCommunityExpansion.Patches.CustomFeatures.CustomAttacks;

//
// IConditionRemovedOnSourceTurnStart
//
[HarmonyPatch(typeof(RulesetActor), "ProcessConditionsMatchingOccurenceType")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class RulesetActor_ProcessConditionsMatchingOccurenceType
{
    internal static void Postfix(RulesetActor __instance, RuleDefinitions.TurnOccurenceType occurenceType)
    {
        if (occurenceType != RuleDefinitions.TurnOccurenceType.StartOfTurn)
        {
            return;
        }

        var battleService = ServiceRepository.GetService<IGameLocationBattleService>();

        if (battleService?.Battle == null)
        {
            return;
        }

        foreach (var contender in battleService.Battle.AllContenders
                     .Where(x => x != null
                                 && !x.destroying && !x.destroyedBody && x.RulesetActor != null))
        {
            var conditionsToRemove = new List<RulesetCondition>();

            foreach (var keyValuePair in contender.RulesetActor.ConditionsByCategory)
            {
                foreach (var rulesetCondition in keyValuePair.Value)
                {
                    if (rulesetCondition.SourceGuid == __instance.Guid &&
                        rulesetCondition.ConditionDefinition is IConditionRemovedOnSourceTurnStart)
                    {
                        conditionsToRemove.Add(rulesetCondition);
                    }
                }
            }

            foreach (var conditionToRemove in conditionsToRemove)
            {
                contender.RulesetActor.RemoveCondition(conditionToRemove);
            }
        }
    }
}
