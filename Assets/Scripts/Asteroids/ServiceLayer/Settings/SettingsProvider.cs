using System;
using System.Collections.Generic;
using Asteroids.ServiceLayer.Settings.Converters;
using Asteroids.SimulationLayer.Helpers;
using Asteroids.SimulationLayer.Settings;

namespace Asteroids.ServiceLayer.Settings
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly Dictionary<string, string> _settings;
        private readonly Dictionary<Type, ISettingsConverter> _converters;

        public SettingsProvider(GameplaySettings settings, params ISettingsConverter[] parsers)
        {
            _settings = new Dictionary<string, string>();
            
            foreach (var scope in settings.Settings)
            {
                foreach (var entry in scope.Settings)
                {
                    _settings.Add(scope.Id + "." + entry.Id, entry.Value);
                }
            }

            _converters = new Dictionary<Type, ISettingsConverter>();

            foreach (var parser in parsers)
            {
                _converters.Add(parser.Type, parser);
            }
        }

        public T GetValue<T>(Enum settingId)
        {
            var id = settingId.ToFullString();
            
            if (!_settings.TryGetValue(id, out var entry))
            {
                throw new ArgumentException($"[{GetType().Name}] no settings entry with id={id}");
            }

            if (!_converters.TryGetValue(typeof(T), out var converter) || converter is not SettingsConverter<T> typedConverter)
            {
                throw new ArgumentException($"[{GetType().Name}] no settings entry with id={id} convertible to {typeof(T)}");
            }

            return typedConverter.Convert(entry);
        }
    }
}