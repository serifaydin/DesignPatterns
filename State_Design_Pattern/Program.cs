using System;

namespace State_Design_Pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            #region First State Implementation
            Context context = new Context();

            StartState startState = new StartState();
            startState.doAction(context);

            Console.WriteLine(context.getState().ToString());

            StopState stopState = new StopState();
            stopState.doAction(context);

            Console.WriteLine(context.getState().ToString());
            #endregion

            Console.ReadKey();
        }
    }
}
