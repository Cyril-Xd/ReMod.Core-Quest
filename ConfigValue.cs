using System;
using System.Linq;
using System.Reflection;
using MelonLoader;
using ReMod.Core.Managers;

namespace ReMod.Core
{
    public class ConfigValue<T>
    {
        public event Action OnValueChanged;

        private readonly MelonPreferences_Entry<T> _entry;

        public T Value => _entry.Value;
        public T DefaultValue => _entry.DefaultValue;

        public ConfigValue(string name, T defaultValue, string displayName = null, string description = null, bool isHidden = false, string filePath = null)
        {
            if(!ConfigManager.Instances.ContainsKey(Assembly.GetCallingAssembly().Location)) 
                throw new Exception("ConfigManager was not found. Please create it first.");
            
            var category = MelonPreferences.CreateCategory(ConfigManager.Instances[Assembly.GetCallingAssembly().Location]);
            if(filePath != null) category.SetFilePath(filePath);
            var entryName = string.Concat(name.Where(c => char.IsLetter(c) || char.IsNumber(c)));
            _entry = category.GetEntry<T>(entryName) ?? category.CreateEntry(entryName, defaultValue, displayName, description, isHidden);
            _entry.OnValueChangedUntyped += () => OnValueChanged?.Invoke();
        }

        public static implicit operator T(ConfigValue<T> conf)
        {
            return conf._entry.Value;
        }

        public void SetValue(T value)
        {
            _entry.Value = value;
            MelonPreferences.Save();
        }

        public override string ToString()
        {
            return _entry.Value.ToString();
        }
    }
}
