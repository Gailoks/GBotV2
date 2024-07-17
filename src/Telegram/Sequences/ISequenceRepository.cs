namespace GBotV2.Telegram.Sequences;

interface ISequenceRepository
{
    public void Load();

    public IEnumerable<TelegramSequence> List();
}
