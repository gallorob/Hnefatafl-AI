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
    - TaflBotMinimaxAB; uses the minimax algorithm with Alpha-Beta pruning to evaluate the best move faster, dropping nodes with known bad moves.

## Example run:
The following is a run of a Tafl game using _Hnefatafl_ rules on a _Brandubh_ table; both players are __TaflBotMinimaxAB__ with a depth of __4__.
> game_062020191052.log
```
[1] - Attacker moves Attacker (BLACK) in D:2 from D:2 to G:2.
	 A  B  C  D  E  F  G 

7	 .  .  .  A  .  .  . 

6	 .  .  .  A  .  .  . 

5	 .  .  .  D  .  .  . 

4	 A  A  D  K  D  A  A 

3	 .  .  .  D  .  .  . 

2	 .  .  .  A  .  .  . 

1	 .  .  .  A  .  .  . 


[2] - Defender moves Defender (WHITE) in D:3 from D:3 to D:2.
	 A  B  C  D  E  F  G 

7	 .  .  .  A  .  .  . 

6	 .  .  .  A  .  .  . 

5	 .  .  .  D  .  .  . 

4	 A  A  D  K  D  A  A 

3	 .  .  .  D  .  .  . 

2	 .  .  .  .  .  .  A 

1	 .  .  .  A  .  .  . 


[3] - Attacker moves Attacker (BLACK) in G:2 from G:2 to F:2.
	 A  B  C  D  E  F  G 

7	 .  .  .  A  .  .  . 

6	 .  .  .  A  .  .  . 

5	 .  .  .  D  .  .  . 

4	 A  A  D  K  D  A  A 

3	 .  .  .  .  .  .  . 

2	 .  .  .  D  .  .  A 

1	 .  .  .  A  .  .  . 


[4] - Defender moves King (WHITE) in D:4 from D:4 to D:3.
	 A  B  C  D  E  F  G 

7	 .  .  .  A  .  .  . 

6	 .  .  .  A  .  .  . 

5	 .  .  .  D  .  .  . 

4	 A  A  D  K  D  A  A 

3	 .  .  .  .  .  .  . 

2	 .  .  .  D  .  A  . 

1	 .  .  .  A  .  .  . 


[5] - Attacker moves Attacker (BLACK) in F:4 from F:4 to F:6.
	 A  B  C  D  E  F  G 

7	 .  .  .  A  .  .  . 

6	 .  .  .  A  .  .  . 

5	 .  .  .  D  .  .  . 

4	 A  A  D  .  D  A  A 

3	 .  .  .  K  .  .  . 

2	 .  .  .  D  .  A  . 

1	 .  .  .  A  .  .  . 


[6] - Defender moves King (WHITE) in D:3 from D:3 to A:3.
	 A  B  C  D  E  F  G 

7	 .  .  .  A  .  .  . 

6	 .  .  .  A  .  A  . 

5	 .  .  .  D  .  .  . 

4	 A  A  D  .  D  .  A 

3	 .  .  .  K  .  .  . 

2	 .  .  .  D  .  A  . 

1	 .  .  .  A  .  .  . 


[7] - Attacker moves Attacker (BLACK) in B:4 from B:4 to B:3.
	 A  B  C  D  E  F  G 

7	 .  .  .  A  .  .  . 

6	 .  .  .  A  .  A  . 

5	 .  .  .  D  .  .  . 

4	 A  A  D  .  D  .  A 

3	 K  .  .  .  .  .  . 

2	 .  .  .  D  .  A  . 

1	 .  .  .  A  .  .  . 


[8] - Defender moves King (WHITE) in A:3 from A:3 to A:1.
	 A  B  C  D  E  F  G 

7	 .  .  .  A  .  .  . 

6	 .  .  .  A  .  A  . 

5	 .  .  .  D  .  .  . 

4	 A  .  D  .  D  .  A 

3	 K  A  .  .  .  .  . 

2	 .  .  .  D  .  A  . 

1	 .  .  .  A  .  .  . 

	 A  B  C  D  E  F  G 

7	 .  .  .  A  .  .  . 

6	 .  .  .  A  .  A  . 

5	 .  .  .  D  .  .  . 

4	 A  .  D  .  D  .  A 

3	 .  A  .  .  .  .  . 

2	 .  .  .  D  .  A  . 

1	 K  .  .  A  .  .  . 

Game over - Defender is victorious!
Game lasted 00:00:09.67
```
__NB__: The game took less than 10 seconds to run thanks to the alpha beta pruning.
