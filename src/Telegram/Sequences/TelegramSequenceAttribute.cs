namespace GBotV2.Telegram.Sequences;

class TelegramSequenceAttribute(SequenceTrigger trigger) : Attribute
{
    public SequenceTrigger Trigger { get; } = trigger;
}
