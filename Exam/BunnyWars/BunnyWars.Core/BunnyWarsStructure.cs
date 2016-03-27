namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private const int MaxNumberOfTeams = 5;

        private SortedSet<Bunny>[] bunniesByTeam;

        private Dictionary<string, Bunny> bunniesByName;

        private OrderedDictionary<int, List<Bunny>[]> bunniesByRoomAndTeam;

        private OrderedDictionary<string, Bunny> namesForSuffixSearch;

        public BunnyWarsStructure()
        {
            this.bunniesByName = new Dictionary<string, Bunny>();
            this.bunniesByTeam = new SortedSet<Bunny>[MaxNumberOfTeams];
            this.bunniesByRoomAndTeam = new OrderedDictionary<int, List<Bunny>[]>();
            this.namesForSuffixSearch = new OrderedDictionary<string, Bunny>(string.CompareOrdinal);
        }

        public int BunnyCount => this.bunniesByName.Count;

        public int RoomCount => this.bunniesByRoomAndTeam.Count;

        public void AddRoom(int roomId)
        {
            if (this.bunniesByRoomAndTeam.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            this.bunniesByRoomAndTeam[roomId] = new List<Bunny>[MaxNumberOfTeams];
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (this.bunniesByName.ContainsKey(name) || !this.bunniesByRoomAndTeam.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            var newBunny = new Bunny(name, team, roomId);
            this.bunniesByName[name] = newBunny;

            if (this.bunniesByTeam[team] == null)
            {
                this.bunniesByTeam[team] = new SortedSet<Bunny>(Comparer<Bunny>.Create((b1, b2) => b2.Name.CompareTo(b1.Name)));
            }

            this.bunniesByTeam[team].Add(newBunny);

            if (this.bunniesByRoomAndTeam[roomId][team] == null)
            {
                this.bunniesByRoomAndTeam[roomId][team] = new List<Bunny>();
            }

            this.bunniesByRoomAndTeam[roomId][team].Add(newBunny);

            var reversedName = string.Join("", name.Reverse());
            this.namesForSuffixSearch.Add(reversedName, newBunny);
        }

        public void Remove(int roomId)
        {
            if (!this.bunniesByRoomAndTeam.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < MaxNumberOfTeams; i++)
            {
                var bunniesForRemoval = this.bunniesByRoomAndTeam[roomId][i];
                if (bunniesForRemoval == null)
                {
                    continue;
                }

                foreach (var bunny in bunniesForRemoval)
                {
                    var reversedName = string.Join("", bunny.Name.Reverse());

                    this.bunniesByName.Remove(bunny.Name);
                    this.bunniesByTeam[bunny.Team].Remove(bunny);
                    this.namesForSuffixSearch.Remove(reversedName);
                }
            }

            this.bunniesByRoomAndTeam.Remove(roomId);
        }

        public void Next(string bunnyName)
        {
            this.ValidateBunnyExist(bunnyName);

            var bunny = this.bunniesByName[bunnyName];

            int nextRoom;
            try
            {
                nextRoom = this.bunniesByRoomAndTeam.RangeFrom(bunny.RoomId, false).Select(x => x.Key).First();
            }
            catch (Exception)
            {
                nextRoom = this.bunniesByRoomAndTeam.Keys.First();
            }

            this.bunniesByRoomAndTeam[bunny.RoomId][bunny.Team].Remove(bunny);

            if (this.bunniesByRoomAndTeam[nextRoom][bunny.Team] == null)
            {
                this.bunniesByRoomAndTeam[nextRoom][bunny.Team] = new List<Bunny>();
            }

            this.bunniesByRoomAndTeam[nextRoom][bunny.Team].Add(bunny);
            bunny.RoomId = nextRoom;
        }

        public void Previous(string bunnyName)
        {
            this.ValidateBunnyExist(bunnyName);

            var bunny = this.bunniesByName[bunnyName];

            int previousRoom;

            try
            {
                previousRoom = this.bunniesByRoomAndTeam.RangeTo(bunny.RoomId, false).Reversed().Keys.First();
            }
            catch (Exception)
            {
                previousRoom = this.bunniesByRoomAndTeam.Reversed().Keys.First();
            }

            this.bunniesByRoomAndTeam[bunny.RoomId][bunny.Team].Remove(bunny);

            if (this.bunniesByRoomAndTeam[previousRoom][bunny.Team] == null)
            {
                this.bunniesByRoomAndTeam[previousRoom][bunny.Team] = new List<Bunny>();
            }

            this.bunniesByRoomAndTeam[previousRoom][bunny.Team].Add(bunny);
            bunny.RoomId = previousRoom;
        }

        public void Detonate(string bunnyName)
        {
            this.ValidateBunnyExist(bunnyName);

            var detonatedBunny = this.bunniesByName[bunnyName];
            var room = detonatedBunny.RoomId;

            for (int i = 0; i < MaxNumberOfTeams; i++)
            {
                if (i == detonatedBunny.Team)
                {
                    continue;
                }

                var bunniesInRoom = this.bunniesByRoomAndTeam[room][i]?.ToList();
                if (bunniesInRoom == null)
                {
                    continue;
                }

                foreach (var bunny in bunniesInRoom)
                {
                    bunny.Health -= 30;

                    if (bunny.Health <= 0)
                    {
                        var reversedName = string.Join("", bunny.Name.Reverse());

                        this.bunniesByName.Remove(bunny.Name);
                        this.bunniesByTeam[i].Remove(bunny);
                        this.bunniesByRoomAndTeam[room][i].Remove(bunny);
                        this.namesForSuffixSearch.Remove(reversedName);

                        detonatedBunny.Score++;
                    }
                }
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            var result = this.bunniesByTeam[team];

            return result;
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            var reversedSuffix = string.Join("", suffix.Reverse());
            var result = this.namesForSuffixSearch.Range(reversedSuffix, true, reversedSuffix + char.MaxValue, true).Values;

            return result;
        }

        private void ValidateBunnyExist(string bunnyName)
        {
            if (!this.bunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }
        }
    }
}
