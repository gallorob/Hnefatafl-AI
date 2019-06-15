# Hnefatafl-AI
A C# implementation of Hnefatafl. The goal is to have a functioning Bot to play against.
Currently it's a console application (maybe in the future I'll try and make something with WPF).

## Current project status:
  * Board and pieces are fully implemented
  * Pieces' position and player moves are fully implemented and fully validated
  * GameEngine processes captures (both for normal pieces and king)
  * Added Attacker's and Defender's victory condition 
  * Current bots are:
    - TaflBotRandom; just moves randomly (it chooses randomly from a list of valid moves)
    - TaflBotBasic; evaluates the best move it can make in the current turn
    - TaflBotMinimax; uses a vanilla minimax algorithm to evaluate the best move it can make with a defined lookahed.
