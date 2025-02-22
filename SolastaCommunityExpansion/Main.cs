﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using HarmonyLib;
using ModKit;
using SolastaCommunityExpansion.Models;
using SolastaCommunityExpansion.Utils;
using UnityModManagerNet;
using Debug = UnityEngine.Debug;

namespace SolastaCommunityExpansion;

internal static class Main
{
    internal static bool IsDebugBuild = Debug.isDebugBuild;

    internal static string MOD_FOLDER { get; } =
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    internal static bool Enabled { get; set; }

    internal static UnityModManager.ModEntry.ModLogger Logger { get; private set; }

    private static ModManager<Core, Settings> Mod { get; set; }
    private static MenuManager Menu { get; set; }

    internal static Settings Settings => Mod.Settings;

    [Conditional("DEBUG")]
    internal static void Log(string msg, bool console = false)
    {
        Logger.Log(msg);

        if (console)
        {
            var game = Gui.Game;
            if (game != null)
            {
                game.GameConsole?.LogSimpleLine(msg);
            }
        }
    }

    // need to be public for MC sidecar
    internal static void Error(Exception ex)
    {
        Logger?.Error(ex.ToString());
    }

    // need to be public for MC sidecar
    internal static void Error(string msg)
    {
        Logger?.Error(msg);
    }

    internal static bool Load(UnityModManager.ModEntry modEntry)
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();

            Logger = modEntry.Logger;
            Mod = new ModManager<Core, Settings>();
            Mod.Enable(modEntry, assembly);
            modEntry.OnShowGUI = OnShowGui;

            Menu = new MenuManager();
            Menu.Enable(modEntry, assembly);

            if (Settings.SelectedOverwriteLanguageCode != "off")
            {
                Translations.LoadTranslations("Game");
            }

            // Users still have SolastaMulticlass.dll
            var multiclassFilename = Path.Combine(MOD_FOLDER, "SolastaMulticlass.dll");

            if (File.Exists(multiclassFilename))
            {
                File.Delete(multiclassFilename);
            }

            // side cars allow us to load any other DLL outside CE
            LoadSidecars(assembly.GetName().Name);
        }
        catch (Exception ex)
        {
            Error(ex);
            throw;
        }

        return true;
    }

    private static void OnShowGui(UnityModManager.ModEntry modEntry)
    {
        if (Settings.EnableHeroesControlledByComputer)
        {
            PlayerControllerContext.RefreshGuiState();
        }
    }

    internal static void LoadSidecars(string currentAssemblyName)
    {
        foreach (var path in Directory.EnumerateFiles(MOD_FOLDER, "Solasta*.dll"))
        {
            var filename = Path.GetFileName(path);

            if (filename.StartsWith(currentAssemblyName))
            {
                continue;
            }

            var sidecarAssembly = Assembly.LoadFile(path);
            var harmony = new Harmony(sidecarAssembly.GetName().Name);

            harmony.PatchAll(sidecarAssembly);
        }
    }
}
