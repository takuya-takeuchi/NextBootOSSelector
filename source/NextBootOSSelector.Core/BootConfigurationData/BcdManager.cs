using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{

    public sealed class BcdManager
    {


        #region イベント
        #endregion

        #region フィールド

        private static readonly string GUID_WINDOWS_BOOTMGR = "{9dea862c-5cdd-4e70-acc1-f32b344d4795}";

        private static readonly string GUID_DEBUGGER_SETTINGS_GROUP = "{4636856e-540f-4170-a130-a84776f4c654}";

        private static readonly string GUID_CURRENT_BOOT_ENTRY = "{fa926493-6f1c-4193-a414-58f0b2456d1e}";

        private static readonly string GUID_WINDOWS_LEGACY_NTLDR = "{466f5a88-0af2-4f76-9038-095b170dc21c}";

        #endregion

        #region コンストラクタ
        #endregion

        #region プロパティ
        #endregion

        #region メソッド

        public static IEnumerable<OperatingSystem> GetOperatingSystems()
        {
            OutputDebugLog("Start GetOperatingSystems");

            var managementScope = CreateManagementScope();

            // 現在の OS を取得
            var currentId = GetCurrentId();

            OutputDebugLog(string.Format("\tcurrentId - '{0}'", currentId));

            // 既定の OS を取得
            var defaultId = GetDefaultId();

            OutputDebugLog(string.Format("\tdefaultId - '{0}'", defaultId));

            // 次回の起動 OS を取得
            var resumeId = GetResumeId();

            OutputDebugLog(string.Format("\tresumeId - '{0}'", resumeId));

            OutputDebugLog("\tStart Enumerate List of Operating Systems");

            var order = 1;
            foreach (var id in GetIds())
            {
                var osObj = new BcdObject(managementScope, id, "");

                // USE A CLASS TO KEEP OS OBJECT AND NAME FOR LATER USE
                var operatingSystem = new OperatingSystem(osObj, id);

                // 順番
                operatingSystem.Order = order;
                order++;

                // 現在の OS かどうか
                operatingSystem.IsCurrent = id.Equals(currentId, StringComparison.InvariantCultureIgnoreCase);

                // 既定の OS かどうか
                operatingSystem.IsDefault = id.Equals(defaultId, StringComparison.InvariantCultureIgnoreCase);

                // 次回の起動 OS かどうか
                operatingSystem.IsNext = id.Equals(resumeId, StringComparison.InvariantCultureIgnoreCase);

                OutputDebugLog(string.Format("\t\tOrder - '{0}'", operatingSystem.Order));
                OutputDebugLog(string.Format("\t\tApplicationDevice - '{0}'", operatingSystem.ApplicationDevice));
                OutputDebugLog(string.Format("\t\tApplicationPath - '{0}'", operatingSystem.ApplicationPath));
                OutputDebugLog(string.Format("\t\tDescription - '{0}'", operatingSystem.Description));
                OutputDebugLog(string.Format("\t\tIdentifier - '{0}'", operatingSystem.Identifier));
                OutputDebugLog(string.Format("\t\tIsCurrent - '{0}'", operatingSystem.IsCurrent));
                OutputDebugLog(string.Format("\t\tIsDefault - '{0}'", operatingSystem.IsDefault));
                OutputDebugLog(string.Format("\t\tIsNext - '{0}'", operatingSystem.IsNext));
                OutputDebugLog(string.Format("\t\tLocale - '{0}'", operatingSystem.Locale));
                OutputDebugLog(string.Format("\t\tSystemRoot - '{0}'", operatingSystem.SystemRoot));

                yield return operatingSystem;
            }

            OutputDebugLog("\tEnd Enumerate List of Operating Systems");

            OutputDebugLog("End GetOperatingSystems");
        }

        public static void Reboot()
        {
            //ユーザー特権を有効にするための設定を作成
            var co = new ConnectionOptions
            {
                Impersonation = ImpersonationLevel.Impersonate,
                EnablePrivileges = true
            };

            var sc = new ManagementScope("\\ROOT\\CIMV2", co);
            sc.Connect();

            var oq = new ObjectQuery("select * from Win32_OperatingSystem");
            using (var mos = new ManagementObjectSearcher(sc, oq))
            {
                foreach (ManagementObject mo in mos.Get())
                {
                    // パラメータを指定
                    var inParams = mo.GetMethodParameters("Win32Shutdown");
                    inParams["Flags"] = 2; // 再起動
                    inParams["Reserved"] = 0;

                    // Win32Shutdownメソッドを呼び出す
                    var outParams = mo.InvokeMethod("Win32Shutdown", inParams, null);
                    mo.Dispose();
                }

            }
        }

        public static bool UpdateDefaultOperatingSystem(string operatingSystemId)
        {
            var bcdObject = GetBcdObject();

            ManagementBaseObject mboOut;
            var success = bcdObject.GetElement((uint)BcdBootMgrElementTypes.BcdBootMgrObject_DefaultObject, out mboOut);
            if (!success)
            {
                throw new Exception();
            }

            var defaultId = GetDefaultId();
            if (defaultId == operatingSystemId)
            {
                return false;
            }

            var result = bcdObject.SetObjectElement(operatingSystemId, (uint)BcdBootMgrElementTypes.BcdBootMgrObject_DefaultObject);
            if (!result)
            {
                var message = string.Format("Failed to execute UpdateDefaultOperatingSystem, Argument is '{0}'", operatingSystemId);
                OutputErrorLog(message);
                throw new Exception(message);
            }

            return true;
        }

        public static bool UpdateDisplayOrder(IEnumerable<string> operatingSystemsIds)
        {
            var bcdObject = GetBcdObject();

            ManagementBaseObject mboOut;
            var success = bcdObject.GetElement((uint)BcdBootMgrElementTypes.BcdBootMgrObjectList_DisplayOrder, out mboOut);
            if (!success)
            {
                throw new Exception();
            }

            var update = false;

            var ids = operatingSystemsIds.ToArray();
            var originalIds = GetIds().ToArray();
            if (originalIds.Length == ids.Length)
            {
                for (var index = 0; index < originalIds.Length; index++)
                {
                    if (originalIds[index] != ids[index])
                    {
                        update = true;
                        break;
                    }
                }
            }

            if (update)
            {
                var result = bcdObject.SetObjectListElement(ids, (uint)BcdBootMgrElementTypes.BcdBootMgrObjectList_DisplayOrder);
                if (!result)
                {
                    var message = string.Format("Failed to execute UpdateDisplayOrder, Argument is '{0}'", string.Join(", ", ids));
                    OutputErrorLog(message);
                    throw new Exception(message);
                }

                return true;
            }
            return false;
        }

        public static bool UpdateNextBootOperatingSystem()
        {
            // 元に戻す方法がわからない
            return UpdateNextBootOperatingSystem(GUID_WINDOWS_BOOTMGR);
        }

        public static bool UpdateNextBootOperatingSystem(string operatingSystemId)
        {
            var bcdObject = GetBcdObject();

            var ids = new[]
            {
                operatingSystemId
            };
            var success = bcdObject.SetObjectListElement(ids, (uint)BcdBootMgrElementTypes.BcdBootMgrObjectList_BootSequence);
            if (!success)
            {
                var message = string.Format("Failed to execute UpdateNextBootOperatingSystem, Argument is '{0}'", operatingSystemId);
                OutputErrorLog(message);
                throw new Exception(message);
            }

            return true;
        }

        #region オーバーライド
        #endregion

        #region イベントハンドラ
        #endregion

        #region ヘルパーメソッド

        private static ManagementScope CreateManagementScope()
        {
            var connectionOptions = new ConnectionOptions
            {
                Impersonation = ImpersonationLevel.Impersonate,
                EnablePrivileges = true
            };

            return new ManagementScope(@"root\WMI", connectionOptions);
        }

        [Conditional("DEBUG")]
        private static void OutputDebugLog(string message)
        {
            Debug.WriteLine("[{0}][DEBUG] {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.hhhh"), message);
        }

        [Conditional("DEBUG")]
        private static void OutputDebugLog(string format, params object[] args)
        {
            OutputDebugLog(string.Format(format, args));
        }

        [Conditional("DEBUG")]
        private static void OutputErrorLog(string message)
        {
            Debug.WriteLine("[{0}][ERROR] {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.hhhh"), message);
        }

        [Conditional("DEBUG")]
        private static void OutputErrorLog(string format, params object[] args)
        {
            OutputErrorLog(string.Format(format, args));
        }

        private static BcdObject GetBcdObject()
        {
            var managementScope = CreateManagementScope();

            return new BcdObject(managementScope, GUID_WINDOWS_BOOTMGR, "");
        }

        private static BcdObject GetCurrentBcdObject()
        {
            var managementScope = CreateManagementScope();
            return new BcdObject(managementScope, GUID_CURRENT_BOOT_ENTRY, "");
        }

        private static string GetCurrentId()
        {
            var bcdObject = GetCurrentBcdObject();

            ManagementBaseObject mboOut;
            var success = bcdObject.GetElement((uint)BcdBootMgrElementTypes.BcdBootMgrObject_DefaultObject, out mboOut);
            if (!success)
            {
                var message = "Failed to execute GetCurrentId";
                OutputErrorLog(message);
                throw new Exception(message);
            }

            return (string)mboOut.GetPropertyValue("ObjectId");
        }

        private static string GetDefaultId()
        {
            var bcdObject = GetBcdObject();

            ManagementBaseObject mboOut;
            var success = bcdObject.GetElement((uint)BcdBootMgrElementTypes.BcdBootMgrObject_DefaultObject, out mboOut);
            if (!success)
            {
                var message = "Failed to execute GetDefaultId";
                OutputErrorLog(message);
                throw new Exception(message);
            }

            return (string)mboOut.GetPropertyValue("Id");
        }

        private static IEnumerable<string> GetIds()
        {
            var bcdObject = GetBcdObject();

            ManagementBaseObject mboOut;
            var success = bcdObject.GetElement((uint)BcdBootMgrElementTypes.BcdBootMgrObjectList_DisplayOrder, out mboOut);
            if (!success)
            {
                var message = "Failed to execute GetIds";
                OutputErrorLog(message);
                throw new Exception(message);
            }

            return (string[])mboOut.GetPropertyValue("Ids");
        }

        private static string GetResumeId()
        {
            var bcdObject = GetBcdObject();

            ManagementBaseObject mboOut;
            var success = bcdObject.GetElement((uint)BcdBootMgrElementTypes.BcdBootMgrObject_ResumeObject, out mboOut);
            if (!success)
            {
                throw new Exception();
            }

            return (string)mboOut.GetPropertyValue("Id");
        }

        #endregion

        #endregion

    }

}
