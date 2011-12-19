using IHI.Server.Habbos;
using IHI.Server.Networking.Messages;
using IHI.Server.Plugins.Cecer1.UserHandlers;

namespace IHI.Server.Plugins.Cecer1.HabboHandlers
{
    [CompatibilityLock(36)]
    public class HabboHandlers : Plugin
    {
        public override void Start()
        {
            CoreManager.ServerCore.GetHabboDistributor().OnHabboLogin += RegisterHandlers;
        }

        private static void RegisterHandlers(object source, HabboEventArgs e)
        {
            Habbo target = (source as Habbo);

            // Is source of type Habbo?
            if (target == null)
                return;

            target.
                GetConnection().
                AddHandler(6, PacketHandlerPriority.DefaultAction, ProcessBalanceRequest).
                AddHandler(7, PacketHandlerPriority.DefaultAction, ProcessHabboInfoRequest).
                AddHandler(8, PacketHandlerPriority.DefaultAction, ProcessGetVolumeLevel).
                AddHandler(228, PacketHandlerPriority.DefaultAction, ProcessGetVolumeLevel).
                AddHandler(482, PacketHandlerPriority.DefaultAction, ProcessUnknown);
        }

        private static void ProcessHabboInfoRequest(Habbo sender, IncomingMessage message)
        {
            new MHabboData
                {
                    Figure = sender.GetFigure() as HabboFigure,
                    Motto = sender.GetMotto(),
                    HabboID = sender.GetID(),
                    Username = sender.GetUsername()
                }.Send(sender);
        }

        private static void ProcessBalanceRequest(Habbo sender, IncomingMessage message)
        {
            new MCreditBalance(sender).Send(sender);
        }

        private static void ProcessGetVolumeLevel(Habbo sender, IncomingMessage message)
        {
            new MVolumeLevel
                {
                    Volume = sender.GetVolume()
                }.Send(sender);
        }


        //TODO: Figure this out.
        private static void ProcessUnknown(Habbo sender, IncomingMessage message)
        {
            CoreManager.ServerCore.GetStandardOut().
                PrintDebug("Part 1: " + message.PopPrefixedString()).
                PrintDebug("Part 2: " + message.PopPrefixedString()).
                PrintDebug("Part 3: " + message.PopPrefixedString()).
                PrintDebug("Part 4: " + message.PopPrefixedString());
        }
    }
}