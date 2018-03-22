using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberphunk
{
    public enum GameStates { start, loadingLevel, gameOn, gameOver };

    class GameState
    {
        public GameStates gameState;

        public GameState()
        {
            gameState = GameStates.start;
        }
    }
}
