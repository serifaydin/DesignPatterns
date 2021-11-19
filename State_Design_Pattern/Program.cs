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

            #region State Machine Work Flow Implementation
            Car car = new Car();

            Console.WriteLine($"State: {car.CurrentState}");

            car.TakeAction(ActionEnum.Start);
            Console.WriteLine($"State: {car.CurrentState}");

            car.TakeAction(ActionEnum.Start);
            Console.WriteLine($"State: {car.CurrentState}");

            car.TakeAction(ActionEnum.Accelerate);
            Console.WriteLine($"State: {car.CurrentState}");

            car.TakeAction(ActionEnum.Stop);
            Console.WriteLine($"State: {car.CurrentState}");

            car.TakeAction(ActionEnum.Start);
            Console.WriteLine($"State: {car.CurrentState}");
            #endregion

            Console.ReadKey();
        }
    }
}
