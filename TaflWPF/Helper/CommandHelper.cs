using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace TaflWPF.Helper
{
	public static class CommandHelper
	{
		private static readonly Dictionary<DependencyObject, Dictionary<string, Delegate>> Handlers = new Dictionary<DependencyObject, Dictionary<string, Delegate>>();

		public static string GetEventName(DependencyObject sender)
		{
			return (string)sender.GetValue(EventNameProperty);
		}

		public static void SetEventName(DependencyObject sender, string value)
		{
			sender.SetValue(EventNameProperty, value);
		}

		// Using a DependencyProperty as the backing store for EventName.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EventNameProperty = DependencyProperty.RegisterAttached("EventName", typeof(string), typeof(CommandHelper), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, EventNameChanged));

		public static ICommand GetCommand(DependencyObject sender)
		{
			return (ICommand)sender.GetValue(CommandProperty);
		}

		public static void SetCommand(DependencyObject sender, ICommand value)
		{
			sender.SetValue(CommandProperty, value);
		}

		// Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(CommandHelper), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

		private static void EventNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
			{
				if (e.NewValue is string name)
					foreach (var eve in d.GetType().GetEvents())
					{
						if (eve.Name == name)
						{
							Delegate handler = null;

							switch (eve.EventHandlerType.Name)
							{
								case nameof(KeyEventHandler):
									handler = new KeyEventHandler((sender, args) =>
									{
										ExecuteCommand(d, args);
									});
									break;
								case nameof(KeyboardEventHandler):
									handler = new KeyboardEventHandler((sender, args) =>
									{
										ExecuteCommand(d, args);
									});
									break;
								case nameof(MouseEventHandler):
									handler = new MouseEventHandler((sender, args) =>
									{
										ExecuteCommand(d, args);
									});
									break;
								case nameof(InputEventHandler):
									handler = new InputEventHandler((sender, args) =>
									{
										ExecuteCommand(d, args);
									});
									break;
								case nameof(MouseButtonEventHandler):
									handler = new MouseButtonEventHandler((sender, args) =>
									{
										ExecuteCommand(d, args);
									});
									break;
								case nameof(RoutedEventHandler):
								default:
									handler = new RoutedEventHandler((sender, args) =>
									{
										ExecuteCommand(d, args);
									});
									break;
							}

							if (handler == null)
								return;

							eve.AddEventHandler(d, handler);

							if (Handlers.ContainsKey(d))
								if (Handlers[d].ContainsKey(name))
									Handlers[d][name] = handler;
								else
									Handlers[d] = new Dictionary<string, Delegate> { { name, handler } };
							else
								Handlers.Add(d, new Dictionary<string, Delegate> { { name, handler } });
						}
					}
			}
			else
			{
				if (e.OldValue is string old)
					foreach (var oldeve in d.GetType().GetEvents())
						if (oldeve.Name == old)
							if (Handlers.ContainsKey(d))
								if (Handlers[d].ContainsKey(old))
								{
									oldeve.RemoveEventHandler(d, Handlers[d][old]);
									Handlers.Remove(d);
								}
			}
		}

		private static void ExecuteCommand(DependencyObject sender, object args)
		{
			if (Handlers.ContainsKey(sender))
				if (Handlers[sender].ContainsKey(GetEventName(sender)))
					if (GetCommand(sender) is ICommand command)
						command.Execute(args);
		}
	}
}
