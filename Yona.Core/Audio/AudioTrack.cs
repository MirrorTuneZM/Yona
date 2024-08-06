﻿using CommunityToolkit.Mvvm.ComponentModel;
using Yona.Core.Audio;

namespace Yona.Core.Projects.Models;

public partial class AudioTrack : ObservableObject
{
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string? category;

    [ObservableProperty]
    private string[] tags = [];

    [ObservableProperty]
    private string? outputPath;

    [ObservableProperty]
    private string? inputFile;

    [ObservableProperty]
    private string? encoder;

    [ObservableProperty]
    private Loop loop = new();
}