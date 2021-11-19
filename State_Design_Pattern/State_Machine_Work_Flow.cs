namespace State_Design_Pattern
{
    public enum StateEnum
    {
        Stopped,
        Started,
        Running
    }
    public enum ActionEnum
    {
        Stop,
        Start,
        Accelerate
    }

    public class Car
    {
        private StateEnum state = StateEnum.Stopped;

        public StateEnum CurrentState
        {
            get
            {
                return state;
            }
        }

        public void TakeAction(ActionEnum action)
        {
            state = (state, action) switch
            {
                (StateEnum.Stopped, ActionEnum.Start) => StateEnum.Started,
                (StateEnum.Started, ActionEnum.Accelerate) => StateEnum.Running,
                (StateEnum.Started, ActionEnum.Stop) => StateEnum.Stopped,
                (StateEnum.Running, ActionEnum.Stop) => StateEnum.Stopped,
                _ => state
            };
        }
    }
}
