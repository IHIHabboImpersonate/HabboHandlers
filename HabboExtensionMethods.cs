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

#endregion

namespace IHI.Server.Plugins.Cecer1.UserHandlers
{
    public static class HabboExtensionMethods
    {
        public static int GetVolume(this Habbo habbo)
        {
            int volume;

            if (!int.TryParse(habbo.GetPersistantVariable("Client.Volume"), out volume))
            {
                volume = 5; // TODO: Default value in config
                habbo.SetPersistantVariable("Client.Volume", volume.ToString());
            }
            return volume;
        }

        public static Habbo SetVolume(this Habbo habbo, int volume)
        {
            habbo.SetPersistantVariable("Client.Volume", volume.ToString());
            new MVolumeLevel
                {
                    Volume = volume
                }.Send(habbo); // Update the client.
            return habbo;
        }
    }
}