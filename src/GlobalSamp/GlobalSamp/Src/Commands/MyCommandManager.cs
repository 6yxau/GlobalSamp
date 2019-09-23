using System;
using System.Reflection;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.SAMP.Commands.ParameterTypes;
using SampSharp.GameMode.SAMP.Commands.PermissionCheckers;
using SampSharp.GameMode.World;

namespace GlobalSamp.Commands
{
    public class MyCommandManager : CommandsManager
{
    public MyCommandManager(BaseMode gameMode) : base(gameMode)
    {
    }

    #region Overrides of CommandsManager

    protected override ICommand CreateCommand(CommandPath[] commandPaths, string displayName, bool ignoreCase,
        IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage)
    {
        // Create an instance of your own command type.
        return new MyCommand(commandPaths, displayName, ignoreCase, permissionCheckers, method, usageMessage);
    }

    #endregion
}

public class MyCommand : DefaultCommand
{
    public MyCommand(CommandPath[] names, string displayName, bool ignoreCase,
        IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage)
        : base(names, displayName, ignoreCase, permissionCheckers, method, usageMessage)
    {
    }

    #region Overrides of DefaultCommand

    protected override ICommandParameterType GetParameterType(ParameterInfo parameter, int index, int count)
    {
        // Override GetParameterType to use your own automatical detection of parameter types.
        // This way, you can avoid having to attach `ParameterType` attributes to all parameters of a custom type.

        // use default parameter type detection.
        var type = base.GetParameterType(parameter, index, count);
            
        if (type != null)
            return type;

        // if no parameter type was found check if it's of any type we recognize.
        if (parameter.ParameterType == typeof (bool))
        {
            // TODO: detected this type to be of type `bool`. 
            // TODO: Return an implementation of ICommandParameterType which processes booleans.
        }

        // Unrecognized type. Return null.
        return null;
    }
        
    protected override bool SendPermissionDeniedMessage(IPermissionChecker permissionChecker, BasePlayer player)
    {
        // Override SendPermissionDeniedMessage to send permission denied messages in the way you prefer.

        if (permissionChecker == null) throw new ArgumentNullException(nameof(permissionChecker));
        if (player == null) throw new ArgumentNullException(nameof(player));

        if (permissionChecker.Message == null)
            return false;

        // Send permission denied message in red instead of white.
        player.SendClientMessage(Color.Red, permissionChecker.Message);
        return true;
    }
        
    #endregion
}

[Controller]
public class MyCommandController : CommandController
{
    #region Overrides of CommandController

    public override void RegisterServices(BaseMode gameMode, GameModeServiceContainer serviceContainer)
    {
        // Register our own commands manager service instead of the default.
        CommandsManager = new MyCommandManager(gameMode);
        serviceContainer.AddService(CommandsManager);

        // Register commands in game mode.
        CommandsManager.RegisterCommands(gameMode.GetType());
    }

    #endregion
}
}