﻿using System.Diagnostics.CodeAnalysis;
using HarmonyLib;

namespace SolastaCommunityExpansion.Patches.GameUi.DialoguesOnConsole;

[HarmonyPatch(typeof(NarrativeDirectionManager), "StartDialogSequence")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class NarrativeDirectionManager_StartDialogSequence
{
    internal static void Postfix()
    {
        if (!Main.Settings.EnableLogDialoguesToConsole)
        {
            return;
        }

        var screen = Gui.GuiService.GetScreen<GuiConsoleScreen>();

        screen.Show();
    }
}
