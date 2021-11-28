namespace DCFAEngine.StateSystem
{
    public static class DefaultStates
    {
        public static readonly string NULL = "None";

        public static readonly string DEFAULT = IDLE;
        public static readonly string IDLE = "Idle";
        public static readonly string ATTACK = "Attack";
        public static readonly string ATTACK_READY = "AttackReady";
        public static readonly string MOVE = "Move";
        public static readonly string ROTATE = "Rotate";
        public static readonly string FOLLOW = "Follow";
        public static readonly string MOVE_TO_ATTACK = "MoveToAttack";
        public static readonly string BIRTH = "Birth";
        public static readonly string DEATH = "Death";
        public static readonly string JOY = "Joy";

        public static readonly string SPELL = "Spell"; 
        public static readonly string STUN = "Stun";
        public static readonly string ATTACHED = "Attached";

        public static readonly string STAND = "Stand";
        public static readonly string WAIT = "Wait";


        public static readonly string SELECTED = "Selected";
        public static readonly string CAN_SELECTED = "CanSelected";

        public static readonly string SHOWED = "Showed";
        public static readonly string HIDDEN = "Hidden";

        public static readonly string ACTOVATED = "Activated";
    }
}
