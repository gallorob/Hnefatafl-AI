using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace TaflWPF.Helper
{
    /// <summary>
    /// Helper class to handle events through commands
    /// Courtesy of https://github.com/Bardaky
    /// </summary>
	public static class CommandHelper
    {
        /// <summary>
        /// Handlers dictionary
        /// DependencyObject ->
        /// Dictionary<string, Delegate> ->
        /// </summary>
		private static readonly Dictionary<DependencyObject, Dictionary<string, Delegate>> Handlers = new Dictionary<DependencyObject, Dictionary<string, Delegate>>();

        /// <summary>
        /// The new Command property to attach to the DependencyObject
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(CommandHelper), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// The new EventName property to attach to the DependencyObject
        /// </summary>
        public static readonly DependencyProperty EventNameProperty = DependencyProperty.RegisterAttached("EventName", typeof(string), typeof(CommandHelper), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, EventNameChanged));

        /// <summary>
        /// Get the EventName
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <returns>The EventName</returns>
        public static string GetEventName(DependencyObject sender)
        {
            return (string)sender.GetValue(EventNameProperty);
        }
        /// <summary>
        /// Set the EventName
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="value">The value to set EventName to</param>
		public static void SetEventName(DependencyObject sender, string value)
        {
            sender.SetValue(EventNameProperty, value);
        }
        /// <summary>
        /// Get the Command
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <returns>The Command</returns>
		public static ICommand GetCommand(DependencyObject sender)
        {
            return (ICommand)sender.GetValue(CommandProperty);
        }
        /// <summary>
        /// Set the Command
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="value">The value to set Command to</param>
		public static void SetCommand(DependencyObject sender, ICommand value)
        {
            sender.SetValue(CommandProperty, value);
        }
        /// <summary>
        /// Handle what happens when the EventName changes
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject</param>
        /// <param name="eventArgs">The DependencyPropertyChangedEventArgs</param>
		private static void EventNameChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            // Sanity check (1): Make sure the NewValue is not null and is a string
            if (eventArgs.NewValue != null && eventArgs.NewValue is string name)
            {
                // cycle through all events of the dependencyObject
                foreach (var objEvent in dependencyObject.GetType().GetEvents())
                {
                    // if the event name is the NewValue
                    if (objEvent.Name == name)
                    {
                        // prepare a delegate
                        Delegate handler = null;
                        // define the new handler accordingly to the objEvent's EventHandlerType, redefining its behaviour through ExecuteCommand
                        switch (objEvent.EventHandlerType.Name)
                        {
                            case nameof(KeyboardEventHandler):
                                handler = new KeyboardEventHandler((sender, args) =>
                                {
                                    ExecuteCommand(dependencyObject, args);
                                });
                                break;
                            case nameof(InputEventHandler):
                                handler = new InputEventHandler((sender, args) =>
                                {
                                    ExecuteCommand(dependencyObject, args);
                                });
                                break;
                            case nameof(MouseButtonEventHandler):
                                handler = new MouseButtonEventHandler((sender, args) =>
                                {
                                    ExecuteCommand(dependencyObject, args);
                                });
                                break;
                            case nameof(RoutedEventHandler):
                            default:
                                handler = new RoutedEventHandler((sender, args) =>
                                {
                                    ExecuteCommand(dependencyObject, args);
                                });
                                break;
                        }
                        // if none of the types applied, the handler is still null
                        if (handler == null)
                        {
                            // in this case, nothing should happen
                            // (the event is handled in the standard way)
                            return;
                        }
                        // otherwise, add the newly set handler to the objEvent for the dependencyObject
                        objEvent.AddEventHandler(dependencyObject, handler);
                        // update the dictionary
                        // check if the dictionary contains the dependencyObject
                        if (Handlers.ContainsKey(dependencyObject))
                        {
                            // this should always be true, but make sure it contains the event name
                            if (Handlers[dependencyObject].ContainsKey(name))
                            {
                                // set the handler value
                                Handlers[dependencyObject][name] = handler;
                            }
                            // if, for some reason, the dictionary doesn't contain the event name
                            else
                            {
                                // add it
                                Handlers[dependencyObject] = new Dictionary<string, Delegate> { { name, handler } };
                            }
                        }
                        // if it doesn't contain the entry for the dependencyObject
                        else
                        {
                            // add it
                            Handlers.Add(dependencyObject, new Dictionary<string, Delegate> { { name, handler } });
                        }
                    }
                }
            }
            // if the newValue is null, remove the handler set previously (if set)
            else
            {
                // make sure the OldValue is a string
                if (eventArgs.OldValue is string old)
                {
                    // cycle through all events of the dependencyObject
                    foreach (var oldEvent in dependencyObject.GetType().GetEvents())
                    {
                        // find the oldEvent to de-handle
                        if (oldEvent.Name == old)
                        {
                            // check if the dependencyObject was handled through a custom EventHandler
                            if (Handlers.ContainsKey(dependencyObject))
                            {
                                // and also check the eventName is the same
                                if (Handlers[dependencyObject].ContainsKey(old))
                                {
                                    // then remove the event handler from the event for the dependencyObject
                                    oldEvent.RemoveEventHandler(dependencyObject, Handlers[dependencyObject][old]);
                                    // and remove the entry from the dictionary
                                    Handlers.Remove(dependencyObject);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Define the behaviour of a custom event handler
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The event arguments</param>
		private static void ExecuteCommand(DependencyObject sender, object args)
        {
            // check the dictionary entry for the dependency object sender
            if (Handlers.ContainsKey(sender))
            {
                // check the dictionary for the event name of the sender
                if (Handlers[sender].ContainsKey(GetEventName(sender)))
                {
                    // get the command associated to the dependency object
                    if (GetCommand(sender) is ICommand command)
                    {
                        // finally, execute the command, passing it the same arguments of the event handler
                        command.Execute(args);
                    }
                }
            }
        }
    }
}
