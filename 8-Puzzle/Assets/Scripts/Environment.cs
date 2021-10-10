using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EightPuzzle
{
    public class Environment
    {
        public char[] gameState { get; private set; }

        public Environment(string initialState = "123456780") {
            gameState = initialState.ToCharArray();
        }

        public bool canMoveLeft (int i) => i % 3 != 1;
        public bool canMoveRight (int i) => i % 3 != 0;
        public bool canMoveUp (int i) => i > 3;
        public bool canMoveDown (int i) => i < 7;

        public void moveLeft(int i) => swap(i, i-1);
        public void moveRight(int i) => swap(i, i+1);
        public void moveUp(int i) => swap(i, i-3);
        public void moveDown(int i) => swap(i, i+3);

        public int getEmptyPieceLocation() {
            for (int i = 0; i < gameState.Length; i++) {
                if (gameState[i] == '0') return i+1;
            }
            return 0;
        }

        public void swap(int i, int j) {
            char x = gameState[i-1];
            gameState[i-1] = gameState[j-1];
            gameState[j-1] = x;
        }
    }
}
