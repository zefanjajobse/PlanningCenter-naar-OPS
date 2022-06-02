﻿using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace PlanningCenter_to_OPS.Actions
{
    internal class LyricsToFile
    {
        private static readonly string[] RemovedText = {
            "Chorus 1",
            "Chorus 2",
            "Chorus 3",
            "Chorus 4",
            "Verse 1",
            "Verse 2",
            "Verse 3",
            "Verse 4",
            "Verse 5",
            "(Bridge",
            "Misc 1",
            "Misc 2",
            "Misc 3",
            "Misc 4",
            "Misc 5",
            "(Pre-Chorus)",
            "INTERMEZZO",
            "INTRO",
            "(TAG)",
            "(Interlude)",
            "(REPEAT)",

            "Refrein:",
            "</b>"
        };

        internal static void ToFile(Config config, Structs.SongListData song)
        {
            Structs.Lyrics lyrics_return = Api.GetLyrics(config, song.links.self);
            if (lyrics_return.data.attributes.lyrics != null)
            {
                string[] current_lyrics = lyrics_return.data.attributes.lyrics.Split(
                    new string[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                );
                string cleaned_lyrics = "";

                foreach (string line in current_lyrics)
                {
                    if (!RemovedText.Any(s => line.Contains(s)))
                    {
                        cleaned_lyrics += line + "\r\n";
                    }
                }

                try
                {
                    File.WriteAllText(Path.Combine(config.song_folder, String.Format("{0}.txt", song.attributes.title)), cleaned_lyrics);
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show(String.Format("locatie {0} niet gevonden, probeer een andere opslaglocatie voor de liedjes", config.song_folder));
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        config.song_folder = dialog.SelectedPath;
                        config.Update();
                    }
                }
            }
        }
    }
}