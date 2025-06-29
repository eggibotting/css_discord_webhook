using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using CSSDiscordWebhook.Discord;
using CSSDiscordWebhook.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static CSSDiscordWebhook.Util.ChatColors;

namespace CSSDiscordWebhook.Player;

public class PlayerCommands(PlayerMethods playerMethods, ILogger<PlayerCommands> logger)
{
    private PlayerMethods _playerMethods = playerMethods;
    private ILogger<PlayerCommands> _logger = logger;

    public void ReadyCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            _logger.LogWarning("Command was called without a player context.");
            return;
        }

        _playerMethods.ReadyPlayer(player);
    }

    public void UnreadyCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            _logger.LogWarning("Command was called without a player context.");
            return;
        }

        _playerMethods.UnreadyPlayer(player);
    }

    public void PauseCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            _logger.LogWarning("Command was called without a player context.");
            return;
        }

        _playerMethods.PauseGame(player.Team);
    }

    public void UnpauseCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            _logger.LogWarning("Command was called without a player context.");
            return;
        }

        _playerMethods.UnpauseGame(player.Team);
    }

    public void CallAdminCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            _logger.LogWarning("Command was called without a player context.");
            return;
        }

        var callMessage = command.ArgString;
        _playerMethods.CallAdmin(player, callMessage);

        command.ReplyToCommand(blue.ToColoredChat("Message sent to Admin."));
    }

    public void HelpCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            _logger.LogWarning("Command was called without a player context.");
            return;
        }

        command.ReplyToCommand(green.ToColoredChat("Available commands:"));
        command.ReplyToCommand(green.ToColoredChat("!ready - Mark yourself as ready.\n"));
        command.ReplyToCommand(green.ToColoredChat("!unready - Unmark yourself as ready.\n"));
        command.ReplyToCommand(green.ToColoredChat("!pause - Pause the game for your team.\n"));
        command.ReplyToCommand(green.ToColoredChat("!unpause - Unpause the game for your team.\n"));
        command.ReplyToCommand(green.ToColoredChat("!admin <message> - Call an admin with an optional message."));
    }
}