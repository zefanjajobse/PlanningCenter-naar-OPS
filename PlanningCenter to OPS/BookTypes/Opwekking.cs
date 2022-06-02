﻿using System;
using System.Linq;

namespace PlanningCenter_to_OPS.BookTypes
{
    internal class Opwekking : BookType
    {
        public Opwekking(string song_name)
        {
            this.song_name = song_name;
        }
        public override string SongBookName => "Opwekking";

        public override string StyleName => "OPS";

        public override string SelectedVersion => "ProjectionVersion";

        public override string SongName => this.song_name;

        public override string SongNumber => this.song_name.Split()[0].TrimStart('0');

        public override string Title => String.Join(" ", this.song_name.Split().Skip(1));
    }
}