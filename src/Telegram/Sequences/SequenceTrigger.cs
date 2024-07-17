using Telegram.Bot.Types;

namespace GBotV2.Telegram.Sequences;

abstract class SequenceTrigger : WaitCondition
{
    public virtual Task<bool> CheckCommandAsync(string command, Message rawMessage) => Task.FromResult(false);
}