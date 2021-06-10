using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Totalview.BlazorMvvm
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable, IAsyncDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Method invoked when the component is ready to start, having received its initial
        /// parameters from its parent in the render tree.
        /// </summary>
        public virtual void OnInitialized()
        {

        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its initial
        /// parameters from its parent in the render tree. Override this method if you will
        /// perform an asynchronous operation and want the component to refresh when that
        /// operation is completed.
        /// </summary>
        /// <returns>
        /// A System.Threading.Tasks.Task representing any asynchronous operation.
        /// </returns>
        public virtual Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        /// <remarks>
        /// The Microsoft.AspNetCore.Components.ComponentBase.OnAfterRender(System.Boolean)
        /// and Microsoft.AspNetCore.Components.ComponentBase.OnAfterRenderAsync(System.Boolean)
        /// lifecycle methods are useful for performing interop, or interacting with values
        /// recieved from @ref. Use the firstRender parameter to ensure that initialization
        /// work is only performed once.
        /// </remarks>
        /// <param name="firstRender">
        /// Set to true if this is the first time Microsoft.AspNetCore.Components.ComponentBase.OnAfterRender(System.Boolean)
        /// has been invoked on this component instance; otherwise false.
        /// </param>
        public virtual void AfterRender(bool firstRender)
        {
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered. Note that the
        /// component does not automatically re-render after the completion of any returned
        /// System.Threading.Tasks.Task, because that would cause an infinite render loop.
        /// </summary>
        /// <param name="firstRender">
        /// Set to true if this is the first time Microsoft.AspNetCore.Components.ComponentBase.OnAfterRender(System.Boolean)
        /// has been invoked on this component instance; otherwise false.
        /// </param>
        /// <returns>
        /// A System.Threading.Tasks.Task representing any asynchronous operation.
        /// </returns>
        /// <remarks>
        /// The Microsoft.AspNetCore.Components.ComponentBase.OnAfterRender(System.Boolean)
        /// and Microsoft.AspNetCore.Components.ComponentBase.OnAfterRenderAsync(System.Boolean)
        /// lifecycle methods are useful for performing interop, or interacting with values
        /// recieved from @ref. Use the firstRender parameter to ensure that initialization
        /// work is only performed once.
        /// </remarks>
        public virtual Task OnAfterRenderAsync(bool firstRender)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in
        /// the render tree, and the incoming values have been assigned to properties.
        /// </summary>
        public virtual void OnParametersSet()
        {
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in
        /// the render tree, and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>
        /// A System.Threading.Tasks.Task representing any asynchronous operation.
        /// </returns>
        public virtual Task OnParametersSetAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Sets parameters supplied by the component's parent in the render tree.
        /// </summary>
        /// <param name="parameters">
        /// A System.Threading.Tasks.Task that completes when the component has finished
        /// updating and rendering itself.
        /// </param>
        /// <returns>
        /// The Microsoft.AspNetCore.Components.ComponentBase.SetParametersAsync(Microsoft.AspNetCore.Components.ParameterView)
        /// method should be passed the entire set of parameter values each time Microsoft.AspNetCore.Components.ComponentBase.SetParametersAsync(Microsoft.AspNetCore.Components.ParameterView)
        /// is called. It not required that the caller supply a parameter value for all parameters
        /// that are logically understood by the component.
        /// The default implementation of Microsoft.AspNetCore.Components.ComponentBase.SetParametersAsync(Microsoft.AspNetCore.Components.ParameterView)
        /// will set the value of each property decorated with Microsoft.AspNetCore.Components.ParameterAttribute
        /// or Microsoft.AspNetCore.Components.CascadingParameterAttribute that has a corresponding
        /// value in the Microsoft.AspNetCore.Components.ParameterView. Parameters that do
        /// not have a corresponding value will be unchanged.
        /// </returns>
        public virtual Task SetParametersAsync(ParameterView parameters)
        {
            IReadOnlyDictionary<string, object> parametersDictionary = parameters.ToDictionary();

            if (parametersDictionary != null)
            {
                Type type = GetType();
                foreach (var v in parametersDictionary)
                {
                    PropertyInfo prop = type.GetProperty(v.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.IgnoreCase);
                    if (prop != null)
                    {
                        prop.SetValue(this, v.Value);
                    }
                }
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Can be implemented by derived classes
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual async Task DisposeAsync(bool disposing)
        {
            // Can be implemented by derived classes
        }

        /// <summary>
        /// Sets a properties storage field with a given value. 
        /// Then raises the PropertyChanged event fror that property name
        /// Will only raise the Property chaged event if the property actually changes
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="storage">Storage field reference</param>
        /// <param name="value">The new value</param>
        /// <param name="propertyName">Name of the property, will be set automatically with CallerMemberName</param>
        /// <returns>True if property was changed, else false</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        protected void RaisePropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
