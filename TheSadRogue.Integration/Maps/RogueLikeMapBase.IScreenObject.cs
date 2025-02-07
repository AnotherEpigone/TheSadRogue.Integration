﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SadConsole;
using SadConsole.Components;
using SadConsole.Entities;
using SadConsole.Input;
using SadRogue.Primitives;

namespace SadRogue.Integration.Maps
{
    public abstract partial class RogueLikeMapBase
    {
        /// <summary>
        /// The IScreenObject acting as the object for the IScreenObject forwarder implementation.
        /// </summary>
        protected IScreenObject BackingObject { get; set; }

        /// <inheritdoc/>
        public virtual void Render(TimeSpan delta) => BackingObject.Render(delta);

        /// <inheritdoc/>
        public virtual void OnFocused() => BackingObject.OnFocused();

        /// <inheritdoc/>
        public virtual void OnFocusLost() => BackingObject.OnFocusLost();

        /// <inheritdoc/>
        TComponent IScreenObject.GetSadComponent<TComponent>() => BackingObject.GetSadComponent<TComponent>();

        /// <inheritdoc/>
        IEnumerable<TComponent> IScreenObject.GetSadComponents<TComponent>() => BackingObject.GetSadComponents<TComponent>();

        /// <inheritdoc/>
        bool IScreenObject.HasSadComponent<TComponent>(out TComponent component) => BackingObject.HasSadComponent(out component);

        /// <inheritdoc/>
        public virtual bool ProcessKeyboard(Keyboard keyboard) => BackingObject.ProcessKeyboard(keyboard);

        /// <inheritdoc/>
        bool IScreenObject.ProcessMouse(MouseScreenObjectState state)
        {
            // Transformation is required to ensure that backing objects which are surfaces are handled correctly.
            // See https://github.com/thesadrogue/TheSadRogue.Integration/issues/47.
            state = new MouseScreenObjectState(BackingObject, state.Mouse.Clone());
            return ProcessMouse(state);
        }

        /// <summary>
        /// Overridable implementation of IScreenObject's ProcessMouse which is guaranteed to receive a mouse state
        /// appropriate for the map implementation.
        /// </summary>
        /// <param name="state"/>
        /// <returns/>
        protected virtual bool ProcessMouse(MouseScreenObjectState state) => BackingObject.ProcessMouse(state);

        /// <inheritdoc/>
        public virtual void LostMouse(MouseScreenObjectState state) => BackingObject.LostMouse(state);

        /// <summary>
        /// Calls Update for all entities, then Updates all SadComponents and Children. Only processes if IsEnabled is
        /// true.
        /// </summary>
        /// <param name="delta">Time since last update.</param>
        public virtual void Update(TimeSpan delta)
        {
            if (!IsEnabled) return;

            foreach (var entity in Entities.Items)
            {
                // Guaranteed to succeed since all must be RoguelikeEntities
                var scEntity = (Entity)entity;
                scEntity.Update(delta);
            }

            BackingObject.Update(delta);
        }

        /// <inheritdoc/>
        public virtual void UpdateAbsolutePosition() => BackingObject.UpdateAbsolutePosition();

        /// <inheritdoc/>
        public FocusBehavior FocusedMode
        {
            get => BackingObject.FocusedMode;
            set => BackingObject.FocusedMode = value;
        }

        /// <inheritdoc/>
        public Point AbsolutePosition => BackingObject.AbsolutePosition;

        /// <inheritdoc/>
        public ScreenObjectCollection Children => BackingObject.Children;

        /// <inheritdoc/>
        public ObservableCollection<IComponent> SadComponents => BackingObject.SadComponents;

        /// <inheritdoc/>
        public bool IsEnabled
        {
            get => BackingObject.IsEnabled;
            set => BackingObject.IsEnabled = value;
        }

        /// <inheritdoc/>
        public bool IsExclusiveMouse
        {
            get => BackingObject.IsExclusiveMouse;
            set => BackingObject.IsExclusiveMouse = value;
        }

        /// <inheritdoc/>
        public bool IsFocused
        {
            get => BackingObject.IsFocused;
            set => BackingObject.IsFocused = value;
        }

        /// <inheritdoc/>
        public bool IsVisible
        {
            get => BackingObject.IsVisible;
            set => BackingObject.IsVisible = value;
        }

        /// <inheritdoc/>
        public IScreenObject Parent
        {
            get => BackingObject.Parent;
            set => BackingObject.Parent = value;
        }

        /// <inheritdoc/>
        public Point Position
        {
            get => BackingObject.Position;
            set => BackingObject.Position = value;
        }

        /// <inheritdoc/>
        public bool UseKeyboard
        {
            get => BackingObject.UseKeyboard;
            set => BackingObject.UseKeyboard = value;
        }

        /// <inheritdoc/>
        public bool UseMouse
        {
            get => BackingObject.UseMouse;
            set => BackingObject.UseMouse = value;
        }

        /// <inheritdoc/>
        public event EventHandler EnabledChanged
        {
            add => BackingObject.EnabledChanged += value;
            remove => BackingObject.EnabledChanged -= value;
        }

        /// <inheritdoc/>
        public event EventHandler<ValueChangedEventArgs<IScreenObject>> ParentChanged
        {
            add => BackingObject.ParentChanged += value;
            remove => BackingObject.ParentChanged -= value;
        }

        /// <inheritdoc/>
        public event EventHandler<ValueChangedEventArgs<Point>> PositionChanged
        {
            add => BackingObject.PositionChanged += value;
            remove => BackingObject.PositionChanged -= value;
        }

        /// <inheritdoc/>
        public event EventHandler VisibleChanged
        {
            add => BackingObject.VisibleChanged += value;
            remove => BackingObject.VisibleChanged -= value;
        }

        /// <inheritdoc/>
        public event EventHandler FocusLost
        {
            add => BackingObject.FocusLost += value;
            remove => BackingObject.FocusLost -= value;
        }

        /// <inheritdoc/>
        public event EventHandler Focused
        {
            add => BackingObject.Focused += value;
            remove => BackingObject.Focused -= value;
        }
    }
}
