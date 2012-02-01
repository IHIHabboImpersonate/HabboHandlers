#region GPLv3

// 
// Copyright (C) 2012  Chris Chenery
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

#endregion

#region Usings

using IHI.Server.Habbos;
using IHI.Server.Networking.Messages;
using IHI.Server.Plugins.Cecer1.UserHandlers;

#endregion

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