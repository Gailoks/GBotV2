using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace GBotV2.Telegram;

class TelegramClient(IOptions<TelegramClient.Options> options, ITelegramEventHandler handler)
{
    public const char CommandPrefix = '/';

    private readonly ITelegramEventHandler _handler = handler;

    private readonly Options _options = options.Value;
    public TelegramBotClient Client { get; } = new(options.Value.Token);

    public void Start()
    {
        Client.StartReceiving(HandleUpdateAsync, HandleExceptionAsync);
        Thread.Sleep(-1);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken ct)
    {
        switch(update.Type)
        {
        case UpdateType.Message:
            Message message = update.Message!;
            if (message.Type == MessageType.Text && message.Text!.StartsWith(CommandPrefix))
                await _handler.HandleCommandAsync(message.Text[1..], message.Chat);
            else await _handler.HandleMessageAsync(message);
            break;

        case UpdateType.CallbackQuery:

                CallbackQuery callbackQuery = update.CallbackQuery!;
                await _handler.HandleButtonAsync(callbackQuery.Message!, callbackQuery.Data!);
            break;
        }
    }

    private async Task HandleExceptionAsync(ITelegramBotClient client, Exception exception, CancellationToken ct)
    {

    }

    public class Options
    {
        public required string Token { get; init; }

    }
}