//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: SettingsStore
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DH.IME.Spot.Core {
	internal static class SettingsStore {
		private const string SETTINGS_KEY_PATH = @"SOFTWARE\DHTOOL\DH.IME.Spot";
		private const string RUN_KEY_PATH = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
		private const string RUN_VALUE_NAME = "DH.IME.Spot";

		public static AppSettings Load() {
			AppSettings settings = AppSettings.Defaults();
			bool bfirstrun = false;

			try {
				using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(SETTINGS_KEY_PATH, false)) {
					if (regkey == null) {
						bfirstrun = true;
					}

					if (regkey != null) {
						settings.BackgroundAlpha = ReadInt(regkey, "BackgroundAlpha", settings.BackgroundAlpha);

						if (regkey.GetValue("ActiveWindowEnabled") == null && regkey.GetValue("Mode") != null) {
							MigrateLegacyMode(regkey, settings);
						}
						else {
							settings.ActiveWindowEnabled = ReadInt(regkey, "ActiveWindowEnabled", settings.ActiveWindowEnabled == true ? 1 : 0) != 0;
							settings.CursorEnabled = ReadInt(regkey, "CursorEnabled", settings.CursorEnabled == true ? 1 : 0) != 0;
							settings.MonitorWidgetEnabled = ReadInt(regkey, "MonitorWidgetEnabled", settings.MonitorWidgetEnabled == true ? 1 : 0) != 0;
						}

						settings.ActiveWindowCorner = ReadEnum(regkey, "ActiveWindowCorner", settings.ActiveWindowCorner);
						settings.MonitorWidgetCorner = ReadEnum(regkey, "MonitorWidgetCorner", settings.MonitorWidgetCorner);
						settings.MonitorWidgetScope = ReadEnum(regkey, "MonitorWidgetScope", settings.MonitorWidgetScope);
						settings.CursorBadgeSize = ReadEnum(regkey, "CursorBadgeSize", settings.CursorBadgeSize);
					}
				}
			}
			catch {
			}

			if (bfirstrun == true) {
				Save(settings);
			}

			settings.RunAtStartup = IsRunAtStartup();
			settings.Normalize();
			return settings;
		}

		public static void Save(AppSettings settings) {
			if (settings == null) {
				return;
			}

			settings.Normalize();

			try {
				using (RegistryKey regkey = Registry.CurrentUser.CreateSubKey(SETTINGS_KEY_PATH)) {
					if (regkey != null) {
						regkey.SetValue("BackgroundAlpha", settings.BackgroundAlpha, RegistryValueKind.DWord);
						regkey.SetValue("ActiveWindowEnabled", settings.ActiveWindowEnabled == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("ActiveWindowCorner", settings.ActiveWindowCorner.ToString(), RegistryValueKind.String);
						regkey.SetValue("CursorEnabled", settings.CursorEnabled == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("CursorBadgeSize", settings.CursorBadgeSize.ToString(), RegistryValueKind.String);
						regkey.SetValue("MonitorWidgetEnabled", settings.MonitorWidgetEnabled == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("MonitorWidgetCorner", settings.MonitorWidgetCorner.ToString(), RegistryValueKind.String);
						regkey.SetValue("MonitorWidgetScope", settings.MonitorWidgetScope.ToString(), RegistryValueKind.String);
					}
				}
			}
			catch {
			}

			ApplyRunAtStartup(settings.RunAtStartup);
		}

		private static void MigrateLegacyMode(RegistryKey regkey, AppSettings settings) {
			enumDisplayMode emode = ReadEnum(regkey, "Mode", enumDisplayMode.ActiveWindowCorner);
			enumBadgeCorner ecorner = ReadEnum(regkey, "Corner", enumBadgeCorner.TopRight);

			settings.ActiveWindowEnabled = emode == enumDisplayMode.ActiveWindowCorner;
			settings.CursorEnabled = emode == enumDisplayMode.CursorCompanion;
			settings.MonitorWidgetEnabled = emode == enumDisplayMode.PerMonitorWidget;

			if (emode == enumDisplayMode.PerMonitorWidget) {
				settings.MonitorWidgetCorner = ecorner;
			}
			else {
				settings.ActiveWindowCorner = ecorner;
			}
		}

		private static int ReadInt(RegistryKey regkey, string strname, int nfallback) {
			object objvalue = regkey.GetValue(strname);
			if (objvalue == null) {
				return nfallback;
			}

			try {
				return Convert.ToInt32(objvalue);
			}
			catch {
				return nfallback;
			}
		}

		private static T ReadEnum<T>(RegistryKey regkey, string strname, T fallback) where T : struct {
			string strtext = regkey.GetValue(strname) as string;
			if (string.IsNullOrEmpty(strtext) == true) {
				return fallback;
			}

			T parsed;
			if (Enum.TryParse(strtext, true, out parsed) == true && Enum.IsDefined(typeof(T), parsed) == true) {
				return parsed;
			}

			return fallback;
		}

		private static bool IsRunAtStartup() {
			try {
				using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(RUN_KEY_PATH, false)) {
					return regkey != null && regkey.GetValue(RUN_VALUE_NAME) != null;
				}
			}
			catch {
				return false;
			}
		}

		private static void ApplyRunAtStartup(bool benable) {
			try {
				using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(RUN_KEY_PATH, true)) {
					if (regkey == null) {
						return;
					}

					if (benable == true) {
						regkey.SetValue(RUN_VALUE_NAME, "\"" + Application.ExecutablePath + "\"", RegistryValueKind.String);
					}
					else if (regkey.GetValue(RUN_VALUE_NAME) != null) {
						regkey.DeleteValue(RUN_VALUE_NAME, false);
					}
				}
			}
			catch {
			}
		}
	}
}
