using System.Collections;
using System.Collections.Generic;

namespace EightPuzzle
{
    public class Environment
    {
        public char[] gameState { get; private set; }
        private char[] goalState;

        public Environment(string initialState = "123456780") {
            gameState = initialState.ToCharArray();
            goalState = "123456780".ToCharArray();
        }

        public bool canMoveLeft (int i) => i % 3 != 1;
        public bool canMoveRight (int i) => i % 3 != 0;
        public bool canMoveUp (int i) => i > 3;
        public bool canMoveDown (int i) => i < 7;

        public void moveLeft(int i) => swap(i, i-1, gameState);
        public void moveRight(int i) => swap(i, i+1, gameState);
        public void moveUp(int i) => swap(i, i-3, gameState);
        public void moveDown(int i) => swap(i, i+3, gameState);

        public int getEmptyPieceLocation(char[] state) {
            for (int i = 0; i < state.Length; i++) {
                if (state[i] == '0') return i+1;
            }
            return 0;
        }

        public string swap(int i, int j, char[] state) {
            char x = state[i-1];
            state[i-1] = state[j-1];
            state[j-1] = x;
            return new string(state);
        }

        public bool isGoalState(char[] state) {
            for (int i = 0; i < state.Length; i++) {
                if (state[i] != goalState[i]) return false;
            }
            return true;
        }

        public List<string> getPossibleMoves(string state, out List<char> action) {
            int i = getEmptyPieceLocation(state.ToCharArray());
            List<string> possibleMoves = new List<string>();
            action = new List<char>();
            if (canMoveUp(i)) {
                possibleMoves.Add(swap(i, i-3, state.ToCharArray()));
                action.Add('U');
            }
            if (canMoveDown(i)) {
                possibleMoves.Add(swap(i, i+3, state.ToCharArray()));
                action.Add('D');
            }
            if (canMoveLeft(i)) {
                possibleMoves.Add(swap(i, i-1, state.ToCharArray()));
                action.Add('L');
            }
            if (canMoveRight(i)) {
                possibleMoves.Add(swap(i, i+1, state.ToCharArray()));
                action.Add('R');
            }
            return possibleMoves;
        }

        // Heuristics

        public int misplacedTiles(string state) {
            int numberOfMisplacedTiles = 0;
            for (int i = 0; i < state.Length; i++) {
                if (state[i] == goalState[i]) numberOfMisplacedTiles++;
            }
            return numberOfMisplacedTiles;
        }

        
    }
}
