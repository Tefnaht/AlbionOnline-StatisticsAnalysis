﻿using StatisticsAnalysisTool.Common.UserSettings;
using StatisticsAnalysisTool.Enumerations;
using StatisticsAnalysisTool.Network.Manager;
using StatisticsAnalysisTool.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatisticsAnalysisTool.Common;

public class CriticalData
{
    private static SaveOnClosing _saveOnClosing;
    private static readonly List<Task> TaskList = new();

    public static async Task SaveAsync()
    {
        if (_saveOnClosing is SaveOnClosing.IsRunning or SaveOnClosing.Done)
        {
            return;
        }

        _saveOnClosing = SaveOnClosing.IsRunning;

        var trackingController = ServiceLocator.Resolve<TrackingController>();
        var mainWindowViewModel = ServiceLocator.Resolve<MainWindowViewModel>();

        var task = Task.Factory.StartNew(trackingController.StopTracking);
        var task2 = Task.Factory.StartNew(mainWindowViewModel.SaveLootLogger);
        var task3 = Task.Factory.StartNew(SettingsController.SaveSettings);
        var task4 = Task.Factory.StartNew(trackingController.SaveData);

        TaskList.Add(task);
        TaskList.Add(task2);
        TaskList.Add(task3);
        TaskList.Add(task4);
        await Task.WhenAll(TaskList.ToArray());

        _saveOnClosing = SaveOnClosing.Done;
    }

    public static SaveOnClosing GetStatus()
    {
        return _saveOnClosing;
    }
}