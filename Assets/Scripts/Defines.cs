public enum ECardEffect
{
	NONE = 0,
	PAY_TO_HOSPITAL,
	WINNING_A_LOTTERY,
	MAX = WINNING_A_LOTTERY + 1

}

public enum ECellType
{
	NONE = 0,
	DEFAULT,
	START,
	DONATE,
	SPACESHIP,
	DESERT,
	MAX = DESERT + 1
}

public enum ESteps
{
	NONE = 0,
	THROWING,
	MOVING,
	BUYING,
	WAITING,
	MAX = WAITING + 1
}