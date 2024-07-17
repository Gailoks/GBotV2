using Telegram.Bot.Types;

namespace GBotV2.Telegram.Sequences;

delegate IAsyncEnumerator<WaitCondition> TelegramSequenceDelegate(Message message, SequenceTrigger trigger);
