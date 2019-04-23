using System;
using System.Collections.Generic;

namespace Assets.Scripts.Networking
{
    public static class AvailableGamesList
    {
        public static event Action<List<LanBroadcastInfo>> OnAvailableGamesListChanged = delegate { };
        private static List<LanBroadcastInfo> games;

        public static void HandleNewGamesList (List<LanBroadcastInfo> newGamesList)
        {
            games = newGamesList;
            OnAvailableGamesListChanged(games);
        }
    }
}