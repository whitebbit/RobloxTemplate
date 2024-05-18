using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using InstantGamesBridge;
using InstantGamesBridge.Common;
using InstantGamesBridge.Modules.Platform;
using InstantGamesBridge.Modules.Storage;
#if GB_NEWTONSOFT_FOR_SAVES
using Newtonsoft.Json;
#endif
using UnityEngine;

namespace GBGamesPlugin
{
    public partial class GBGames
    {
        public static SavesGB saves = new();

        private static SavesGB _platformInternalSaves;
        private static SavesGB _localStorageSaves;

        public static event Action SaveLoadedCallback;

        private const string SavesID = "saves";

        /// <summary>
        /// Тип хранилища по умолчанию. Используется автоматически, если при работе с данными не указывать конкретный тип.
        /// </summary>
        public static StorageType storageDefaultType => Bridge.storage.defaultType;

        /// <summary>
        /// Проверка на поддержку.
        /// </summary>
        public static bool IsSupportedStorageType(StorageType storageType) =>
            Bridge.storage.IsSupported(storageType);

        /// <summary>
        /// Проверка на доступность.
        /// </summary>
        public static bool IsAvailableStorageType(StorageType storageType) =>
            Bridge.storage.IsAvailable(storageType);


        #region Load

        ///<summary>
        /// Загрузка данных.
        /// </summary>
        private void Load()
        {
            StartCoroutine(FetchData());
        }

        private IEnumerator FetchData()
        {
            if (IsSupportedStorageType(StorageType.PlatformInternal) &&
                IsAvailableStorageType(StorageType.PlatformInternal) && instance.settings.useCloudSaves)
                yield return LoadByStorageType(StorageType.PlatformInternal);

            if (IsSupportedStorageType(StorageType.LocalStorage) &&
                IsAvailableStorageType(StorageType.LocalStorage))
                yield return LoadByStorageType(StorageType.LocalStorage);

            saves = _platformInternalSaves != null && _localStorageSaves != null &&
                    _platformInternalSaves.saveID > _localStorageSaves.saveID
                ? _platformInternalSaves
                : _localStorageSaves ?? _platformInternalSaves;

            SaveLoadedCallback?.Invoke();
        }

        private IEnumerator LoadByStorageType(StorageType storageType)
        {
            var loader = 0;
            var result = new SavesGB();

            Bridge.storage.Get(SavesID, (success, data) =>
            {
                if (success)
                {
#if GB_NEWTONSOFT_FOR_SAVES
                    try
                    {
                        result = string.IsNullOrEmpty(data)
                            ? new SavesGB()
                            : JsonConvert.DeserializeObject<SavesGB>(data);
                    }
                    catch (Exception e)
                    {
                        Message(e.Message, LoggerState.error);
                        result = new SavesGB();
                        throw;
                    }
#else
                    result = new SavesGB();
#endif
                    Message($"{storageType} loading saves success. Data - {data}");
                }
                else
                {
                    Message($"{storageType} loading failed", LoggerState.error);
                }

                loader += 1;
            }, storageType);

            yield return new WaitUntil(() => loader >= 1);

            switch (storageType)
            {
                case StorageType.LocalStorage:
                    _localStorageSaves = result;
                    break;
                case StorageType.PlatformInternal:
                    _platformInternalSaves = result;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(storageType), storageType, null);
            }
        }

        #endregion

        #region Save

        ///<summary>
        /// Сохранение данных.
        /// </summary>
        public void Save()
        {
            saves.saveID++;
            StartCoroutine(SaveData());
        }

        private static IEnumerator SaveData()
        {
            if (IsSupportedStorageType(StorageType.PlatformInternal) &&
                IsAvailableStorageType(StorageType.PlatformInternal) && instance.settings.useCloudSaves)
                yield return SaveByStorageType(StorageType.PlatformInternal);

            if (IsSupportedStorageType(StorageType.LocalStorage) &&
                IsAvailableStorageType(StorageType.LocalStorage))
                yield return SaveByStorageType(StorageType.LocalStorage);
        }

        private static IEnumerator SaveByStorageType(StorageType storageType)
        {
#if GB_NEWTONSOFT_FOR_SAVES
            var data = JsonConvert.SerializeObject(saves);
            var saver = 0;
#else
            var data = "";
#endif
            Bridge.storage.Set(SavesID, data, (success) =>
            {
                if (success)
                {
                    Message($"{storageType} save data - {data} success ");
                }
                else
                {
                    Message($"{storageType} save data - {data} failed", LoggerState.error);
                }

                saver += 1;
            }, storageType);

            yield return new WaitUntil(() => saver >= 1);
        }

        private IEnumerator IntervalSave()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(60 * instance.settings.saveInterval);
                Save();
            }
        }

        #endregion

        #region Delete

        public static void Delete()
        {
            var id = saves.saveID;
            saves = new SavesGB {saveID = id};
            instance.Save();
        }

        #endregion
    }
}