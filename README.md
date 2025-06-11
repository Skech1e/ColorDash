# ColorDash
The project is made with Unity 6000.0.43f1<br/>
Controls are the buttons on screen.<br/>
As player crosses gates, its speed also increases. Score is also updated according along with saving highscores on the device with PlayerPrefs.<br/>
Object-pooling has been used for gates' spawning and ground tiles' spawning.<br/>
Player-Gate engagement has been handled with a combination of OnTriggerEnter2D, OnTriggerExit2D, and events.<br/>
A basic UI is also present.<br/>

#### Player
Controls the player- its running speed, animator, its colour, and listens to game events<br/>
#### Gate
Each gate has this class and it handles the engagement with Player using OnTriggerEnter2D and OnTriggerExit2D.<br/>
This also handles different behaviours of the gates like setting its colour, and fading out after player passes through.<br/>
It stores and retrieves its relevant data- color and if the player has passed the gate in the struct GateData.<br/>
#### GroundPoolManager
This handles shuffling a fixed set of ground tiles as the player moves implementing object pooling.<br/>
#### Spawner
Spawns the gate at a given distance range. This also handles shuffling of fixed set of gates as the player moves implementing object pooling.<br/>
#### GameManager
Deals with game over event, game pause/resume/retry, and updates scores live and saves highscore locally with PlayerPrefs.<br/>
