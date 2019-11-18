﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using NLog;

namespace FSyncLib
{

    public class FSync
    {
        /// <summary>
        ///     Nlog
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Configuration _configuration;

        /// <summary>
        ///     Таймер для задания периода сканирования каталога
        /// </summary>
        private Timer _timer;

        /// <summary>
        ///     Получение в указанной папке файлы с указанным расширением
        /// </summary>
        /// <param name="folder"> Папка из которой получаем файлы </param>
        /// <param name="extension"> Расширение, по которому получаем файлы </param>
        /// <returns> Список файлов </returns>
        private List<string> GetFiles(string folder, string extension)
        {
            var files = Directory.EnumerateFiles(folder, $"*.{extension}", SearchOption.TopDirectoryOnly).ToList();
            return files;
        }

        /// <summary>
        ///     Список каталого с картинками (жипег и пр) , исключаемые при построения дерева в директории-приёмнике
        /// </summary>
        public List<string> Exclusions;

        /// <summary>
        ///     Список расширений для создания жёсткой ссылки в папке-приёмнике
        /// </summary>
        public List<string> Extensions;

        /// <summary>
        ///     Период проверки каталога
        /// </summary>
        public int Period;

        /// <summary>
        ///     Размерность периода проверки каталога
        /// </summary>
        public Measure Measure;

        /// <summary>
        ///     Тип операции
        /// </summary>
        public OperationType OperationType;

        /// <summary>
        ///     Количество новых файлов для копирования
        /// </summary>
        public int NumberFilesToCopy;

        /// <summary>
        ///     Запись в лог
        /// </summary>
        public bool WriteToLog;

        /// <summary>
        ///     Рекурсивное копирование
        /// </summary>
        public bool Recursive;

        /// <summary>
        ///     Папка-источник
        /// </summary>
        public string SourceFolder;

        /// <summary>
        ///     Папка-приёмник
        /// </summary>
        public string DestFolder;

        /// <summary>
        ///     Создание жёсткой ссылки
        /// https://stackoverflow.com/questions/3387690/how-to-create-a-hardlink-in-c
        /// </summary>
        /// <param name="lpFileName"> Имя файла-ссылки </param>
        /// <param name="lpExistingFileName"> Имя файла-источника </param>
        /// <param name="lpSecurityAttributes"></param>
        /// <returns> Успешность создания ссылки </returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool CreateHardLink(
            string lpFileName,
            string lpExistingFileName,
            IntPtr lpSecurityAttributes);

