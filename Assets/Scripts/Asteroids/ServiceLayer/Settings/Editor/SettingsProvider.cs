using System;
using System.Collections.Generic;
using Asteroids.ServiceLayer.Settings.Parsers;
using Asteroids.SimulationLayer.Settings;

namespace Asteroids.ServiceLayer.Settings.Editor
{
    public class SettingsProvider : ISettingsProvider
    {
        private Dictionary<string, SettingsEntry> _settings;
        private Dictionary<Type, ISettingsConverter> _converters;

        public SettingsProvider(GameplaySettings settings, params ISettingsConverter[] parsers)
        {
            _settings = new Dictionary<string, SettingsEntry>();
            
            foreach (var entry in settings.Settings)
            {
                _settings.Add(entry.Id, entry);
            }

            _converters = new Dictionary<Type, ISettingsConverter>();

            foreach (var parser in parsers)
            {
                _converters.Add(parser.Type, parser);
            }
        }

        public T GetValue<T>(string id)
        {
            if (!_settings.TryGetValue(id, out var entry))
            {
                throw new ArgumentException($"[{GetType().Name}] no settings entry with id={id}");
            }

            if (!_converters.TryGetValue(typeof(T), out var converter) || converter is not SettingsConverter<T> typedConverter)
            {
                throw new ArgumentException($"[{GetType().Name}] no settings entry with id={id} convertible to {typeof(T)}");
            }

            return typedConverter.Convert(entry.Value);
        }
    }
}