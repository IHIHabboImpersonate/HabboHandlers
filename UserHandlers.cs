using IHI.Server.Habbos;
using IHI.Server.Networking.Messages;

namespace IHI.Server.Plugins.Cecer1.UserHandlers
{
    public class UserHandlers : Plugin
    {
        public override void Start()
        {
            Habbo.OnHabboLogin += RegisterHandlers;
        }

        private static void RegisterHandlers(object source, HabboEventArgs e)
        {
            var target = (source as Habbo);

            // Is source of type Habbo?
            if (target == null)
                return;

            target.
                GetConnection().
                AddHandler(6, PacketHandlerPriority.DefaultAction, ProcessBalanceRequest).
                AddHandler(7, PacketHandlerPriority.DefaultAction, ProcessUserInfoRequest).
                AddHandler(8, PacketHandlerPriority.DefaultAction, ProcessGetVolumeLevel).
                AddHandler(228, PacketHandlerPriority.DefaultAction, ProcessGetVolumeLevel).
                AddHandler(482, PacketHandlerPriority.DefaultAction, ProcessUnknown);
        }

        private static void ProcessUserInfoRequest(Habbo sender, IncomingMessage message)
        {
            new MUserData(sender).Send(sender);
        }

        private static void ProcessBalanceRequest(Habbo sender, IncomingMessage message)
        {
            new MCreditBalance(sender).Send(sender);
        }

        private static void ProcessGetVolumeLevel(Habbo sender, IncomingMessage message)
        {
            new MVolumeLevel(sender.GetVolume()).Send(sender);
        }


        //TODO: Figure this out.
        private static void ProcessUnknown(Habbo sender, IncomingMessage message)
        {
            CoreManager.GetServerCore().GetStandardOut().
                PrintDebug("Part 1: " + message.PopPrefixedString()).
                PrintDebug("Part 2: " + message.PopPrefixedString()).
                PrintDebug("Part 3: " + message.PopPrefixedString()).
                PrintDebug("Part 4: " + message.PopPrefixedString());
        }
    }
}