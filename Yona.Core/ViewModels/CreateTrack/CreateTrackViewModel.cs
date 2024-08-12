﻿using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System.Reactive.Linq;
using Yona.Core.Audio.Models;
using Yona.Core.Common.Interactions;
using Yona.Core.Projects.Models;

namespace Yona.Core.ViewModels.CreateTrack;

public partial class CreateTrackViewModel : ViewModelBase, IActivatableViewModel
{
    private readonly ProjectBundle project;

    public CreateTrackViewModel(ProjectBundle project, IEnumerable<string> encoders)
    {
        this.project = project;
        this.Encoders = encoders;

        this.Track = new();
        this.Track.Loop.Enabled = project.Data.DefaultLoopState;
        this.Track.Encoder = project.Data.DefaultEncoder;
        this.Track.OutputPath = project.Data.DefaultOutputPath;
    }

    public CreateTrackViewModel(AudioTrack track, ProjectBundle project, IEnumerable<string> encoders)
    {
        this.project = project;
        this.Track = track;
        this.Encoders = encoders;
        this.IsEditing = true;
    }

    public AudioTrack Track { get; }

    public bool IsEditing { get; }

    public CloseInteraction Close { get; } = new();

    public ViewModelActivator Activator { get; } = new();

    public IEnumerable<string> Encoders { get; }

    // TODO: More robust validation.
    public string Name
    {
        get => this.Track.Name;
        set
        {
            this.SetProperty(this.Track.Name, value, this.Track, (m, n) => m.Name = n);
            this.ConfirmCommand.NotifyCanExecuteChanged();
        }
    }

    // TODO: More robust validation.
    public string? OutputPath
    {
        get => this.Track.OutputPath;
        set
        {
            this.SetProperty(this.Track.OutputPath, value, this.Track, (m, n) => this.Track.OutputPath = n);
            this.ConfirmCommand.NotifyCanExecuteChanged();
        }
    }

    [RelayCommand]
    private void Delete()
    {
        try
        {
            this.project.Data.Tracks.Remove(this.Track);
            this.project.Save();
        }
        catch (Exception)
        {
            // TODO: Log error.
        }
    }

    private bool CanConfirm => !string.IsNullOrWhiteSpace(this.Name) && !string.IsNullOrWhiteSpace(this.OutputPath);

    [RelayCommand(CanExecute = nameof(CanConfirm))]
    private async Task Confirm()
    {
        try
        {
            if (this.IsEditing)
            {
                this.project.Save();
                await this.Close.Handle(new());
            }
            else
            {
                this.project.Data.Tracks.Add(this.Track);
                this.project.Save();
                await this.Close.Handle(new());
            }
        }
        catch (Exception)
        {
            // TODO: Log error.
        }
    }

    [RelayCommand]
    private async Task Cancel() => await this.Close.Handle(new());
}
