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

            Console.WriteLine("---------------");
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

            Console.WriteLine("---------------");
            #endregion

            #region State Machine Work Flow Stateless Implementation
            var externalState = StateEnum.Stopped;

            var carStateless = new Stateless.StateMachine<StateEnum, ActionEnum>(
                () => externalState,
                s => externalState = s
                );

            carStateless.OnTransitioned(t => Console.WriteLine($"Transioned : {t.Source}, {t.Destination}"));

            carStateless.Configure(StateEnum.Stopped)
                .Permit(ActionEnum.Start, StateEnum.Started);

            carStateless.Configure(StateEnum.Started)
                .Permit(ActionEnum.Accelerate, StateEnum.Running)
                .PermitReentry(ActionEnum.Start)
                .Permit(ActionEnum.Stop, StateEnum.Stopped)
                .OnEntry(s => Console.WriteLine($"Entry : {s.Source}, {s.Destination}"))
                .OnExit(s => Console.WriteLine($"Exit : {s.Source}, {s.Destination}"));

            var triggerWSithParam = carStateless.SetTriggerParameters<int>(ActionEnum.Accelerate);

            carStateless.Configure(StateEnum.Running)
                .SubstateOf(StateEnum.Started)
                .Permit(ActionEnum.Stop, StateEnum.Stopped)
                .OnEntryFrom(triggerWSithParam, speed => Console.WriteLine($"Speed : {speed}"))
                .InternalTransition(ActionEnum.Start, () => Console.WriteLine("Start called while running"));

            Console.WriteLine($"State: {externalState}");


            carStateless.Fire(ActionEnum.Start);
            Console.WriteLine($"State: {externalState}");

            carStateless.Fire(triggerWSithParam, 50);
            Console.WriteLine($"State: {externalState}");

            carStateless.Fire(ActionEnum.Start);
            Console.WriteLine($"State: {externalState}");

            carStateless.Fire(ActionEnum.Stop);
            Console.WriteLine($"State: {externalState}");

            Console.WriteLine(carStateless.IsInState(StateEnum.Running));

            Console.WriteLine("------------------------------");
            #endregion

            OrderCreated_Work_Flow ff = new OrderCreated_Work_Flow();

            Console.ReadKey();
        }
    }
}

//Ignore : 
//Permit : 
//Configure : Work Flow
//PermitReentry : 
//OnEntry : Event çalıştığında yapılacak işler tanımlanabilir.
//OnExit : Exent ile işi bittiğinde çalışacak olan event ler tanımlanabilir.
//InternalTransition : Çalışan event sonrasında farklı workflowların geldiğinde yeniden eventin çalışmaması sağlanabilir. Örneğin Running den sonra start yapılması gibi !. InternalTransition olarak tanımlandığı için state değişmeyecektir. 
//OnEntryFrom : 
//OnTransitioned : Her event bittiğinde çalışacak methotlar tanımlanabilir.
//IsInState : State in o andaki durumunu verir.