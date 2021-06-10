using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Totalview.BlazorMvvm
{
    public abstract class MvvmLayoutComponentBase<T> : LayoutComponentBase, IDisposable, IAsyncDisposable where T : ViewModelBase
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        [Inject] public T ViewModel { get; set; }

        private readonly System.Timers.Timer _shouldRenderDelayTimer;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        protected MvvmLayoutComponentBase()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            _shouldRenderDelayTimer = new System.Timers.Timer
            {
                AutoReset = false,
                Interval = 50
            };

            _shouldRenderDelayTimer.Elapsed += (o, e) =>
            {
                InvokeAsync(() =>
                {
                    StateHasChanged();
                    ShouldRender();
                });
            };
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await ViewModel.SetParametersAsync(parameters);
            await base.SetParametersAsync(parameters);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(disposing: true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected override void OnInitialized()
        {
            ViewModel.OnInitialized();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged; ;

            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            await ViewModel.OnInitializedAsync();
            await base.OnInitializedAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            ViewModel.AfterRender(firstRender);
            base.OnAfterRender(firstRender);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await ViewModel.OnAfterRenderAsync(firstRender);
            await base.OnAfterRenderAsync(firstRender);
        }

        protected override void OnParametersSet()
        {
            ViewModel.OnParametersSet();
            base.OnParametersSet();
        }

        protected override async Task OnParametersSetAsync()
        {
            await ViewModel.OnParametersSetAsync();
            await base.OnParametersSetAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ViewModel.Dispose();
            }
        }

        protected virtual async Task DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await ViewModel.DisposeAsync();
            }
        }

        /* ----------------------------------------------------------------------------  */
        /*                                EVENT HANDLERS                                 */
        /* ----------------------------------------------------------------------------  */
        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_shouldRenderDelayTimer.Enabled == false)
            {
                _shouldRenderDelayTimer.Start();
            }
            else
            {
                _shouldRenderDelayTimer.Stop();
                _shouldRenderDelayTimer.Start();
            }
        }
    }
}