        /// <summary>
        ///     Получить доступ к общему файлу конфигурации для всех сборок
        /// </summary>
        /// <returns></returns>
        public static Configuration GetConfiguration()
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (assemblyFolder != null)
                map.ExeConfigFilename = Path.Combine(assemblyFolder, "Config.file");

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            return config;
        }

        /// <summary>
        ///     Сканирование директории источника и постороение аналогичной иерархии папок в директории-приёмнике. 
        /// Проверка на соответствие файлов в источнике и приёмнике. Если есть различия - создаются жёсткие ссылки
        /// </summary>
        /// <param name="sourcePath"> Полный путь к папке-источнику </param>
        /// <param name="destPath"> Полный путь к папке-приёмнику </param>
        public void SyncFoldersTree(string sourcePath, string destPath)
        {
            string slash = "\\";
            // Считывание из настроек списка папок исключённых из построения дерева, файлы из них просто копируются в директорию назначения
            Exclusions = _configuration.AppSettings.Settings[nameof(Exclusions)].Value.Split(';').ToList();
            // Считываение из настроек расширений, для которых создаются жёсткие ссылки
            Extensions = _configuration.AppSettings.Settings[nameof(Extensions)].Value.Split(';').ToList();


            var subFolders = Recursive
                ? Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories)
                    .Where(d => !Exclusions.Exists(e => e == Path.GetFileName(d)))
                    .ToList()
                : new List<string> {sourcePath};
            subFolders.Add(sourcePath);
            // Добавляем слеш в конец папки-приёмника, если отсутствует
            if (destPath.Substring(destPath.Length - 1) == slash)
                destPath = destPath.Substring(0, destPath.Length - 1);
            if (!Exclusions.Contains(String.Empty))
                Exclusions.Add(String.Empty);
            
            if (WriteToLog)
                Logger.Info("Start of synchronization...");

            foreach (var sourceSubFolder in subFolders)
            {
                // Относительный путь в папке-источнике
                var subPath = sourceSubFolder.Substring(sourcePath.Length);
                // Путь к папке-приёмнику
                var destSubFolder = destPath + subPath;
                // Создание папки (если не существует)
                Directory.CreateDirectory(destSubFolder);

                foreach (var exclusion in Exclusions)
                {
                    var sourceFolder = Path.Combine(sourceSubFolder, exclusion);
                    if (Directory.Exists(sourceFolder))
                    {
                        foreach (var extension in Extensions)
                        {
                            List<string> sourceFiles = GetFiles(sourceFolder, extension);
                            if (NumberFilesToCopy > 0)
                            {
                                // Упорядочиваем по времени и берём NumberFilesToCopy последних
                                sourceFiles = sourceFiles
                                    .Select(f => new KeyValuePair<string, DateTime>(f, new FileInfo(f).LastWriteTime))
                                    .OrderByDescending(pair => pair.Value)
                                    .Take(NumberFilesToCopy)
                                    .Select(pair => pair.Key)
                                    .ToList();
                            }
                            if (sourceFiles.Count > 0)
                            {
                                var destFiles = GetFiles(destSubFolder, extension);
                                var missingFiles = sourceFiles.Except(destFiles).ToList();
                                // Создание ссылки на файл
                                foreach (var missingFile in missingFiles)
                                {
                                    var missingFileName = Path.GetFileName(missingFile);
                                    if (missingFileName != null)
                                    {
                                        var newFileName = Path.Combine(destSubFolder, missingFileName);
                                        try
                                        {
                                            if (OperationType == OperationType.HardLink)
                                            {
                                                CreateHardLink(newFileName, missingFile, IntPtr.Zero);
                                            }
                                            else
                                            {
                                                File.Copy(missingFile, newFileName, true);
                                            }
                                            if (WriteToLog)
                                                Logger.Info($"{newFileName} - Ok");
                                        }
                                        catch (Exception e)
                                        {
                                            if (WriteToLog)
                                            {
                                                Logger.Info($"{newFileName} - Failure");
                                            }
                                            Logger.Error(e);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (WriteToLog)
                Logger.Info("End of synchronization");
        }

        /// <summary>
        ///     Начало сканирования каталога на наличие изменений
        /// </summary>
        public void StartChecking()
        {
            SourceFolder = _configuration.AppSettings.Settings[nameof(SourceFolder)].Value;

            DestFolder = _configuration.AppSettings.Settings[nameof(DestFolder)].Value;

            TimerCallback timerCallback = state => { SyncFoldersTree(SourceFolder, DestFolder); };

            Period = 0;
            Measure = _configuration.AppSettings.Settings[nameof(Measure)].Value.ToMeasure();
            var period = _configuration.AppSettings.Settings[nameof(Period)].Value;
            Int32.TryParse(period, out Period);

            OperationType = _configuration.AppSettings.Settings[nameof(OperationType)].Value.ToOperationType();

            var numberFilesToCopy = _configuration.AppSettings.Settings[nameof(NumberFilesToCopy)].Value;
            Int32.TryParse(numberFilesToCopy, out NumberFilesToCopy);

            WriteToLog = Convert.ToBoolean(_configuration.AppSettings.Settings[nameof(WriteToLog)].Value);

            Recursive = Convert.ToBoolean(_configuration.AppSettings.Settings[nameof(Recursive)].Value);

            // создаем таймер
            _timer = new Timer(timerCallback, null, 0, Period * (int) Measure);
        }

        /// <summary>
        ///     Окончание сканирования каталога
        /// </summary>
        public void StopChecking()
        {
            _timer.Dispose();
        }

        public FSync()
        {
            _configuration = GetConfiguration();
        }
    }
}