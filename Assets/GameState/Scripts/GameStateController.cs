using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alabaster.GameState
{
    public enum EGameState
    {
        Environment,
        Dialogue,
        Menu
    }
    
    public class GameStateController : MonoBehaviour
    {
        private static EGameState gameState;

        public static EGameState GameState { get => gameState; set => gameState = value; }
    }
}

