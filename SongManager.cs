using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesura
{
    public class SongManager
    {
        public Song[] songs;
        Caesura game;
        Random random;
        int startFrequency = 4;
        int stopFrequency = 2;
        public bool caesura = false;
        int previousMode = 1;

        public SongManager(Caesura game)
        {
            this.game = game;
            songs = new Song[4];
            random = new Random();
            MediaPlayer.IsRepeating = true;
        }

        public void LoadContent(ContentManager content)
        {
            songs[0] = content.Load<Song>("Glimmer");
            songs[1] = content.Load<Song>("Shine");
            songs[2] = content.Load<Song>("Brilliant");
            songs[3] = content.Load<Song>("Flow");
        }

        public void ModeChange(int newMode)
        {
            if(!caesura)
            {
                if (game.mode != newMode)
                {
                    game.mode = newMode;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(songs[newMode]);
                }
            }         
        }

        public void Update()
        {
            int randomizer = random.Next(1, 1001);
            if(!caesura && (randomizer < stopFrequency))
            {
                MediaPlayer.Pause();
                caesura = true;
                previousMode = game.mode;
                game.mode = 1;
                game.player1.points += game.player1.currentPoints;
                game.player2.points += game.player2.currentPoints;
            }
            else if(caesura && (randomizer < startFrequency))
            {
                MediaPlayer.Resume();
                caesura = false;
                game.mode = previousMode;
            }            
        }
    }
}
