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
						settings.PollIntervalMs = ReadInt(regkey, "PollIntervalMs", settings.PollIntervalMs);

						if (regkey.GetValue("ActiveWindowEnabled") == null && regkey.GetValue("Mode") != null) {
							MigrateLegacyMode(regkey, settings);
						}
						else {
							settings.ActiveWindowEnabled = ReadBool(regkey, "ActiveWindowEnabled", settings.ActiveWindowEnabled);
							settings.CursorEnabled = ReadBool(regkey, "CursorEnabled", settings.CursorEnabled);
							settings.MonitorWidgetEnabled = ReadBool(regkey, "MonitorWidgetEnabled", settings.MonitorWidgetEnabled);
						}

						settings.ActiveWindowCorner = ReadEnum(regkey, "ActiveWindowCorner", settings.ActiveWindowCorner);
						settings.MonitorWidgetCorner = ReadEnum(regkey, "MonitorWidgetCorner", settings.MonitorWidgetCorner);
						settings.MonitorWidgetScope = ReadEnum(regkey, "MonitorWidgetScope", settings.MonitorWidgetScope);
						settings.CursorBadgeSize = ReadEnum(regkey, "CursorBadgeSize", settings.CursorBadgeSize);

						settings.ShowCapsLock = ReadBool(regkey, "ShowCapsLock", settings.ShowCapsLock);
						settings.ShowNumLock = ReadBool(regkey, "ShowNumLock", settings.ShowNumLock);
						settings.ShowScrollLock = ReadBool(regkey, "ShowScrollLock", settings.ShowScrollLock);
						settings.CapsLockCorner = ReadEnum(regkey, "CapsLockCorner", settings.CapsLockCorner);
						settings.NumLockCorner = ReadEnum(regkey, "NumLockCorner", settings.NumLockCorner);
						settings.ScrollLockCorner = ReadEnum(regkey, "ScrollLockCorner", settings.ScrollLockCorner);
						settings.CapsLockDotSize = ReadInt(regkey, "CapsLockDotSize", settings.CapsLockDotSize);
						settings.NumLockDotSize = ReadInt(regkey, "NumLockDotSize", settings.NumLockDotSize);
						settings.ScrollLockDotSize = ReadInt(regkey, "ScrollLockDotSize", settings.ScrollLockDotSize);
						settings.CapsLockDotColor = ReadEnum(regkey, "CapsLockDotColor", settings.CapsLockDotColor);
						settings.NumLockDotColor = ReadEnum(regkey, "NumLockDotColor", settings.NumLockDotColor);
						settings.ScrollLockDotColor = ReadEnum(regkey, "ScrollLockDotColor", settings.ScrollLockDotColor);

						settings.BadgeShadow = ReadBool(regkey, "BadgeShadow", settings.BadgeShadow);
						settings.BadgeLockPill = ReadBool(regkey, "BadgeLockPill", settings.BadgeLockPill);

						settings.FadeIdleEnabled = ReadBool(regkey, "FadeIdleEnabled", settings.FadeIdleEnabled);
						settings.FadeIdleDelayMs = ReadInt(regkey, "FadeIdleDelayMs", settings.FadeIdleDelayMs);
						settings.FadeIdleAction = ReadEnum(regkey, "FadeIdleAction", settings.FadeIdleAction);
						settings.FadeIdleDimPercent = ReadInt(regkey, "FadeIdleDimPercent", settings.FadeIdleDimPercent);

						settings.FlashEnabled = ReadBool(regkey, "FlashEnabled", settings.FlashEnabled);
						settings.FlashOnImeSwitch = ReadBool(regkey, "FlashOnImeSwitch", settings.FlashOnImeSwitch);
						settings.FlashOnCapsLock = ReadBool(regkey, "FlashOnCapsLock", settings.FlashOnCapsLock);
						settings.FlashOnNumLock = ReadBool(regkey, "FlashOnNumLock", settings.FlashOnNumLock);
						settings.FlashOnScrollLock = ReadBool(regkey, "FlashOnScrollLock", settings.FlashOnScrollLock);
						settings.FlashDurationMs = ReadInt(regkey, "FlashDurationMs", settings.FlashDurationMs);
						settings.FlashAnchor = ReadEnum(regkey, "FlashAnchor", settings.FlashAnchor);
						settings.FlashSize = ReadEnum(regkey, "FlashSize", settings.FlashSize);
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
						regkey.SetValue("PollIntervalMs", settings.PollIntervalMs, RegistryValueKind.DWord);
						regkey.SetValue("ActiveWindowEnabled", settings.ActiveWindowEnabled == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("ActiveWindowCorner", settings.ActiveWindowCorner.ToString(), RegistryValueKind.String);
						regkey.SetValue("CursorEnabled", settings.CursorEnabled == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("CursorBadgeSize", settings.CursorBadgeSize.ToString(), RegistryValueKind.String);
						regkey.SetValue("MonitorWidgetEnabled", settings.MonitorWidgetEnabled == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("MonitorWidgetCorner", settings.MonitorWidgetCorner.ToString(), RegistryValueKind.String);
						regkey.SetValue("MonitorWidgetScope", settings.MonitorWidgetScope.ToString(), RegistryValueKind.String);

						regkey.SetValue("ShowCapsLock", settings.ShowCapsLock == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("ShowNumLock", settings.ShowNumLock == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("ShowScrollLock", settings.ShowScrollLock == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("CapsLockCorner", settings.CapsLockCorner.ToString(), RegistryValueKind.String);
						regkey.SetValue("NumLockCorner", settings.NumLockCorner.ToString(), RegistryValueKind.String);
						regkey.SetValue("ScrollLockCorner", settings.ScrollLockCorner.ToString(), RegistryValueKind.String);
						regkey.SetValue("CapsLockDotSize", settings.CapsLockDotSize, RegistryValueKind.DWord);
						regkey.SetValue("NumLockDotSize", settings.NumLockDotSize, RegistryValueKind.DWord);
						regkey.SetValue("ScrollLockDotSize", settings.ScrollLockDotSize, RegistryValueKind.DWord);
						regkey.SetValue("CapsLockDotColor", settings.CapsLockDotColor.ToString(), RegistryValueKind.String);
						regkey.SetValue("NumLockDotColor", settings.NumLockDotColor.ToString(), RegistryValueKind.String);
						regkey.SetValue("ScrollLockDotColor", settings.ScrollLockDotColor.ToString(), RegistryValueKind.String);

						regkey.SetValue("BadgeShadow", settings.BadgeShadow == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("BadgeLockPill", settings.BadgeLockPill == true ? 1 : 0, RegistryValueKind.DWord);

						regkey.SetValue("FadeIdleEnabled", settings.FadeIdleEnabled == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("FadeIdleDelayMs", settings.FadeIdleDelayMs, RegistryValueKind.DWord);
						regkey.SetValue("FadeIdleAction", settings.FadeIdleAction.ToString(), RegistryValueKind.String);
						regkey.SetValue("FadeIdleDimPercent", settings.FadeIdleDimPercent, RegistryValueKind.DWord);

						regkey.SetValue("FlashEnabled", settings.FlashEnabled == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("FlashOnImeSwitch", settings.FlashOnImeSwitch == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("FlashOnCapsLock", settings.FlashOnCapsLock == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("FlashOnNumLock", settings.FlashOnNumLock == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("FlashOnScrollLock", settings.FlashOnScrollLock == true ? 1 : 0, RegistryValueKind.DWord);
						regkey.SetValue("FlashDurationMs", settings.FlashDurationMs, RegistryValueKind.DWord);
						regkey.SetValue("FlashAnchor", settings.FlashAnchor.ToString(), RegistryValueKind.String);
						regkey.SetValue("FlashSize", settings.FlashSize.ToString(), RegistryValueKind.String);
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

		private static bool ReadBool(RegistryKey regkey, string strname, bool bfallback) {
			return ReadInt(regkey, strname, bfallback == true ? 1 : 0) != 0;
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
