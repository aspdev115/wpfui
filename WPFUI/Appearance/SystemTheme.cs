﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Microsoft.Win32;
using System.Windows;
using System.Windows.Media;

namespace WPFUI.Appearance
{
    internal static class SystemTheme
    {
        /// <summary>
        /// Gets the current main color of the system.
        /// </summary>
        /// <returns></returns>
        public static Color GlassColor => SystemParameters.WindowGlassColor;

        /// <summary>
        /// Determines whether the system is currently set to hight contrast mode.
        /// </summary>
        /// <returns><see langword="true"/> if <see cref="SystemParameters.HighContrast"/>.</returns>
        public static bool HighContrast => SystemParameters.HighContrast;

        /// <summary>
        /// Gets currently set system theme based on <see cref="Registry"/> value.
        /// </summary>
        public static SystemThemeType GetTheme()
        {
            string currentTheme =
                Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes",
                    "CurrentTheme", "aero.theme") as string;

            if (string.IsNullOrEmpty(currentTheme))
                return SystemThemeType.Unknown;

            currentTheme = currentTheme.ToLower().Trim();

            // This may be changed in the next versions, check the Insider previews

            if (currentTheme.Contains("aero.theme"))
                return SystemThemeType.Light;

            if (currentTheme.Contains("dark.theme"))
                return SystemThemeType.Dark;

            if (currentTheme.Contains("themea.theme"))
                return SystemThemeType.Glow;

            if (currentTheme.Contains("themeb.theme"))
                return SystemThemeType.CapturedMotion;

            if (currentTheme.Contains("themec.theme"))
                return SystemThemeType.Sunrise;

            if (currentTheme.Contains("themed.theme"))
                return SystemThemeType.Flow;

            //if (currentTheme.Contains("custom.theme"))
            //    return ; custom can be light or dark

            int appsUseLightTheme = (int)Registry.GetValue(
            "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize",
            "AppsUseLightTheme", 1)!;

            if (appsUseLightTheme == 0)
                return SystemThemeType.Dark;

            int systemUsesLightTheme = (int)Registry.GetValue(
                "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize",
                "SystemUsesLightTheme", 1)!;

            if (systemUsesLightTheme == 0)
                return SystemThemeType.Dark;

            return SystemThemeType.Light;
        }
    }
}
