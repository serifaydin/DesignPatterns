using System;

namespace State_Design_Pattern
{
    public interface State
    {
        public void doAction(Context context);
    }

    public class Context
    {
        private State state;

        public Context()
        {
            state = null;
        }

        public void setState(State state)
        {
            this.state = state;
        }

        public State getState()
        {
            return state;
        }
    }

    public class StartState : State
    {
        public void doAction(Context context)
        {
            Console.WriteLine("Player is in start state");
            context.setState(this);
        }

        public override String ToString()
        {
            return "Start State";
        }
    }

    public class StopState : State
    {

        public void doAction(Context context)
        {
            Console.WriteLine("Player is in stop state");
            context.setState(this);
        }

        public override String ToString()
        {
            return "Stop State";
        }
    }
}