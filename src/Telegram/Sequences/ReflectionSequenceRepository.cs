
using System.Reflection;

namespace GBotV2.Telegram.Sequences;

class ReflectionSequenceRepository(IEnumerable<ITelegramSequenceModule> modules) : ISequenceRepository
{
    private readonly IEnumerable<ITelegramSequenceModule> _modules = modules;
    private readonly List<TelegramSequence> _sequences = [];


    public void Load()
    {
        foreach (var module in _modules)
        {
            var result = module
                .GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(s => new { Method = s, Attribute = s.GetCustomAttribute<TelegramSequenceAttribute>() })
                .Where(s => s.Attribute is not null)
                .Select(s =>
                {
                    var sequenceDelegate = s.Method.CreateDelegate<TelegramSequenceDelegate>(module);
                    return new TelegramSequence(sequenceDelegate, s.Attribute!.Trigger);
                });

            _sequences.AddRange(result);
        }
    }

    public IEnumerable<TelegramSequence> List()
    {
        return _sequences;
    }
}