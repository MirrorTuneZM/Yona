using Avalonia.ReactiveUI;
using ReactiveUI;
using Yona.Core.ViewModels.Dashboard.Projects;

namespace Yona.Desktop.Views.Dashboard.Projects;

public partial class ProjectsView : ReactiveUserControl<ProjectsViewModel>
{
    public ProjectsView()
    {
        InitializeComponent();
        this.WhenActivated(disposables => { });
    }
}