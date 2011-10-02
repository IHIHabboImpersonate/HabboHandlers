using IHI.Server.Habbos;
using IHI.Server.Networking.Messages;

namespace IHI.Server.Plugins.Cecer1.UserHandlers
{
    public static class HabboExtensionMethods
    {
        public static int GetVolume(this Habbo habbo)
        {
            int volume;

            if (!int.TryParse(habbo.GetPersistantVariable("Client.Volume"), out volume))
            {
                volume = 5; // TODO: Default value in config?
                habbo.SetPersistantVariable("Client.Volume", volume.ToString());
            }
            return volume;
        }

        public static Habbo SetVolume(this Habbo habbo, int volume)
        {
            habbo.SetPersistantVariable("Client.Volume", volume.ToString());
            new MVolumeLevel(volume).Send(habbo); // Update the client.
            return habbo;
        }
    }
}